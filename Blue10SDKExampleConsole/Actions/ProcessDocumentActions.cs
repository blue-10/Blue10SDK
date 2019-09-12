using Blue10SDK;
using System;
using System.IO;
using System.Xml.Linq;

namespace Blue10SDKExampleConsole
{
    public class ProcessDocumentActions
    {
        private string mExportPath { get; }
        private Desk mDesk { get; }
        public ProcessDocumentActions(Desk pDesk, string pExportPath)
        {
            mDesk = pDesk;
            mExportPath = pExportPath;
        }

        public void Process()
        {
            if (!Directory.Exists(mExportPath))
            {
                Directory.CreateDirectory(mExportPath);
            }
            var fDocumentActions = mDesk.GetDocumentActions();
            foreach (var fDocumentAction in fDocumentActions)
            {
                switch (fDocumentAction.action)
                {
                    case "create_purchase_invoice":
                        break;
                    case "get_payment_due_date":
                        break;
                    case "post_block_purchase_invoice":
                    case "post_purchase_invoice":
                        PostPurchaseInvoice(fDocumentAction);
                        break;
                    case "get_purchase_invoice_lines":
                        break;
                    case "block_purchase_invoice_for_payment":
                        break;
                    case "unblock_purchase_invoice_for_payment":
                        break;
                    case "match_purchase_order":
                        break;
                }
            }

        }

        private void PostPurchaseInvoice(DocumentAction pDocumentAction)
        {
            var fDocumentOriginal = mDesk.GetPurchaseInvoiceOriginal(pDocumentAction.purchase_invoice.id);
            var fDocumentFolder = string.Empty;
            var fDocumentId = string.Empty;
            var fExportCompanyPath = Path.Combine(mExportPath, pDocumentAction.purchase_invoice.id_company);
            if (!Directory.Exists(fExportCompanyPath)) Directory.CreateDirectory(fExportCompanyPath);
            if (fDocumentOriginal!= null)
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

            var fDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), fInvoice);
            var fInvoiceFileName = Path.Combine(fExportCompanyPath, $"{pDocumentAction.purchase_invoice.blue10_code}.xml");
            fDoc.Save(fInvoiceFileName);
        }
    }
}
