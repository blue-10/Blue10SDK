using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blue10SDK.Models;

namespace Blue10SDK
{
    public class Blue10Desk : IBlue10Client
    {
        #region Constants

        private const string ADMINISTRATIONACTIONS = "administrationactions";
        private const string COMPANIES = "companies";
        private const string COSTCENTERS = "costcenters";
        private const string COSTUNITS = "costunits";
        private const string DOCUMENTACTIONS = "documentactions";
        private const string DOCUMENTORIGINALS = "documentoriginals";
        private const string GLACCOUNTS = "glaccounts";
        private const string LOGISTICSDOCUMENTACTIONS = "logisticsdocumentactions";
        private const string ME = "me";
        private const string PAYMENTTERMS = "paymentterms";
        private const string PROJECTS = "projects";
        private const string PURCHASEINVOICES = "purchaseinvoices";
        private const string PURCHASEORDERS = "purchaseorders";
        private const string VATCODES = "vatcodes";
        private const string VATSCENARIOS = "vatscenarios";
        private const string VENDORS = "vendors";

        #endregion

        #region Fields

        private readonly IWebApiAdapter _mB10WebWebApi;

        #endregion

        #region Constructors

        public Blue10Desk(IWebApiAdapter pB10WebWebApi)
        {
            _mB10WebWebApi = pB10WebWebApi;
        }

        #endregion

        #region Me

        public string GetMe() =>
            SyncHelper.RunAsyncAsSync(() => GetItems<Me>(ME)).EnvironmentName;


        #endregion

        #region AdministrationActions

        public List<AdministrationAction> GetAdministrationActions() =>
                SyncHelper.RunAsyncAsSync(() =>
                    GetItems<List<AdministrationAction>>(ADMINISTRATIONACTIONS));

        public bool FinishAdministrationAction(AdministrationAction pAdministrationAction) =>
            SyncHelper.RunAsyncAsSync(() =>
                    DeleteItem($"{ADMINISTRATIONACTIONS}/{pAdministrationAction.Id}"));

        #endregion

        #region Companies

        public List<Company> GetCompanies() =>
            SyncHelper.RunAsyncAsSync(() =>
                GetItems<List<Company>>(COMPANIES));

        public Company UpdateCompany(Company pCompany)
        {
            throw new NotImplementedException();
        }


        public Company EditCompany(Company pCompany) =>
           SyncHelper.RunAsyncAsSync(() =>
                EditAndReturnItem(pCompany, $"{COMPANIES}/{pCompany.Id}"));

        #endregion

        #region CostUnits

        public List<CostUnit> GetCostUnits(string pIdCompany) =>
            SyncHelper.RunAsyncAsSync(() =>
                GetItems<List<CostUnit>>($"{COSTUNITS}/{pIdCompany}"));

        public CostUnit AddCostUnit(CostUnit pCostUnit) =>
            SyncHelper.RunAsyncAsSync(() =>
                AddItem(pCostUnit, COSTUNITS));

        public CostUnit EditCostUnit(CostUnit pCostUnit) =>
            SyncHelper.RunAsyncAsSync(() =>
                EditAndReturnItem(pCostUnit, $"{COSTUNITS}/{pCostUnit.Id}"));


        public bool DeleteCostUnit(CostUnit pCostUnit) =>
            SyncHelper.RunAsyncAsSync(() =>
               DeleteItem($"{COSTUNITS}/{pCostUnit.Id}"));

        #endregion 

        #region CostCenters

        public List<CostCenter> GetCostCenters(string pIdCompany) =>
            SyncHelper.RunAsyncAsSync(() =>
                GetItems<List<CostCenter>>($"{COSTCENTERS}/{pIdCompany}"));

        public CostCenter AddCostCenter(CostCenter pCostCenter) =>
            SyncHelper.RunAsyncAsSync(() =>
                AddItem(pCostCenter, COSTCENTERS));

        public CostCenter EditCostCenter(CostCenter pCostCenter) =>
            SyncHelper.RunAsyncAsSync(() =>
                EditAndReturnItem(pCostCenter, $"{COSTCENTERS}/{pCostCenter.Id}"));

        public bool DeleteCostCenter(CostCenter pCostCenter) =>
            SyncHelper.RunAsyncAsSync(() =>
                DeleteItem($"{COSTCENTERS}/{pCostCenter.Id}"));

        #endregion

        #region DocumentActions

        public List<DocumentAction> GetDocumentActions() =>
                SyncHelper.RunAsyncAsSync(() =>
                    GetItems<List<DocumentAction>>(DOCUMENTACTIONS));

