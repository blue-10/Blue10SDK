using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Blue10SDK;
using Blue10SDK.Models;

namespace Blue10SDKExampleConsole
{   
    public class ProcessDocumentActions
    {
        private readonly string mExportPath;
        private readonly IBlue10Client mBlue10Client;

        public ProcessDocumentActions(IBlue10Client pBlue10Client, string pExportPath)
        {
            mBlue10Client = pBlue10Client;
            mExportPath = pExportPath;
        }

        public void Process()
        {
            if (!Directory.Exists(mExportPath))
            {
                Directory.CreateDirectory(mExportPath);
            }
            var fDocumentActions = mBlue10Client.GetDocumentActions();
            foreach (var fDocumentAction in fDocumentActions)
            {
                switch (fDocumentAction.Action)
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
                var fDocumentOriginal = mBlue10Client.GetPurchaseInvoiceOriginal(pDocumentAction.PurchaseInvoice.Id);
                var fDocumentFolder = string.Empty;
                var fDocumentId = string.Empty;
                var fExportCompanyPath = Path.Combine(mExportPath, pDocumentAction.PurchaseInvoice.IdCompany);
                if (!Directory.Exists(fExportCompanyPath)) Directory.CreateDirectory(fExportCompanyPath);
                if (fDocumentOriginal != null)
                {
                    fDocumentFolder = Path.Combine(mExportPath, pDocumentAction.PurchaseInvoice.IdCompany, pDocumentAction.PurchaseInvoice.Blue10Code);
                    if (!Directory.Exists(fDocumentFolder)) Directory.CreateDirectory(fDocumentFolder);
                    fDocumentId = pDocumentAction.PurchaseInvoice.Id.ToString();
                    var fDocumentFileName = Path.Combine(fDocumentFolder, $"{fDocumentId}.pdf");
                    File.WriteAllBytes(fDocumentFileName, fDocumentOriginal);
                }
                var fInvoice = new XElement("Invoice");
                fInvoice.Add(new XElement("Company", pDocumentAction.PurchaseInvoice.IdCompany));
                fInvoice.Add(new XElement("Journal", "1"));
                fInvoice.Add(new XElement("Supplier", pDocumentAction.PurchaseInvoice.VendorCode));
                fInvoice.Add(new XElement("InvoiceDate", pDocumentAction.PurchaseInvoice.InvoiceDate.ToString("yyyy-MM-dd")));
                fInvoice.Add(new XElement("InvoiceNumber", pDocumentAction.PurchaseInvoice.InvoiceNumber));
                fInvoice.Add(new XElement("Currency", pDocumentAction.PurchaseInvoice.CurrencyCode));
                fInvoice.Add(new XElement("TotalAmount", pDocumentAction.PurchaseInvoice.GrossAmount));
                fInvoice.Add(new XElement("Description", pDocumentAction.PurchaseInvoice.HeaderDescription));
                fInvoice.Add(new XElement("DocumentFolder", fDocumentFolder));
                fInvoice.Add(new XElement("DocumentId", fDocumentId));
                if (pDocumentAction.PurchaseInvoice.InvoiceLines != null)
                {
                    foreach (var fInvoiceLine in pDocumentAction.PurchaseInvoice.InvoiceLines.OrderBy(x => x.LineNo).ToList())
                    {
                        var fLine = new XElement("InvoiceLine");
                        fLine.Add(new XElement("LedgerAccount", fInvoiceLine.GlAccountCode));
                        fLine.Add(new XElement("CostCenter", fInvoiceLine.CostCenterCode ?? string.Empty));
                        fLine.Add(new XElement("Description", fInvoiceLine.Description));
                        fLine.Add(new XElement("NetAmount", fInvoiceLine.NetAmount));
                        fLine.Add(new XElement("VatCode", fInvoiceLine.VatCode));
                        fLine.Add(new XElement("VatAmount", fInvoiceLine.VatAmount));
                        var fGrossAmount = (fInvoiceLine.VatReverseCharged) ? fInvoiceLine.NetAmount : fInvoiceLine.NetAmount + fInvoiceLine.VatAmount;
                        fLine.Add(new XElement("TotalAmount", fGrossAmount));
                        fInvoice.Add(fLine);
                    }
                }

                var fDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), fInvoice);
                var fInvoiceFileName = Path.Combine(fExportCompanyPath, $"{pDocumentAction.PurchaseInvoice.Blue10Code}.xml");
                fDoc.Save(fInvoiceFileName);
                // 
                var fDocumentActionReturn = new DocumentAction()
                {
                    Id = pDocumentAction.Id,
                    Status = "done",
                    PurchaseInvoice = new PurchaseInvoice()
                    {
                        // code of invoice in Erp, take dummy for demo
                        AdministrationCode =  Math.Round((DateTime.Now - new DateTime(2019, 1, 1)).TotalSeconds, 0).ToString(),
                        PaymentDueDate = pDocumentAction.PurchaseInvoice.InvoiceDate.AddDays(30)
                    }
                };
                mBlue10Client.EditDocumentAction(fDocumentActionReturn);
            } 
            catch(Exception ex)
            {
                var fDocumentActionReturn = new DocumentAction()
                {
                    Id = pDocumentAction.Id,
                    Status = "done",
                    Result = "error",
                    Message = ex.Message,
                    
                };
                mBlue10Client.EditDocumentAction(fDocumentActionReturn);
            }
        }

        private void ActionNotImplemented(DocumentAction pDocumentAction)
        {
            var fDocumentActionReturn = new DocumentAction()
            {
                Id = pDocumentAction.Id,
                Status = "done"                
            };
            mBlue10Client.EditDocumentAction(fDocumentActionReturn);
        }
    }
}
