using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Blue10SDK;
using Blue10SDK.Models;
using Blue10SDK.Utils;

namespace Blue10SdkWpfExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IBlue10Client mBlue10Client;
        private readonly ILogger<MainWindow> mlogger;
        
        public MainWindow()
        {
            InitializeComponent();
            ApiKey.Text = Settings.ApiKey;
            ApiUrl.Text = Settings.ApiUrl;
        }

        private void ConnectToApi(object sender, RoutedEventArgs e)
        {
            var fApiKey = ApiKey.Text;
            var fApiUrl = ApiUrl.Text;
            mShowMenu = false;
            mBlue10Client = Blue10.CreateClient(fApiKey, fApiUrl);
            try
            {
                var fMe = new MeActions(mBlue10Client).GetMe();
                connectionStatus.Content = $"Connected to {fMe}";
                // get companies to fill in
                var fCompanies = new CompanyActions(mBlue10Client).GetAll();
                glaccountCompanyList.ItemsSource = fCompanies.Select(x => x.Id).ToList();
                vendorCompanyList.ItemsSource = fCompanies.Select(x => x.Id).ToList();
                vatcodeCompanyList.ItemsSource = fCompanies.Select(x => x.Id).ToList();
                costcenterCompanyList.ItemsSource = fCompanies.Select(x => x.Id).ToList();
                costunitCompanyList.ItemsSource = fCompanies.Select(x => x.Id).ToList();
                paymenttermCompanyList.ItemsSource = fCompanies.Select(x => x.Id).ToList();
                mShowMenu = true;
            }
            catch (Exception ex)
            {
                connectionStatus.Content = $"Connection failed ({ex.Message})";
                mBlue10Client = null;
            }
            ShowMenu();
        }

        private bool mShowMenu = false;
        public void ShowMenu()
        {
            var fValue = (mShowMenu) ? Visibility.Visible : Visibility.Hidden;
            CompanyTab.Visibility = fValue;
            VatCodeTab.Visibility = fValue;
            VatScenarioTab.Visibility = fValue;
            GLAccountTab.Visibility = fValue;
            VendorTab.Visibility = fValue;
            CostCenterTab.Visibility = fValue;
            CostUnitTab.Visibility = fValue;
            ErpActionTab.Visibility = fValue;
            PaymenttermTab.Visibility = fValue;
        }
        #region ErpAction
        private void ListErpActions(object sender, RoutedEventArgs e)
        {
            try
            {
                var fErpActions = new AdministrationActions(mBlue10Client).GetAll();
                erpactionGrid.ItemsSource = fErpActions;
                //companyGrid.Columns[0].IsReadOnly = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"List ErpActions failed ({ex.Message}");
            }
        }

        private void FinishErpAction(object sender, RoutedEventArgs e)
        {
            var fAdministrationAction = ((Button)sender).DataContext as AdministrationAction;
            var fRes = new AdministrationActions(mBlue10Client);
            fRes.Finish(fAdministrationAction);
            ListErpActions(sender, e);
        }
        #endregion

        #region Companies
        private void ListCompanies(object sender, RoutedEventArgs e)
        {
            try
            {
                var fCompanies = new CompanyActions(mBlue10Client).GetAll();
                companyGrid.ItemsSource = fCompanies;
                companyGrid.Columns[0].IsReadOnly = true;
                companyLoginStatusList.ItemsSource = new List<string>() { "login_ok", "login_failed", "unknown" };
                companyCurrencyList.ItemsSource = new List<string>() { "EUR", "USD", "GBP" };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"List Companies failed ({ex.Message}");
            }
        }

        private void SaveCompany(object sender, RoutedEventArgs e)
        {
            var fCompany = ((Button)sender).DataContext as Company;
            var fRes = new CompanyActions(mBlue10Client);
            fRes.Save(fCompany);
        }
        #endregion
        #region VatCodes
        private List<VatCode> mCurrentVatCodes { get; set; }
        private void ListVatCodes(object sender, RoutedEventArgs e)
        {
            try
            {
                var fSelectCompany = (string)vatcodeCompanyList.SelectedItem;
                var fVatCodes = new VatCodeActions(mBlue10Client).GetAll(fSelectCompany);
                mCurrentVatCodes = Extensions.Clone<List<VatCode>>(fVatCodes);
                vatCodesGrid.ItemsSource = fVatCodes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed retrieve VatCodes ({ex.Message}");
            }
        }

        private void SaveVatCode(object sender, RoutedEventArgs e)
        {
            var fVatCode = ((Button)sender).DataContext as VatCode;
            var fRes = new VatCodeActions(mBlue10Client);
            var fCurrent = mCurrentVatCodes.FirstOrDefault(x => x.Id == fVatCode.Id);
            if (fCurrent != null && fCurrent.AdministrationCode != fVatCode.AdministrationCode)
            {
                fVatCode.Id = Guid.Empty;
                fRes.Delete(fCurrent);
            }
            if (string.IsNullOrEmpty(fVatCode.IdCompany)) fVatCode.IdCompany = (string)vatcodeCompanyList.SelectedItem;       
            fRes.Save(fVatCode);
            ListVatCodes(sender, e);
        }

        private void DeleteVatCode(object sender, RoutedEventArgs e)
        {
            var fVatCode = ((Button)sender).DataContext as VatCode;
            var fRes = new VatCodeActions(mBlue10Client);
            fRes.Delete(fVatCode);
            ListVatCodes(sender, e);
        }
        #endregion

        #region VatScenarios
        private List<VatScenario> mCurrentVatScenarios { get; set; }
        private void ListVatScenarios(object sender, RoutedEventArgs e)
        {
            try
            {
                var fSelectCompany = (string)vatscenarioCompanyList.SelectedItem;
                var fVatScenarios = new VatScenarioActions(mBlue10Client).GetAll(fSelectCompany);
                mCurrentVatScenarios = Extensions.Clone<List<VatScenario>>(fVatScenarios);
                vatscenariosGrid.ItemsSource = fVatScenarios;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed retrieve VatCodes ({ex.Message}");
            }
        }

        private void SaveVatScenario(object sender, RoutedEventArgs e)
        {
            var fVatScenario = ((Button)sender).DataContext as VatScenario;
            var fRes = new VatScenarioActions(mBlue10Client);
            var fCurrent = mCurrentVatScenarios.FirstOrDefault(x => x.Id == fVatScenario.Id);
            if (fCurrent != null && fCurrent.AdministrationCode != fVatScenario.AdministrationCode)
            {
                fVatScenario.Id = Guid.Empty;
                fRes.Delete(fCurrent);
            }
            if (string.IsNullOrEmpty(fVatScenario.IdCompany)) fVatScenario.IdCompany = (string)vatscenarioCompanyList.SelectedItem;
            fRes.Save(fVatScenario);
            ListVatScenarios(sender, e);
        }

        private void DeleteVatScenario(object sender, RoutedEventArgs e)
        {
            var fVatScenario = ((Button)sender).DataContext as VatScenario;
            var fRes = new VatScenarioActions(mBlue10Client);
            fRes.Delete(fVatScenario);
            ListVatScenarios(sender, e);
        }
        #endregion

        #region CostCenters
        private List<CostCenter> mCurrentCostCenters { get; set; }
        private void ListCostCenters(object sender, RoutedEventArgs e)
        {
            try
            {
                var fSelectCompany = (string)costcenterCompanyList.SelectedItem;
                var fCostCenters = new CostCenterActions(mBlue10Client).GetAll(fSelectCompany);
                mCurrentCostCenters = Extensions.Clone<List<CostCenter>>(fCostCenters);
                costcenterGrid.ItemsSource = fCostCenters;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed retrieve CostCenters ({ex.Message}");
            }
        }

        private void SaveCostCenter(object sender, RoutedEventArgs e)
        {
            var fCostCenter = ((Button)sender).DataContext as CostCenter;
            var fRes = new CostCenterActions(mBlue10Client);
            var fCurrent = mCurrentCostCenters.FirstOrDefault(x => x.Id == fCostCenter.Id);
            if (fCurrent != null && fCurrent.AdministrationCode != fCostCenter.AdministrationCode)
            {
                fCostCenter.Id = Guid.Empty;
                fRes.Delete(fCurrent);
            }
            if (string.IsNullOrEmpty(fCostCenter.IdCompany)) fCostCenter.IdCompany = (string)costcenterCompanyList.SelectedItem;
            fRes.Save(fCostCenter);
            ListCostCenters(sender, e);
        }

        private void DeleteCostCenter(object sender, RoutedEventArgs e)
        {
            var fCostCenter = ((Button)sender).DataContext as CostCenter;
            var fRes = new CostCenterActions(mBlue10Client);
            fRes.Delete(fCostCenter);
            ListCostCenters(sender, e);
        }
        #endregion

        #region CostUnits
        private List<CostUnit> mCurrentCostUnits { get; set; }
        private void ListCostUnits(object sender, RoutedEventArgs e)
        {
            try
            {
                var fSelectCompany = (string)costunitCompanyList.SelectedItem;
                var fCostUnits = new CostUnitActions(mBlue10Client).GetAll(fSelectCompany);
                mCurrentCostUnits = Extensions.Clone<List<CostUnit>>(fCostUnits);
                costunitGrid.ItemsSource = fCostUnits;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed retrieve CostCenters ({ex.Message}");
            }
        }

        private void SaveCostUnit(object sender, RoutedEventArgs e)
        {
            var fCostUnit = ((Button)sender).DataContext as CostUnit;
            var fRes = new CostUnitActions(mBlue10Client);
            var fCurrent = mCurrentCostUnits.FirstOrDefault(x => x.Id == fCostUnit.Id);
            if (fCurrent != null && fCurrent.AdministrationCode != fCostUnit.AdministrationCode)
            {
                fCostUnit.Id = Guid.Empty;
                fRes.Delete(fCurrent);
            }
            if (string.IsNullOrEmpty(fCostUnit.IdCompany)) fCostUnit.IdCompany = (string)costunitCompanyList.SelectedItem;
            fRes.Save(fCostUnit);
            ListCostUnits(sender, e);
        }

        private void DeleteCostUnit(object sender, RoutedEventArgs e)
        {
            var fCostUnit = ((Button)sender).DataContext as CostUnit;
            var fRes = new CostUnitActions(mBlue10Client);
            fRes.Delete(fCostUnit);
            ListCostUnits(sender, e);
        }
        #endregion

        #region GLAccounts
        private List<GLAccount> mCurrentGLAccounts { get; set; }
        private void ListGLAccounts(object sender, RoutedEventArgs e)
        {
            try
            {
                var fSelectCompany = (string)glaccountCompanyList.SelectedItem;
                if (string.IsNullOrEmpty(fSelectCompany))
                {
                    MessageBox.Show($"Please select company");
                    return;
                }
                var fGLAccounts = new GLAccountActions(mBlue10Client).GetAll(fSelectCompany);
                mCurrentGLAccounts = Extensions.Clone<List<GLAccount>>(fGLAccounts);
                glAccountGrid.ItemsSource = fGLAccounts;
                glaccountVatCodeList.ItemsSource = new VatCodeActions(mBlue10Client).GetAll(fSelectCompany).ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
                glaccountCostCenterList.ItemsSource = new CostCenterActions(mBlue10Client).GetAll(fSelectCompany).ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
                glaccountCostUnitList.ItemsSource = new CostUnitActions(mBlue10Client).GetAll(fSelectCompany).ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection failed ({ex.Message}");
            }
        }

        private void SaveGLAccount(object sender, RoutedEventArgs e)
        {
            var fGLAccount = ((Button)sender).DataContext as GLAccount;
            var fRes = new GLAccountActions(mBlue10Client);
            var fCurrent = mCurrentGLAccounts.FirstOrDefault(x => x.Id == fGLAccount.Id);
            if (fCurrent != null && fCurrent.AdministrationCode != fGLAccount.AdministrationCode)
            {
                fGLAccount.Id = Guid.Empty;
                fRes.Delete(fCurrent);
            }
            if (string.IsNullOrEmpty(fGLAccount.IdCompany)) fGLAccount.IdCompany = (string)glaccountCompanyList.SelectedItem;
            fRes.Save(fGLAccount);
            ListGLAccounts(sender, e);
        }

        private void DeleteGLAccount(object sender, RoutedEventArgs e)
        {
            var fVendor = ((Button)sender).DataContext as Vendor;
            var fRes = new VendorActions(mBlue10Client);
            fRes.Delete(fVendor);
        }

        #endregion

        #region Paymentterms
        private List<PaymentTerm> mCurrentPaymentTerms { get; set; }
        private void ListPaymentTerms(object sender, RoutedEventArgs e)
        {
            try
            {
                var fSelectCompany = (string)paymenttermCompanyList.SelectedItem;
                var fPaymentTerms = new PaymentTermActions(mBlue10Client).GetAll(fSelectCompany);
                mCurrentPaymentTerms = Extensions.Clone<List<PaymentTerm>>(fPaymentTerms);
                paymenttermGrid.ItemsSource = fPaymentTerms;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed retrieve PaymentTerms ({ex.Message}");
            }
        }

        private void SavePaymentTerm(object sender, RoutedEventArgs e)
        {
            var fPaymentTerm = ((Button)sender).DataContext as PaymentTerm;
            var fRes = new PaymentTermActions(mBlue10Client);
            var fCurrent = mCurrentPaymentTerms.FirstOrDefault(x => x.Id == fPaymentTerm.Id);
            if (fCurrent != null && fCurrent.AdministrationCode != fPaymentTerm.AdministrationCode)
            {
                fPaymentTerm.Id = Guid.Empty;
                fRes.Delete(fCurrent);
            }
            if (string.IsNullOrEmpty(fPaymentTerm.IdCompany)) fPaymentTerm.IdCompany = (string)paymenttermCompanyList.SelectedItem;
            fRes.Save(fPaymentTerm);
            ListPaymentTerms(sender, e);
        }

        private void DeletePaymentTerm(object sender, RoutedEventArgs e)
        {
            var fPaymentTerm = ((Button)sender).DataContext as PaymentTerm;
            var fRes = new PaymentTermActions(mBlue10Client);
            fRes.Delete(fPaymentTerm);
            ListPaymentTerms(sender, e);
        }
        #endregion

        #region Vendors
        private List<Vendor> mCurrentVendors { get; set; }
        private void ListVendors(object sender, RoutedEventArgs e)
        {
            try
            {
                var fSelectCompany = (string)vendorCompanyList.SelectedItem;
                if (string.IsNullOrEmpty(fSelectCompany))
                {
                    MessageBox.Show($"Please select company");
                    return;
                }
                var fVendors = new VendorActions(mBlue10Client).GetAll(fSelectCompany);
                mCurrentVendors = Extensions.Clone<List<Vendor>>(fVendors);
                vendorGrid.ItemsSource = fVendors;
                vendorCurrencyList.ItemsSource = new List<string>() { "EUR", "USD", "GBP" };
                vendorVatCodeList.ItemsSource = new VatCodeActions(mBlue10Client).GetAll(fSelectCompany).ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
                vendorGLAccountList.ItemsSource = new GLAccountActions(mBlue10Client).GetAll(fSelectCompany).ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
                vendorPaymenttermList.ItemsSource = new PaymentTermActions(mBlue10Client).GetAll(fSelectCompany).ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection failed ({ex.Message}");
            }
        }

        private void SaveVendor(object sender, RoutedEventArgs e)
        {
            var fVendor = ((Button)sender).DataContext as Vendor;
            var fRes = new VendorActions(mBlue10Client);
            var fCurrent = mCurrentVendors.FirstOrDefault(x => x.Id == fVendor.Id);
            if (fCurrent != null && fCurrent.AdministrationCode != fVendor.AdministrationCode)
            {
                fVendor.Id = Guid.Empty;
                fRes.Delete(fCurrent);
            }
            if (string.IsNullOrEmpty(fVendor.IdCompany)) fVendor.IdCompany = (string)vendorCompanyList.SelectedItem;
            fRes.Save(fVendor);
            ListVendors(sender, e);
        }

        private void DeleteVendor(object sender, RoutedEventArgs e)
        {
            var fVendor = ((Button)sender).DataContext as Vendor;
            var fRes = new VendorActions(mBlue10Client);
            fRes.Delete(fVendor);
            ListVendors(sender, e);
        }

        #endregion


    }
}
