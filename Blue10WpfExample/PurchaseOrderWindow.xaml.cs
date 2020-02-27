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
    public partial class PurchaseOrderWindow : Window
    {
        public PurchaseOrderWindow(B10DeskHelper pB10DH, PurchaseOrder pPurchaseOrder, List<VendorWpf> pVendors)
        {
            InitializeComponent();
            B10DH = pB10DH;
            PO = pPurchaseOrder;
            Vendors = pVendors;
            this.DataContext = PO;
            FillPurchaseOrderLineTab();
        }

        private B10DeskHelper B10DH { get; set; }
        public PurchaseOrder PO { get; set; }

        private List<VendorWpf> Vendors { get; set; }


        private async void FillPurchaseOrderLineTab()
        {
            vendorList.ItemsSource = Vendors.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            vendorList.DisplayMemberPath = "Value";
            vendorList.SelectedValuePath = "Key";
            var fVendorSelected = Vendors.First(x => x.AdministrationCode == PO.VendorCode);
            vendorList.SelectedIndex = Vendors.IndexOf(fVendorSelected);
           
            //PODescription.SetBinding(PO, "Description");

            var fGLAccounts = await B10DH.GetGLAccounts(PO.IdCompany);
            PurchaseInvoiceLineGLAccountList.ItemsSource = fGLAccounts.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            var fVatCodes = await B10DH.GetVatCodes(PO.IdCompany);
            PurchaseInvoiceLineVatCodeList.ItemsSource = fVatCodes.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            var fCostCenters = await B10DH.GetCostCenters(PO.IdCompany);
            PurchaseInvoiceLineCostCenterList.ItemsSource = fCostCenters.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            var fCostUnits = await B10DH.GetCostUnits(PO.IdCompany);
            PurchaseInvoiceLineCostUnitList.ItemsSource = fCostUnits.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            var fProjects = await B10DH.GetProjects(PO.IdCompany);
            PurchaseInvoiceLineProjectList.ItemsSource = fProjects.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            PurchaseInvoiceLineGrid.ItemsSource = PO.OrderLines;
        }

        private void CloseNoSave(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Save(object sender, RoutedEventArgs e)
        {
            var fPurchaseOrder = ((Button)sender).DataContext as PurchaseOrder;
            //fPurchaseOrder = await B10DH.SavePurchaseOrder(fPurchaseOrder);
            //this.Close();
        }

        //
    }
}
