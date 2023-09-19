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
    public partial class DocumentActionWindow : Window
    {
        public DocumentActionWindow(B10DeskHelper pB10DH, DocumentAction pDocAction)
        {
            InitializeComponent();
            B10DH = pB10DH;
            DocAction = pDocAction;
            PurchaseInvoiceLineTab.Visibility = Visibility.Hidden;
            CreatePurchaseInvoiceTab.Visibility = Visibility.Hidden;
            GetPurchaseInvoiceDueDateTab.Visibility = Visibility.Hidden;
            PostPurchaseInvoiceTab.Visibility = Visibility.Hidden;
            switch (DocAction.Action)
            {
                case EDocumentAction.get_purchase_invoice_lines:                   
                    FillPurchaseInvoiceLineTab();
                    break;
                case EDocumentAction.create_purchase_invoice:
                    FillCreatePurchaseInvoiceTab();
                    break;
                case EDocumentAction.post_block_purchase_invoice:
                    FillPostPurchaseInvoiceTab();
                    break;
                case EDocumentAction.get_payment_due_date:                   
                    FillGetPurchaseInvoiceDueDateTab();
                    break;
                case EDocumentAction.unblock_purchase_invoice_for_payment:
                case EDocumentAction.block_purchase_invoice_for_payment:
                    FillUnblockInvoiceTab();
                    break;
                case EDocumentAction.match_purchase_order:
                    FillMatchPurchaseOrderTab();
                    break;

            }
        }

        private B10DeskHelper B10DH { get; set; }
        private DocumentAction DocAction { get; set; }


        private void FillCreatePurchaseInvoiceTab()
        {
            CreatePurchaseInvoiceTab.Visibility = Visibility.Visible;
            CreatePurchaseInvoiceTab.IsSelected = true;
            var fInvoice = DocAction.PurchaseInvoice;
            CreatePurchaseInvoiceText.Text = $"Invoice: {fInvoice.AdministrationCode} / {fInvoice.Blue10Code}, Company: {fInvoice.IdCompany}, Vendor: {fInvoice.VendorCode}, net: {fInvoice.NetAmount.ToString()}, gross: {fInvoice.GrossAmount}, vat: {(fInvoice.GrossAmount - fInvoice.NetAmount)}";
        }
       
        private async void FinishCreatePurchaseInvoice(object sender, RoutedEventArgs e)
        {
            DocAction.PurchaseInvoice.AdministrationCode = CreatePurchaseInvoiceAdminitrationCode.Text;
            if (CreatePurchaseInvoiceDueDate.SelectedDate != null)
            {
                DocAction.PurchaseInvoice.PaymentDueDate = (DateTime)CreatePurchaseInvoiceDueDate.SelectedDate;
            }
            DocAction.Status = "done";
            DocAction.Result = "success";
            await B10DH.SaveDocumentAction(DocAction);
            this.Close();
        }

        private void FillMatchPurchaseOrderTab()
        {
            MatchPurchaseOrderTab.Visibility = Visibility.Visible;
            MatchPurchaseOrderTab.IsSelected = true;
            var fInvoice = DocAction.PurchaseInvoice;
            MatchPurchaseOrderText.Text = $"Invoice: {fInvoice.AdministrationCode} / {fInvoice.Blue10Code}, Company: {fInvoice.IdCompany}, Vendor: {fInvoice.VendorCode}, Purchase order number: {fInvoice.PurchaseOrderNumber}";
        }

        private async void FinishMatchPurchaseOrder(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(MatchPurchaseOrderError.Text))
            {
                DocAction.Result = "error";
                DocAction.Message = MatchPurchaseOrderError.Text;
            }
            else
            {
                DocAction.Result = "success";
            }
            DocAction.PurchaseInvoice.PurchaseOrderNumber = MatchPurchaseOrderNumber.Text;
            DocAction.Status = "done";
            
            await B10DH.SaveDocumentAction(DocAction);
            this.Close();
        }

        private void FillPostPurchaseInvoiceTab()
        {
            PostPurchaseInvoiceTab.Visibility = Visibility.Visible;
            PostPurchaseInvoiceTab.IsSelected = true;
            var fInvoice = DocAction.PurchaseInvoice;
            PostPurchaseInvoiceText.Text = $"Invoice: {fInvoice.AdministrationCode} / {fInvoice.Blue10Code}, Company: {fInvoice.IdCompany}, Vendor: {fInvoice.VendorCode}, net: {fInvoice.NetAmount.ToString()}, gross: {fInvoice.GrossAmount}, vat: {(fInvoice.GrossAmount - fInvoice.NetAmount)}";
            purchaseInvoiceLineGrid.ItemsSource = fInvoice.InvoiceLines;
        }

        private async void FinishPostPurchaseInvoice(object sender, RoutedEventArgs e)
        {
            DocAction.PurchaseInvoice.AdministrationCode = PostPurchaseInvoiceAdminitrationCode.Text;
            if (PostPurchaseInvoiceDueDate.SelectedDate != null)
            {
                DocAction.PurchaseInvoice.PaymentDueDate = (DateTime)PostPurchaseInvoiceDueDate.SelectedDate;
            }
            DocAction.Status = "done";
            DocAction.Result = "success";
            await B10DH.SaveDocumentAction(DocAction);
            this.Close();
        }

        private void FillGetPurchaseInvoiceDueDateTab()
        {
            GetPurchaseInvoiceDueDateTab.Visibility = Visibility.Visible;
            GetPurchaseInvoiceDueDateTab.IsSelected = true;
            var fInvoice = DocAction.PurchaseInvoice;
            PurchaseInvoiceDueDateText.Text = $"Invoice: {fInvoice.AdministrationCode} / {fInvoice.Blue10Code}, Company: {fInvoice.IdCompany}, Vendor: {fInvoice.VendorCode}, date: {fInvoice.InvoiceDate.ToString("yyyy-MM-dd")}, gross: {fInvoice.GrossAmount}, vat: {(fInvoice.GrossAmount - fInvoice.NetAmount)}";
        }
        private async void FinishPurchaseInvoiceDueDate(object sender, RoutedEventArgs e)
        {
            if (GetPurchaseInvoiceDueDate.SelectedDate != null)
            {
                DocAction.PurchaseInvoice.PaymentDueDate = (DateTime)GetPurchaseInvoiceDueDate.SelectedDate;
                DocAction.Status = "done";
                DocAction.Result = "success";
                await B10DH.SaveDocumentAction(DocAction);
            }
            this.Close();
        }

        

        private async void FillPurchaseInvoiceLineTab()
        {
            PurchaseInvoiceLineTab.Visibility = Visibility.Visible;
            PurchaseInvoiceLineTab.IsSelected = true;
            var fInvoice = DocAction.PurchaseInvoice;
            PurchaseInvoiceLineInvoiceText.Text = $"Invoice: {fInvoice.AdministrationCode} / {fInvoice.Blue10Code}, Company: {fInvoice.IdCompany}, Vendor: {fInvoice.VendorCode}, net: {fInvoice.NetAmount.ToString()}, gross: {fInvoice.GrossAmount}, vat: {(fInvoice.GrossAmount - fInvoice.NetAmount)}";

            var fGLAccounts = await B10DH.GetGLAccounts(fInvoice.IdCompany);
            PurchaseInvoiceLineGLAccountList.ItemsSource = fGLAccounts.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            var fVatCodes = await B10DH.GetVatCodes(fInvoice.IdCompany);
            PurchaseInvoiceLineVatCodeList.ItemsSource = fVatCodes.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            var fCostCenters = await B10DH.GetCostCenters(fInvoice.IdCompany);
            PurchaseInvoiceLineCostCenterList.ItemsSource = fCostCenters.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            var fCostUnits = await B10DH.GetCostUnits(fInvoice.IdCompany);
            PurchaseInvoiceLineCostUnitList.ItemsSource = fCostUnits.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            var fDimension3s = await B10DH.GetDimension3s(fInvoice.IdCompany);
            PurchaseInvoiceLineDimension3List.ItemsSource = fDimension3s.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            var fDimension4s = await B10DH.GetDimension4s(fInvoice.IdCompany);
            PurchaseInvoiceLineDimension4List.ItemsSource = fDimension4s.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            var fDimension5s = await B10DH.GetDimension5s(fInvoice.IdCompany);
            PurchaseInvoiceLineDimension5List.ItemsSource = fDimension5s.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            var fProjects = await B10DH.GetProjects(fInvoice.IdCompany);           
            PurchaseInvoiceLineProjectList.ItemsSource = fProjects.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            var fArticles = await B10DH.GetArticles(fInvoice.IdCompany);
            PurchaseInvoiceLineArticleList.ItemsSource = fArticles.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            var fWarehouses = await B10DH.GetWarehouses(fInvoice.IdCompany);
            PurchaseInvoiceLineWarehouseList.ItemsSource = fWarehouses.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            var fItemSource = (DocAction.PurchaseInvoice.InvoiceLines.Count == 0) ? new List<InvoiceLine>() : DocAction.PurchaseInvoice.InvoiceLines;
            PurchaseInvoiceLineGrid.ItemsSource = DocAction.PurchaseInvoice.InvoiceLines;
        }

        private async void FinishPurchaseInvoiceLine(object sender, RoutedEventArgs e)
        {
            DocAction.PurchaseInvoice.InvoiceLines = PurchaseInvoiceLineGrid.ItemsSource as List<InvoiceLine>;
            DocAction.Status = "done";
            DocAction.Result = "success";
            await B10DH.SaveDocumentAction(DocAction);
            this.Close();
        }

        private void FillUnblockInvoiceTab()
        {
            UnblockInvoiceTab.Visibility = Visibility.Visible;
            UnblockInvoiceTab.IsSelected = true;
            var fInvoice = DocAction.PurchaseInvoice;
            var fBlockUnBlock = (DocAction.Action == EDocumentAction.block_purchase_invoice_for_payment) ? "Block" : "Unblock";
            UnblockInvoiceHeaderText.Text = $"{fBlockUnBlock} Invoice: {fInvoice.AdministrationCode} / {fInvoice.Blue10Code}, Company: {fInvoice.IdCompany}, Vendor: {fInvoice.VendorCode}, net: {fInvoice.NetAmount.ToString()}, gross: {fInvoice.GrossAmount}, vat: {(fInvoice.GrossAmount - fInvoice.NetAmount)}";
        }

        private async void FinishUnblockInvoice(object sender, RoutedEventArgs e)
        {
            DocAction.Status = "done";
            if (!string.IsNullOrEmpty(UnblockInvoiceText.Text))
            {
                DocAction.Message = UnblockInvoiceText.Text;
                DocAction.Result = "success_warning";
            }
            else
            {
                DocAction.Result = "success";
            }
            await B10DH.SaveDocumentAction(DocAction);
            this.Close();
    }

    private async void CloseWait(object sender, RoutedEventArgs e)
        {
            DocAction.Status = "waiting_for_administration";
            await B10DH.SaveDocumentAction(DocAction);
            this.Close();
        }

        private void CloseNoSave(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

         //
    }
}
