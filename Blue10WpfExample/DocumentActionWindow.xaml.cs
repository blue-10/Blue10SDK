using Blue10SDK;
using Blue10SDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Blue10SdkWpfExample
{
    /// <summary>
    /// Interaction logic for DocumentAction.xaml
    /// </summary>
    public partial class DocumentActionWindow : Window
    {
        public DocumentActionWindow(B10DeskHelper pB10DH, DocumentAction pDocAction)
        {
            InitializeComponent();
            B10DH = pB10DH;
            DocAction = pDocAction;
            switch (DocAction.Action)
            {
                case EDocumentAction.get_purchase_invoice_lines:
                    PurchaseInvoiceLineTab.Visibility = Visibility.Visible;
                    FillPurchaseInvoiceLineTab();
                    break;
            }

        }

        private B10DeskHelper B10DH { get; set; }
        private DocumentAction DocAction { get; set; }

        private async void FillPurchaseInvoiceLineTab()
        {
            var fInvoice = DocAction.PurchaseInvoice;
            PurchaseInvoiceLineInvoiceText.Text = $"Invoice: {fInvoice.AdministrationCode} / {fInvoice.Blue10Code}, Company: {fInvoice.IdCompany}, Vendor: {fInvoice.VendorCode}, net: {fInvoice.NetAmount.ToString()}, gross: {fInvoice.GrossAmount}";

            var fGLAccounts = await B10DH.GetGLAccounts(fInvoice.IdCompany);
            PurchaseInvoiceLineGLAccountList.ItemsSource = fGLAccounts.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");

            var fItemSource = (DocAction.PurchaseInvoice.InvoiceLines.Count == 0) ? new List<InvoiceLine>() : DocAction.PurchaseInvoice.InvoiceLines;
            PurchaseInvoiceLineGrid.ItemsSource = DocAction.PurchaseInvoice.InvoiceLines;
        }

        private void FinishPurchaseInvoiceLine(object sender, RoutedEventArgs e)
        {
            DocAction.PurchaseInvoice.InvoiceLines = PurchaseInvoiceLineGrid.ItemsSource as List<InvoiceLine>;
            
            //PurchaseInvoiceLineGLAccountList.ItemsSource.
        }

        private void CloseNoSave(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //
    }
}