        public string EditDocumentAction(DocumentAction pDocumentAction) =>
                SyncHelper.RunAsyncAsSync(() =>
                    EditItem(pDocumentAction, $"{DOCUMENTACTIONS}/{pDocumentAction.Id}"));


        #endregion

        #region GLAccounts


        public List<GLAccount> GetGLAccounts(string pIdCompany) =>
            SyncHelper.RunAsyncAsSync(() => GetItems<List<GLAccount>>($"{GLACCOUNTS}/{pIdCompany}"));


        public GLAccount AddGLAccount(GLAccount pGLAccount) =>
            SyncHelper.RunAsyncAsSync(() =>
                AddItem(pGLAccount, GLACCOUNTS));


        public GLAccount EditGLAccount(GLAccount pGLAccount) =>
            SyncHelper.RunAsyncAsSync(() =>
                EditAndReturnItem(pGLAccount, $"{GLACCOUNTS}/{pGLAccount.Id}"));


        public bool DeleteGLAccount(GLAccount pGLAccount) =>
            SyncHelper.RunAsyncAsSync(() =>
                DeleteItem($"{GLACCOUNTS}/{pGLAccount.Id}"));

        #endregion 

        #region DocumentActions

        public List<LogisticsDocumentAction> GetLogisticsDocumentActions() =>
            SyncHelper.RunAsyncAsSync(() =>
                GetItems<List<LogisticsDocumentAction>>(LOGISTICSDOCUMENTACTIONS));

        public string EditLogisticsDocumentAction(LogisticsDocumentAction pLogisticsDocumentAction) =>
            SyncHelper.RunAsyncAsSync(() =>
                EditItem(pLogisticsDocumentAction, $"{LOGISTICSDOCUMENTACTIONS}/{pLogisticsDocumentAction.Id}"));

        #endregion

        #region PaymentTerms

 
        public List<PaymentTerm> GetPaymentTerms(string pIdCompany) =>
            SyncHelper.RunAsyncAsSync(() =>
                GetItems<List<PaymentTerm>>($"{PAYMENTTERMS}/{pIdCompany}"));

        public PaymentTerm AddPaymentTerm(PaymentTerm pPaymentTerm) =>
            SyncHelper.RunAsyncAsSync(() =>
                AddItem(pPaymentTerm, PAYMENTTERMS));

        public PaymentTerm EditPaymentTerm(PaymentTerm pPaymentTerm) =>
            SyncHelper.RunAsyncAsSync(() =>
                EditAndReturnItem(pPaymentTerm, $"{PAYMENTTERMS}/{pPaymentTerm.Id}"));


        public bool DeletePaymentTerm(PaymentTerm pPaymentTerm) =>
            SyncHelper.RunAsyncAsSync(() =>
                DeleteItem($"{PAYMENTTERMS}/{pPaymentTerm.Id}"));

        #endregion 

        #region Projects

        public List<Project> GetProjects(string pIdCompany) =>
            SyncHelper.RunAsyncAsSync(() =>
                GetItems<List<Project>>($"{PROJECTS}/{pIdCompany}"));

        public Project AddProject(Project pProject) =>
            SyncHelper.RunAsyncAsSync(() =>
                AddItem(pProject, PROJECTS));

        public Project EditProject(Project pProject) =>
                SyncHelper.RunAsyncAsSync(() =>
                    EditAndReturnItem(pProject, $"{PROJECTS}/{pProject.Id}"));

        public bool DeleteProject(Project pProject) =>
            SyncHelper.RunAsyncAsSync(() =>
                DeleteItem($"{PROJECTS}/{pProject.Id}"));

        #endregion

        #region PurchaseInvoice

        public PurchaseInvoice GetPurchaseInvoice(Guid pId) =>
                SyncHelper.RunAsyncAsSync(() =>
                    GetItems<PurchaseInvoice>($"{PURCHASEINVOICES}/{pId}"));

        public byte[] GetPurchaseInvoiceOriginal(Guid pId)
        {
            var fRet = SyncHelper.RunAsyncAsSync(() =>
                GetItems<DocumentOriginal>($"{PURCHASEINVOICES}/{pId}/documentoriginal"));
            return Base64Helper.GetBytesFromJsonResult(fRet.Content);
        }

        public List<PurchaseInvoice> GetPurchaseInvoiceWithoutPaymentDate(string pIdCompany) =>
             SyncHelper.RunAsyncAsSync(() =>
                 GetItems<List<PurchaseInvoice>>($"{PURCHASEINVOICES}/?filter[payment_date]=null&filter[id_company]={pIdCompany}"));

