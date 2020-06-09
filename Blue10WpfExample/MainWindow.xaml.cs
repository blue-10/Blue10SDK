using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Blue10SDK;
using Blue10SDK.Models;

namespace Blue10SdkWpfExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private B10DeskHelper mB10DH = new B10DeskHelper();
        
        private readonly ILogger<MainWindow> mlogger;
        
        public MainWindow()
        {
            InitializeComponent();
            ApiKey.Text = Settings.ApiKey;
            ApiUrl.Text = Settings.ApiUrl;
        }

        private async void ConnectToApi(object sender, RoutedEventArgs e)
        {
            mShowMenu = false;
            var fApiKey = ApiKey.Text;
            var fApiUrl = ApiUrl.Text;
            try
            {
                var fMe = await mB10DH.ConnectToApiAsync(fApiKey, fApiUrl);
                connectionStatus.Content = $"Connected to {fMe}";
                mShowMenu = true;
            }
            catch (Exception ex)
            {
                connectionStatus.Content = $"Connection failed ({ex.Message})";
                return;
            }
            // get companies to fill in
            var fCompanies = await mB10DH.GetCompanies();
            glaccountCompanyList.ItemsSource = fCompanies.Select(x => x.Id).ToList();
            vendorCompanyList.ItemsSource = fCompanies.Select(x => x.Id).ToList();
            vatcodeCompanyList.ItemsSource = fCompanies.Select(x => x.Id).ToList();
            vatscenarioCompanyList.ItemsSource = fCompanies.Select(x => x.Id).ToList();
            costcenterCompanyList.ItemsSource = fCompanies.Select(x => x.Id).ToList();
            costunitCompanyList.ItemsSource = fCompanies.Select(x => x.Id).ToList();
            paymenttermCompanyList.ItemsSource = fCompanies.Select(x => x.Id).ToList();
            projectCompanyList.ItemsSource = fCompanies.Select(x => x.Id).ToList();
            ShowMenu();
        }

        private bool mShowMenu = false;
        public void ShowMenu()
        {
            var fValue = (mShowMenu) ? Visibility.Visible : Visibility.Hidden;
            CompanyTab.Visibility = fValue;
            DocumentActionTab.Visibility = fValue;
            VatCodeTab.Visibility = fValue;
            VatScenarioTab.Visibility = fValue;
            GLAccountTab.Visibility = fValue;
            VendorTab.Visibility = fValue;
            CostCenterTab.Visibility = fValue;
            CostUnitTab.Visibility = fValue;
            ErpActionTab.Visibility = fValue;
            PaymenttermTab.Visibility = fValue;
            ProjectTab.Visibility = fValue;
        }
        #region ErpAction
        private async void ListErpActions(object sender, RoutedEventArgs e)
        {
            try
            {
                var fErpActions = await mB10DH.GetAdministrationActions();
                erpactionGrid.ItemsSource = fErpActions;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"List ErpActions failed ({ex.Message}");
            }
        }

        private async void FinishErpAction(object sender, RoutedEventArgs e)
        {
            try
            {
                var fAdministrationAction = ((Button)sender).DataContext as AdministrationAction;
                await mB10DH.FinishAdministrationAction(fAdministrationAction);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Finish administration action failed ({ex.Message}");
            }
            ListErpActions(sender, e);
        }
        #endregion

        #region DocumentAction

        private async void ListDocumentActions(object sender, RoutedEventArgs e)
        {
            try
            {
                var fDocumentActions = await mB10DH.GetDocumentActions();
                documentactionGrid.ItemsSource = fDocumentActions;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"List DocumentActions failed ({ex.Message}");
            }
        }

        private void OpenDocumentAction(object sender, RoutedEventArgs e)
        {
            var fDocumentAction = ((Button)sender).DataContext as DocumentAction;
            var fDocList = (List<DocumentAction>) documentactionGrid.ItemsSource;
            documentactionGrid.ItemsSource = fDocList.Except(new List<DocumentAction> { fDocumentAction }).ToList();
            var fDocumentActionWindow = new DocumentActionWindow(mB10DH, fDocumentAction);
            fDocumentActionWindow.Show();
        }

        #endregion

        #region Companies
        private async void ListCompanies(object sender, RoutedEventArgs e)
        {
            try
            {
                var fCompanies = await mB10DH.GetCompanies();
                companyGrid.ItemsSource = fCompanies;
                companyGrid.Columns[0].IsReadOnly = true;
                companyGrid.Columns[1].IsReadOnly = true;
                companyLoginStatusList.ItemsSource = new List<string>() { "login_ok", "login_failed", "unknown" };
                companyCurrencyList.ItemsSource = new List<string>() { "EUR", "USD", "GBP" };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"List Companies failed ({ex.Message}");
            }
        }

        private async void SaveCompany(object sender, RoutedEventArgs e)
        {
            try
            {
                var fCompany = ((Button)sender).DataContext as Company;
                await mB10DH.SaveCompany(fCompany);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Save Company failed ({ex.Message}");
            }
            ListCompanies(sender, e);
        }
        #endregion

        #region VatCodes
        private List<VatCode> mCurrentVatCodes { get; set; }
        private async void ListVatCodes(object sender, RoutedEventArgs e)
        {
            try
            {
                var fSelectCompany = (string)vatcodeCompanyList.SelectedItem;
                var fVatCodes = await mB10DH.GetVatCodes(fSelectCompany);
                mCurrentVatCodes = Extensions.Clone<List<VatCode>>(fVatCodes);
                vatCodesGrid.ItemsSource = fVatCodes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed retrieve VatCodes ({ex.Message}");
            }
        }

        private async void SaveVatCode(object sender, RoutedEventArgs e)
        {
            try
            {
                var fVatCode = ((Button)sender).DataContext as VatCode;
                var fCurrent = mCurrentVatCodes.FirstOrDefault(x => x.Id == fVatCode.Id);
                if (fCurrent != null && fCurrent.AdministrationCode != fVatCode.AdministrationCode)
                {
                    fVatCode.Id = Guid.Empty;
                    await mB10DH.DeleteVatCode(fCurrent);
                }
                if (string.IsNullOrEmpty(fVatCode.IdCompany)) fVatCode.IdCompany = (string)vatcodeCompanyList.SelectedItem;
                fVatCode = await mB10DH.SaveVatCode(fVatCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Save VatCode failed ({ex.Message}");
            }
            ListVatCodes(sender, e);
        }

        private async void DeleteVatCode(object sender, RoutedEventArgs e)
        {
            try
            {
                var fVatCode = ((Button)sender).DataContext as VatCode;
                await mB10DH.DeleteVatCode(fVatCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Delete VatCode failed ({ex.Message}");
            }
            ListVatCodes(sender, e);
        }
        #endregion

        #region VatScenarios
        private List<VatScenario> mCurrentVatScenarios { get; set; }
        private async void ListVatScenarios(object sender, RoutedEventArgs e)
        {
            try
            {
                var fSelectCompany = (string)vatscenarioCompanyList.SelectedItem;
                var fVatScenarios = await mB10DH.GetVatScenarios(fSelectCompany);
                mCurrentVatScenarios = Extensions.Clone<List<VatScenario>>(fVatScenarios);
                vatscenariosGrid.ItemsSource = fVatScenarios;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed retrieve VatScenarios ({ex.Message}");
            }
        }

        private async void SaveVatScenario(object sender, RoutedEventArgs e)
        {
            try
            {
                var fVatScenario = ((Button)sender).DataContext as VatScenario;
                var fCurrent = mCurrentVatScenarios.FirstOrDefault(x => x.Id == fVatScenario.Id);
                if (fCurrent != null && fCurrent.AdministrationCode != fVatScenario.AdministrationCode)
                {
                    fVatScenario.Id = Guid.Empty;
                    await mB10DH.DeleteVatScenario(fCurrent);
                }
                if (string.IsNullOrEmpty(fVatScenario.IdCompany)) fVatScenario.IdCompany = (string)vatscenarioCompanyList.SelectedItem;
                fVatScenario = await mB10DH.SaveVatScenario(fVatScenario);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"List ErpActions failed ({ex.Message}");
            }
            ListVatScenarios(sender, e);
        }

        private async void DeleteVatScenario(object sender, RoutedEventArgs e)
        {
            try
            {
                var fVatScenario = ((Button)sender).DataContext as VatScenario;
                await mB10DH.DeleteVatScenario(fVatScenario);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"List ErpActions failed ({ex.Message}");
            }
            ListVatScenarios(sender, e);
        }
        #endregion

        #region CostCenters
        private List<CostCenter> mCurrentCostCenters { get; set; }
        private async void ListCostCenters(object sender, RoutedEventArgs e)
        {
            try
            {
                var fSelectCompany = (string)costcenterCompanyList.SelectedItem;
                var fCostCenters = await mB10DH.GetCostCenters(fSelectCompany);
                mCurrentCostCenters = Extensions.Clone<List<CostCenter>>(fCostCenters);
                costcenterGrid.ItemsSource = fCostCenters;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed retrieve CostCenters ({ex.Message}");
            }
        }

        private async void SaveCostCenter(object sender, RoutedEventArgs e)
        {
            try
            {
                var fCostCenter = ((Button)sender).DataContext as CostCenter;
                var fCurrent = mCurrentCostCenters.FirstOrDefault(x => x.Id == fCostCenter.Id);
                if (fCurrent != null && fCurrent.AdministrationCode != fCostCenter.AdministrationCode)
                {
                    fCostCenter.Id = Guid.Empty;
                    await mB10DH.DeleteCostCenter(fCurrent);
                }
                if (string.IsNullOrEmpty(fCostCenter.IdCompany)) fCostCenter.IdCompany = (string)costcenterCompanyList.SelectedItem;
                fCostCenter = await mB10DH.SaveCostCenter(fCostCenter);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Save CostCenter failed ({ex.Message}");
            }
            ListCostCenters(sender, e);
        }

        private async void DeleteCostCenter(object sender, RoutedEventArgs e)
        {
            try
            {
                var fCostCenter = ((Button)sender).DataContext as CostCenter;
                await mB10DH.DeleteCostCenter(fCostCenter);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Delete CostCenter failed ({ex.Message}");
            }
            ListCostCenters(sender, e);
        }
        #endregion

        #region CostUnits
        private List<CostUnit> mCurrentCostUnits { get; set; }
        private async void ListCostUnits(object sender, RoutedEventArgs e)
        {
            try
            {
                var fSelectCompany = (string)costunitCompanyList.SelectedItem;
                var fCostUnits = await mB10DH.GetCostUnits(fSelectCompany);
                mCurrentCostUnits = Extensions.Clone<List<CostUnit>>(fCostUnits);
                costunitGrid.ItemsSource = fCostUnits;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed retrieve CostCenters ({ex.Message}");
            }
        }

        private async void SaveCostUnit(object sender, RoutedEventArgs e)
        {
            try
            {
                var fCostUnit = ((Button)sender).DataContext as CostUnit;
                var fCurrent = mCurrentCostUnits.FirstOrDefault(x => x.Id == fCostUnit.Id);
                if (fCurrent != null && fCurrent.AdministrationCode != fCostUnit.AdministrationCode)
                {
                    fCostUnit.Id = Guid.Empty;
                    await mB10DH.DeleteCostUnit(fCurrent);
                }
                if (string.IsNullOrEmpty(fCostUnit.IdCompany)) fCostUnit.IdCompany = (string)costunitCompanyList.SelectedItem;
                fCostUnit = await mB10DH.SaveCostUnit(fCostUnit);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Save CostUnit failed ({ex.Message}");
            }
            ListCostUnits(sender, e);
        }

        private async void DeleteCostUnit(object sender, RoutedEventArgs e)
        {
            try
            {
                var fCostUnit = ((Button)sender).DataContext as CostUnit;
                await mB10DH.DeleteCostUnit(fCostUnit);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Delete CostUnit failed ({ex.Message}");
            }
            ListCostUnits(sender, e);
        }
        #endregion

        #region GLAccounts
        private List<GLAccount> mCurrentGLAccounts { get; set; }
        private async void ListGLAccounts(object sender, RoutedEventArgs e)
        {
            try
            {
                var fSelectCompany = (string)glaccountCompanyList.SelectedItem;
                if (string.IsNullOrEmpty(fSelectCompany))
                {
                    MessageBox.Show($"Please select company");
                    return;
                }
                var fGLAccounts = await mB10DH.GetGLAccounts(fSelectCompany);
                mCurrentGLAccounts = Extensions.Clone<List<GLAccount>>(fGLAccounts);
                glAccountGrid.ItemsSource = fGLAccounts;
                var fVatCodes = new List<VatCode>() { new VatCode() { AdministrationCode = string.Empty, Id = Guid.Empty, Name = "None" } };
                fVatCodes.AddRange(await mB10DH.GetVatCodes(fSelectCompany));
                glaccountVatCodeList.ItemsSource = fVatCodes.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
                var fCostCenters = new List<CostCenter>() { new CostCenter() { AdministrationCode = string.Empty, Id = Guid.Empty, Name = "None" } };
                fCostCenters.AddRange(await mB10DH.GetCostCenters(fSelectCompany));
                glaccountCostCenterList.ItemsSource = fCostCenters.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
                var fCostUnits = new List<CostUnit>() { new CostUnit() { AdministrationCode = string.Empty, Id = Guid.Empty, Name = "None" } };
                fCostUnits.AddRange(await mB10DH.GetCostUnits(fSelectCompany));
                glaccountCostUnitList.ItemsSource = fCostUnits.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed retreive GLAccounts ({ex.Message}");
            }
        }

        private async void SaveGLAccount(object sender, RoutedEventArgs e)
        {
            try
            {
                var fGLAccount = ((Button)sender).DataContext as GLAccount;
                var fCurrent = mCurrentGLAccounts.FirstOrDefault(x => x.Id == fGLAccount.Id);
                if (fCurrent != null && fCurrent.AdministrationCode != fGLAccount.AdministrationCode)
                {
                    fGLAccount.Id = Guid.Empty;
                    await mB10DH.DeleteGLAccount(fCurrent);
                }
                if (string.IsNullOrEmpty(fGLAccount.IdCompany)) fGLAccount.IdCompany = (string)glaccountCompanyList.SelectedItem;
                fGLAccount = await mB10DH.SaveGLAccount(fGLAccount);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Save GL Account failed ({ex.Message}");
            }
            ListGLAccounts(sender, e);
        }

        private async void DeleteGLAccount(object sender, RoutedEventArgs e)
        {
            try
            {
                var fGLAccount = ((Button)sender).DataContext as GLAccount;
                await mB10DH.DeleteGLAccount(fGLAccount);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Delete GLAccount failed ({ex.Message}");
            }
            ListGLAccounts(sender, e);
        }

        #endregion

        #region Paymentterms
        private List<PaymentTerm> mCurrentPaymentTerms { get; set; }
        private async void ListPaymentTerms(object sender, RoutedEventArgs e)
        {
            try
            {
                var fSelectCompany = (string)paymenttermCompanyList.SelectedItem;
                var fPaymentTerms = await mB10DH.GetPaymentTerms(fSelectCompany);
                mCurrentPaymentTerms = Extensions.Clone<List<PaymentTerm>>(fPaymentTerms);
                paymenttermGrid.ItemsSource = fPaymentTerms;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed retrieve PaymentTerms ({ex.Message}");
            }
        }

        private async void SavePaymentTerm(object sender, RoutedEventArgs e)
        {
            try
            {
                var fPaymentTerm = ((Button)sender).DataContext as PaymentTerm;

                var fCurrent = mCurrentPaymentTerms.FirstOrDefault(x => x.Id == fPaymentTerm.Id);
                if (fCurrent != null && fCurrent.AdministrationCode != fPaymentTerm.AdministrationCode)
                {
                    fPaymentTerm.Id = Guid.Empty;
                    await mB10DH.DeletePaymentTerm(fCurrent);
                }
                if (string.IsNullOrEmpty(fPaymentTerm.IdCompany)) fPaymentTerm.IdCompany = (string)paymenttermCompanyList.SelectedItem;
                fPaymentTerm = await mB10DH.SavePaymentTerm(fPaymentTerm);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Save PaymentTerm failed ({ex.Message}");
            }
            ListPaymentTerms(sender, e);
        }

        private async void DeletePaymentTerm(object sender, RoutedEventArgs e)
        {
            try
            {
                var fPaymentTerm = ((Button)sender).DataContext as PaymentTerm;
                await mB10DH.DeletePaymentTerm(fPaymentTerm);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Delete PaymentTerm failed ({ex.Message}");
            }
            ListPaymentTerms(sender, e);
        }
        #endregion

        #region Vendors
        private List<VendorWpf> mCurrentVendors { get; set; }
        private async void ListVendors(object sender, RoutedEventArgs e)
        {
            try
            {
                var fSelectCompany = (string)vendorCompanyList.SelectedItem;
                var fVendors =  await mB10DH.GetVendors(fSelectCompany);
                var fVendorsWpf = VendorWpf.GetFromVendors(fVendors);
                mCurrentVendors = Extensions.Clone<List<VendorWpf>>(fVendorsWpf);
                vendorGrid.ItemsSource = fVendorsWpf;
                vendorCurrencyList.ItemsSource = new List<string>() { "", "EUR", "USD", "GBP" };
                var fVatCodes = new List<VatCode>() { new VatCode() { AdministrationCode = string.Empty, Id = Guid.Empty, Name = "None" } };
                fVatCodes.AddRange(await mB10DH.GetVatCodes(fSelectCompany));
                vendorVatCodeList.ItemsSource = fVatCodes.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
                var fGLAccounts = new List<GLAccount>() { new GLAccount() { AdministrationCode = string.Empty, Id = Guid.Empty, Name = "None" } };
                fGLAccounts.AddRange(await mB10DH.GetGLAccounts(fSelectCompany));
                vendorGLAccountList.ItemsSource = fGLAccounts.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
                var fPaymentTerms = new List<PaymentTerm>() { new PaymentTerm() { AdministrationCode = string.Empty, Id = Guid.Empty, Name = "None" } };
                fPaymentTerms.AddRange(await mB10DH.GetPaymentTerms(fSelectCompany));
                vendorPaymenttermList.ItemsSource = fPaymentTerms.ToDictionary(x => x.AdministrationCode, y => $"{y.AdministrationCode} - {y.Name}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Get Vendors failed ({ex.Message}");
            }
        }

        private async void SaveVendor(object sender, RoutedEventArgs e)
        {
            try
            {
                var fVendorWpf = ((Button)sender).DataContext as VendorWpf;
                var fVendor = fVendorWpf as Vendor;
                var fCurrent = mCurrentVendors.FirstOrDefault(x => x.Id == fVendor.Id);
                if (fCurrent != null && fCurrent.AdministrationCode != fVendor.AdministrationCode)
                {
                    fVendor.Id = Guid.Empty;
                    await mB10DH.DeleteVendor(fCurrent);
                }
                if (string.IsNullOrEmpty(fVendor.IdCompany)) fVendor.IdCompany = (string)vendorCompanyList.SelectedItem;
                fVendor = await mB10DH.SaveVendor(fVendor);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Save Vendor failed ({ex.Message}");
            }
            ListVendors(sender, e);
        }

        private async void DeleteVendor(object sender, RoutedEventArgs e)
        {
            try
            {
                var fVendor = ((Button)sender).DataContext as Vendor;
                await mB10DH.DeleteVendor(fVendor);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Delete Vendor failed ({ex.Message}");
            }
            ListVendors(sender, e);
        }

        #endregion

        #region Projects
        private List<Project> mCurrentProjects { get; set; }
        private async void ListProjects(object sender, RoutedEventArgs e)
        {
            try
            {
                var fSelectCompany = (string)projectCompanyList.SelectedItem;
                var fProjects = await mB10DH.GetProjects(fSelectCompany);
                mCurrentProjects = Extensions.Clone<List<Project>>(fProjects);
                projectGrid.ItemsSource = fProjects;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed retrieve Projects ({ex.Message}");
            }
        }

        private async void SaveProject(object sender, RoutedEventArgs e)
        {
            try
            {
                var fProject = ((Button)sender).DataContext as Project;
                var fCurrent = mCurrentProjects.FirstOrDefault(x => x.Id == fProject.Id);
                if (fCurrent != null && fCurrent.AdministrationCode != fProject.AdministrationCode)
                {
                    fProject.Id = Guid.Empty;
                    await mB10DH.DeleteProject(fCurrent);
                }
                if (string.IsNullOrEmpty(fProject.IdCompany)) fProject.IdCompany = (string)projectCompanyList.SelectedItem;
                fProject = await mB10DH.SaveProject(fProject);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Save project failed ({ex.Message}");
            }
            ListProjects(sender, e);
        }

        private async void DeleteProject(object sender, RoutedEventArgs e)
        {
            try
            {
                var fProject = ((Button)sender).DataContext as Project;
                await mB10DH.DeleteProject(fProject);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Delete project failed ({ex.Message}");
            }
            ListProjects(sender, e);
        }
        #endregion
    }
}
