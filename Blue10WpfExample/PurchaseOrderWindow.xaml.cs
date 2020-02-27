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
        public PurchaseOrderWindow(B10DeskHelper pB10DH, PurchaseOrder pPurchaseOrder, List<VendorWpf> pVendors, List<Article> pArticles, List<Warehouse> pWarehouses, List<GLAccount> pGLAccounts, List<VatCode> pVatCodes, 
            List<Project> pProjects, List<CostCenter> pCostCenters, List<CostUnit> pCostUnits)
        {
            InitializeComponent();
            B10DH = pB10DH;
            PO = pPurchaseOrder;
            if (PO.OrderLines == null) PO.OrderLines = new List<PurchaseOrderLine>();
            Vendors = pVendors;
            Articles = pArticles;
            Warehouses = pWarehouses;
            GLAccounts = pGLAccounts;
            VatCodes = pVatCodes;
            Projects = pProjects;
            CostCenters = pCostCenters;
            CostUnits = pCostUnits;
            this.DataContext = PO;
            FillPurchaseOrderLineTab();
        }

        private B10DeskHelper B10DH { get; set; }
        public PurchaseOrder PO { get; set; }
        private List<VendorWpf> Vendors { get; set; }
        private List<Article> Articles { get; set; }
        private List<Warehouse> Warehouses { get; set; }
        private List<GLAccount> GLAccounts { get; set; }
        private List<VatCode> VatCodes { get; set; }
        private List<Project> Projects { get; set; }
        private List<CostCenter> CostCenters { get; set; }
        private List<CostUnit> CostUnits { get; set; }


        private void FillPurchaseOrderLineTab()
        {
            vendorList.ItemsSource = Vendors.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            vendorList.DisplayMemberPath = "Value";
            vendorList.SelectedValuePath = "Key";
            var fVendorSelected = Vendors.First(x => x.AdministrationCode == PO.VendorCode);
            vendorList.SelectedIndex = Vendors.IndexOf(fVendorSelected);

            //PODescription.SetBinding(PO, "Description");

            GLAccountList.ItemsSource = GLAccounts.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            ArticleList.ItemsSource = Articles.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            WarehouseList.ItemsSource = Warehouses.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            VatCodeList.ItemsSource = VatCodes.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            CostCenterList.ItemsSource = CostCenters.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            CostUnitList.ItemsSource = CostUnits.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            ProjectList.ItemsSource = Projects.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            purchaseOrderLineGrid.ItemsSource = PO.OrderLines;
            purchaseOrderLineGrid.CanUserAddRows = true;
            
        }

        private void CloseNoSave(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Save(object sender, RoutedEventArgs e)
        {
            var fPurchaseOrder = ((Button)sender).DataContext as PurchaseOrder;
            try
            {
                var fTest = await B10DH.SavePurchaseOrder(fPurchaseOrder);
            } 
            catch(Exception ex)
            {
                var fError = ex.Message;
            }
            this.Close();
        }

        //
    }
}
