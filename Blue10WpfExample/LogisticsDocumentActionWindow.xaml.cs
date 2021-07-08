using Blue10SDK;
using Blue10SDK.Models;
using System.Collections.Generic;
using System.Windows;

namespace Blue10SdkWpfExample
{
    /// <summary>
    /// Interaction logic for DocumentAction.xaml
    /// </summary>
    public partial class LogisticsDocumentActionWindow : Window
    {
        public LogisticsDocumentActionWindow(B10DeskHelper pB10DH, LogisticsDocumentAction pDocAction)
        {
            InitializeComponent();
            B10DH = pB10DH;
            DocAction = pDocAction;
            CreateInvoiceInLogisticsAdapterTab.Visibility = Visibility.Hidden;
            ExportInvoiceToLogisticsAdapterTab.Visibility = Visibility.Hidden;
            GetMatchInvoiceTab.Visibility = Visibility.Hidden;

            switch (DocAction.Action)
            {
                case EDocumentAction.create_logistics_purchase_invoice:
                    FillCreatePurchaseInvoiceTab();
                    break;
                case EDocumentAction.get_match_result:
                    FillMatchPurchaseOrderTab();
                    break;
                case EDocumentAction.export_invoice_to_logistics_adapter:                   
                    ExportInvoiceToLogisticsTab();
                    break;
            }
        }

        private B10DeskHelper B10DH { get; set; }
        private LogisticsDocumentAction DocAction { get; set; }

        private void FillCreatePurchaseInvoiceTab()
        {
            CreateInvoiceInLogisticsAdapterTab.Visibility = Visibility.Visible;
            CreateInvoiceInLogisticsAdapterTab.IsSelected = true;
            var fInvoice = DocAction.PurchaseInvoice;
            DocAction.Message = "gezet";
            CreatePurchaseInvoiceText.Text = $"Invoice: {fInvoice.Blue10Code}, Company: {fInvoice.IdCompany}, Vendor: {fInvoice.VendorCode}, net: {fInvoice.NetAmount.ToString()}, gross: {fInvoice.GrossAmount}, vat: {(fInvoice.GrossAmount - fInvoice.NetAmount)}";
        }
       
        private async void FinishCreatePurchaseInvoice(object sender, RoutedEventArgs e)
        {
            DocAction.Message = CreatePurchaseInvoiceMessage.Text;
            DocAction.Status = "done";
            DocAction.Result = "success";
            await B10DH.SaveLogisticsDocumentAction(DocAction);
            this.Close();
        }

        private void FillMatchPurchaseOrderTab()
        {
            GetMatchInvoiceTab.Visibility = Visibility.Visible;
            GetMatchInvoiceTab.IsSelected = true;
            var fInvoice = DocAction.PurchaseInvoice;
            MatchPurchaseInvoiceText.Text = $"Invoice: {fInvoice.AdministrationCode} / {fInvoice.Blue10Code}, Company: {fInvoice.IdCompany}, Vendor: {fInvoice.VendorCode}, net: {fInvoice.NetAmount.ToString()}, gross: {fInvoice.GrossAmount}, vat: {(fInvoice.GrossAmount - fInvoice.NetAmount)}";
            DocAction.PurchaseInvoice.InvoiceLines = new List<InvoiceLine>() { };
            InvoiceLineGrid.ItemsSource = new List<InvoiceLine>() { new InvoiceLine() { Description = "doei" } };
        }

        private async void FinishGetMatchInvoice(object sender, RoutedEventArgs e)
        {
            //DocAction.PurchaseInvoice.AdministrationCode = PostPurchaseInvoiceAdminitrationCode.Text;
            //if (PostPurchaseInvoiceDueDate.SelectedDate != null)
            //{
            //    DocAction.PurchaseInvoice.PaymentDueDate = (DateTime)PostPurchaseInvoiceDueDate.SelectedDate;
            //}
            DocAction.Status = "done";
            DocAction.Result = "success";
            await B10DH.SaveLogisticsDocumentAction(DocAction);
            this.Close();
        }

        private void ExportInvoiceToLogisticsTab()
        {
            ExportInvoiceToLogisticsAdapterTab.Visibility = Visibility.Visible;
            ExportInvoiceToLogisticsAdapterTab.IsSelected = true;
            var fInvoice = DocAction.PurchaseInvoice;
            ExportInvoiceToLogisticsText.Text = $"Invoice: {fInvoice.AdministrationCode} / {fInvoice.Blue10Code}, Company: {fInvoice.IdCompany}, Vendor: {fInvoice.VendorCode}, date: {fInvoice.InvoiceDate.ToString("yyyy-MM-dd")}, gross: {fInvoice.GrossAmount}, vat: {(fInvoice.GrossAmount - fInvoice.NetAmount)}";
        }

        private async void FinishExportInvoiceToLogistics(object sender, RoutedEventArgs e)
        {
            DocAction.Status = "done";
            DocAction.Result = "success";
            await B10DH.SaveLogisticsDocumentAction(DocAction);
            this.Close();
        }

 

    private async void CloseWait(object sender, RoutedEventArgs e)
        {
            DocAction.Status = "waiting_for_administration";
            await B10DH.SaveLogisticsDocumentAction(DocAction);
            this.Close();
        }

        private void CloseNoSave(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //
    }
}
