using Blue10SDK;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Blue10SDKExampleConsole
{
    
    public class ProcessDocumentActions
    {
        private string mExportPath { get; }
        private IBlue10Desk MBlue10Desk { get; }
        public ProcessDocumentActions(IBlue10Desk pBlue10Desk, string pExportPath)
        {
            MBlue10Desk = pBlue10Desk;
            mExportPath = pExportPath;
        }

        public void Process()
        {
            if (!Directory.Exists(mExportPath))
            {
                Directory.CreateDirectory(mExportPath);
            }
            var fDocumentActions = MBlue10Desk.GetDocumentActions();
            foreach (var fDocumentAction in fDocumentActions)
            {
                switch (fDocumentAction.action)
                {
                    case EDocumentAction.create_purchase_invoice:
                        break;
                    case EDocumentAction.get_payment_due_date:
                        break;
                    case EDocumentAction.post_block_purchase_invoice:
                    case EDocumentAction.post_purchase_invoice:
                        PostPurchaseInvoice(fDocumentAction);
                        break;
                    case EDocumentAction.get_purchase_invoice_lines:
                        // TODO: Get invoice lines from invoice in Erp
                        ActionNotImplemented(fDocumentAction);
                        break;
                    case EDocumentAction.block_purchase_invoice_for_payment:
                        break;
                    case EDocumentAction.unblock_purchase_invoice_for_payment:
                        break;
                    case EDocumentAction.match_purchase_order:
                        break;
                }
            }

        }

        private void PostPurchaseInvoice(DocumentAction pDocumentAction)
        {
            try
            {
                var fDocumentOriginal = MBlue10Desk.GetPurchaseInvoiceOriginal(pDocumentAction.purchase_invoice.id);
                var fDocumentFolder = string.Empty;
                var fDocumentId = string.Empty;
                var fExportCompanyPath = Path.Combine(mExportPath, pDocumentAction.purchase_invoice.id_company);
                if (!Directory.Exists(fExportCompanyPath)) Directory.CreateDirectory(fExportCompanyPath);
                if (fDocumentOriginal != null)
                {
                    fDocumentFolder = Path.Combine(mExportPath, pDocumentAction.purchase_invoice.id_company, pDocumentAction.purchase_invoice.blue10_code);
                    if (!Directory.Exists(fDocumentFolder)) Directory.CreateDirectory(fDocumentFolder);
                    fDocumentId = pDocumentAction.purchase_invoice.id.ToString();
                    var fDocumentFileName = Path.Combine(fDocumentFolder, $"{fDocumentId}.pdf");
                    File.WriteAllBytes(fDocumentFileName, fDocumentOriginal);
                }
                var fInvoice = new XElement("Invoice");
                fInvoice.Add(new XElement("Company", pDocumentAction.purchase_invoice.id_company));
                fInvoice.Add(new XElement("Journal", "1"));
                fInvoice.Add(new XElement("Supplier", pDocumentAction.purchase_invoice.vendor_code));
                fInvoice.Add(new XElement("InvoiceDate", pDocumentAction.purchase_invoice.invoice_date.ToString("yyyy-MM-dd")));
                fInvoice.Add(new XElement("InvoiceNumber", pDocumentAction.purchase_invoice.invoice_number));
                fInvoice.Add(new XElement("Currency", pDocumentAction.purchase_invoice.currency_code));
                fInvoice.Add(new XElement("TotalAmount", pDocumentAction.purchase_invoice.gross_amount));
                fInvoice.Add(new XElement("Description", pDocumentAction.purchase_invoice.header_description));
                fInvoice.Add(new XElement("DocumentFolder", fDocumentFolder));
                fInvoice.Add(new XElement("DocumentId", fDocumentId));
                if (pDocumentAction.purchase_invoice.invoice_lines != null)
                {
                    foreach (var fInvoiceLine in pDocumentAction.purchase_invoice.invoice_lines.OrderBy(x => x.line_no).ToList())
                    {
                        var fLine = new XElement("InvoiceLine");
                        fLine.Add(new XElement("LedgerAccount", fInvoiceLine.gl_account_code));
                        fLine.Add(new XElement("CostCenter", fInvoiceLine.cost_center_code ?? string.Empty));
                        fLine.Add(new XElement("Description", fInvoiceLine.description));
                        fLine.Add(new XElement("NetAmount", fInvoiceLine.net_amount));
                        fLine.Add(new XElement("VatCode", fInvoiceLine.vat_code));
                        fLine.Add(new XElement("VatAmount", fInvoiceLine.vat_amount));
                        var fGrossAmount = (fInvoiceLine.vat_reverse_charged) ? fInvoiceLine.net_amount : fInvoiceLine.net_amount + fInvoiceLine.vat_amount;
                        fLine.Add(new XElement("TotalAmount", fGrossAmount));
                        fInvoice.Add(fLine);
                    }
                }

                var fDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), fInvoice);
                var fInvoiceFileName = Path.Combine(fExportCompanyPath, $"{pDocumentAction.purchase_invoice.blue10_code}.xml");
                fDoc.Save(fInvoiceFileName);
                // 
                var fDocumentActionReturn = new DocumentAction()
                {
                    id = pDocumentAction.id,
                    status = "done",
                    purchase_invoice = new PurchaseInvoice()
                    {
                        // code of invoice in Erp, take dummy for demo
                        administration_code =  Math.Round((DateTime.Now - new DateTime(2019, 1, 1)).TotalSeconds, 0).ToString(),
                        payment_due_date = pDocumentAction.purchase_invoice.invoice_date.AddDays(30)
                    }
                };
                MBlue10Desk.EditDocumentAction(fDocumentActionReturn);
            } 
            catch(Exception ex)
            {
                var fDocumentActionReturn = new DocumentAction()
                {
                    id = pDocumentAction.id,
                    status = "done",
                    result = "error",
                    message = ex.Message,
                    
                };
                MBlue10Desk.EditDocumentAction(fDocumentActionReturn);
            }
        }

        private void ActionNotImplemented(DocumentAction pDocumentAction)
        {
            var fDocumentActionReturn = new DocumentAction()
            {
                id = pDocumentAction.id,
                status = "done"                
            };
            MBlue10Desk.EditDocumentAction(fDocumentActionReturn);
        }
    }
}
