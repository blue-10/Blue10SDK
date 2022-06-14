using Blue10SDK;
using Blue10SDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Blue10SdkWpfExample
{
    /// <summary>
    /// Interaction logic for DocumentAction.xaml
    /// </summary>
    public partial class LogisticsActionWindow : Window
    {
        public LogisticsActionWindow(B10DeskHelper pB10DH, LogisticsDocumentAction pDocAction)
        {
            InitializeComponent();
            B10DH = pB10DH;
            DocAction = pDocAction;
            CreatePurchaseInvoiceTab.Visibility = Visibility.Hidden;
            GetMatchResultTab.Visibility = Visibility.Hidden;
            ExportInvoiceTab.Visibility = Visibility.Hidden;
            switch (DocAction.Action)
            {
                case ELogisticsDocumentAction.create_logistics_purchase_invoice:
                    FillCreatePurchaseInvoiceTab();
                    break;
                case ELogisticsDocumentAction.get_match_result:
                    FillGetMatchResultTab();
                    break;
                case ELogisticsDocumentAction.export_logistics_purchase_invoice:
                    FillExportInvoiceTab();
                    break;
            }
        }

        private B10DeskHelper B10DH { get; set; }
        private LogisticsDocumentAction DocAction { get; set; }


        private void FillCreatePurchaseInvoiceTab()
        {
            CreatePurchaseInvoiceTab.Visibility = Visibility.Visible;
            CreatePurchaseInvoiceTab.IsSelected = true;
            var fInvoice = DocAction.PurchaseInvoice;
            CreatePurchaseInvoiceText.Text = $"Invoice: {fInvoice.AdministrationCode} / {fInvoice.Blue10Code}, Company: {fInvoice.IdCompany}, Vendor: {fInvoice.VendorCode}, Purchase Order: {fInvoice.PurchaseOrderNumber}, net: {fInvoice.NetAmount.ToString()}, gross: {fInvoice.GrossAmount}";
        }
       
        private async void FinishCreatePurchaseInvoice(object sender, RoutedEventArgs e)
        {
            DocAction.Message = CreatePurchaseInvoiceMessage.Text;            
            DocAction.Status = "done";
            DocAction.Result = "success";
            await B10DH.SaveLogisticsDocumentAction(DocAction);
            this.Close();
        }    

        private async void FillGetMatchResultTab()
        {
            GetMatchResultTab.Visibility = Visibility.Visible;
            GetMatchResultTab.IsSelected = true;
            var fInvoice = DocAction.PurchaseInvoice;
            MatchResultText.Text = $"Invoice: {fInvoice.AdministrationCode} / {fInvoice.Blue10Code}, Company: {fInvoice.IdCompany}, Vendor: {fInvoice.VendorCode}, Purchase Order: {fInvoice.PurchaseOrderNumber}, net: {fInvoice.NetAmount.ToString()}, gross: {fInvoice.GrossAmount}";
            PurchaseOrderNumber.Text = fInvoice.PurchaseOrderNumber;
            var fGLAccounts = await B10DH.GetGLAccounts(fInvoice.IdCompany);
            MatchResultInvoiceLineGLAccountList.ItemsSource = fGLAccounts.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            var fVatCodes = await B10DH.GetVatCodes(fInvoice.IdCompany);
            MatchResultInvoiceLineVatCodeList.ItemsSource = fVatCodes.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            var fCostCenters = await B10DH.GetCostCenters(fInvoice.IdCompany);
            MatchResultInvoiceLineCostCenterList.ItemsSource = fCostCenters.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            var fCostUnits = await B10DH.GetCostUnits(fInvoice.IdCompany);
            MatchResultInvoiceLineCostUnitList.ItemsSource = fCostUnits.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            var fProjects = await B10DH.GetProjects(fInvoice.IdCompany);
            MatchResultInvoiceLineProjectList.ItemsSource = fProjects.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            var fItemSource = new List<InvoiceLine>();
            MatchResultInvoiceLineGrid.ItemsSource = fItemSource;
        }

        private async void FinishMatchResult(object sender, RoutedEventArgs e)
        {
            DocAction.PurchaseInvoice.InvoiceLines = MatchResultInvoiceLineGrid.ItemsSource as List<InvoiceLine>;
            DocAction.PurchaseInvoice.PurchaseOrderNumber = MatchResultText.Text;
            DocAction.Status = "done";
            DocAction.Result = "success";
            await B10DH.SaveLogisticsDocumentAction(DocAction);
            this.Close();
        }

        private void FillExportInvoiceTab()
        {
            ExportInvoiceTab.Visibility = Visibility.Visible;
            ExportInvoiceTab.IsSelected = true;
            var fInvoice = DocAction.PurchaseInvoice;
            ExportInvoiceHeaderText.Text = $"Invoice: {fInvoice.AdministrationCode} / {fInvoice.Blue10Code}, Company: {fInvoice.IdCompany}, Vendor: {fInvoice.VendorCode}, Purchase Order: {fInvoice.PurchaseOrderNumber}, net: {fInvoice.NetAmount.ToString()}, gross: {fInvoice.GrossAmount}";
        }

        private async void FinishExportInvoice(object sender, RoutedEventArgs e)
        {
            DocAction.Message = ExportInvoiceRemark.Text;
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