        #endregion

        #region PurchaseOrder

        public List<PurchaseOrder> GetPurchaseOrders(string pIdCompany) =>
            SyncHelper.RunAsyncAsSync(() =>
                GetItems<List<PurchaseOrder>>($"{PURCHASEORDERS}/{pIdCompany}"));

        public PurchaseOrder AddPurchaseOrder(PurchaseOrder pPurchaseOrder) =>
                SyncHelper.RunAsyncAsSync(() =>
                    AddItem(pPurchaseOrder, PURCHASEORDERS));

        public PurchaseOrder EditPurchaseOrder(PurchaseOrder pPurchaseOrder) =>
                SyncHelper.RunAsyncAsSync(() =>
                    EditAndReturnItem(pPurchaseOrder, $"{PURCHASEORDERS}/{pPurchaseOrder.Id}"));


        public bool DeletePurchaseOrder(PurchaseOrder pPurchaseOrder) =>
                SyncHelper.RunAsyncAsSync(() =>
                    DeleteItem($"{PURCHASEORDERS}/{pPurchaseOrder.Id}"));

        #endregion

        #region VatCodes

        public List<VatCode> GetVatCodes(string pIdCompany) =>
            SyncHelper.RunAsyncAsSync(() => GetItems<List<VatCode>>(
                $"{VATCODES}/{pIdCompany}"));

        public VatCode AddVatCode(VatCode pVatCode) =>
                SyncHelper.RunAsyncAsSync(() => AddItem(pVatCode, VATCODES));

        public VatCode EditVatCode(VatCode pVatCode) =>
                SyncHelper.RunAsyncAsSync(() =>
                    EditAndReturnItem(pVatCode, $"{VATCODES}/{pVatCode.Id}"));

        public bool DeleteVatCode(VatCode pVatCode) =>
            SyncHelper.RunAsyncAsSync(() =>
                    DeleteItem($"{VATCODES}/{pVatCode.Id}"));

        #endregion

        #region VatScenarios

        public List<VatScenario> GetVatScenarios(string pIdCompany) =>
            SyncHelper.RunAsyncAsSync(() => GetItems<List<VatScenario>>(
                $"{VATSCENARIOS}/{pIdCompany}"));

        public VatScenario AddVatScenario(VatScenario pVatScenario) =>
                SyncHelper.RunAsyncAsSync(() => AddItem(pVatScenario, VATSCENARIOS));

        public VatScenario EditVatScenario(VatScenario pVatScenario) =>
                SyncHelper.RunAsyncAsSync(() =>
                    EditAndReturnItem(pVatScenario, $"{VATSCENARIOS}/{pVatScenario.Id}"));

        public bool DeleteVatScenario(VatScenario pVatScenario) =>
            SyncHelper.RunAsyncAsSync(() =>
                    DeleteItem($"{VATSCENARIOS}/{pVatScenario.Id}"));

        #endregion

        #region Vendors

        public List<Vendor> GetVendors(string pIdCompany) =>
                SyncHelper.RunAsyncAsSync(() =>
                    GetItems<List<Vendor>>($"{VENDORS}/{pIdCompany}"));

        public Vendor AddVendor(Vendor pVendor) =>
                SyncHelper.RunAsyncAsSync(() =>
                    AddItem(pVendor, VENDORS));

        public Vendor EditVendor(Vendor pVendor) =>
            SyncHelper.RunAsyncAsSync(() =>
                EditAndReturnItem(pVendor, $"{VENDORS}/{pVendor.Id}"));

        public bool DeleteVendor(Vendor pVendor) =>
                SyncHelper.RunAsyncAsSync(() =>
                    DeleteItem($"{VENDORS}/{pVendor.Id}"));

        #endregion
        
        #region Private methods

        private async Task<T> GetItems<T>(string pPath) =>
            await _mB10WebWebApi.GetAsync<T>(pPath);

        private async Task<T> AddItem<T>(T pItem, string path) =>
            await _mB10WebWebApi.PostAsync(pItem, path);

        private async Task<T> EditAndReturnItem<T>(T pItem, string pUrl) =>
            await _mB10WebWebApi.PutAndReturnAsync(pItem, pUrl);

        private async Task<string> EditItem<T>(T pItem, string pUrl) =>
            await _mB10WebWebApi.PutAsync(pItem, pUrl);

        private async Task<bool> DeleteItem(string pUrl) =>
            await _mB10WebWebApi.DeleteAsync(pUrl);


        #endregion
    }
}