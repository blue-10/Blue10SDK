using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blue10SDK.Models;

namespace Blue10SDK
{
    public class Blue10AsyncDesk : IBlue10AsyncClient
    {
        #region Constants

        private const string ADMINISTRATIONACTIONS = "administrationactions";
        private const string ARTICLES = "articles";
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
        private const string WAREHOUSES = "warehouses";

        #endregion

        #region Fields

        private readonly IWebApiAdapter _mB10WebWebApi;

        #endregion

        #region Constructors

        public Blue10AsyncDesk(IWebApiAdapter pB10WebWebApi)
        {
            _mB10WebWebApi = pB10WebWebApi;
        }

        #endregion

        #region Me

        public Task<Me> GetMeAsync() =>
                GetItem<Me>(ME);

        public Task<List<AdministrationAction>> GetAdministrationActionsAsync() => 
                GetItems<List<AdministrationAction>>(ADMINISTRATIONACTIONS);

        #endregion

        #region AdministrationActions

       

        public Task<bool> FinishAdministrationActionAsync(AdministrationAction pAdministrationAction) =>
                    DeleteItem($"{ADMINISTRATIONACTIONS}/{pAdministrationAction.Id}");

        #endregion

        #region Articles

        public Task<List<Article>> GetArticlesAsync(string pIdCompany) =>
             GetItems<List<Article>>($"{ARTICLES}/{pIdCompany}");

        public Task<Article> AddArticleAsync(Article pArticle) =>
                AddItem(pArticle, ARTICLES);

        public Task<Article> EditArticleAsync(Article pArticle) =>
                EditAndReturnItem(pArticle, $"{ARTICLES}/{pArticle.Id}");

        public Task<bool> DeleteArticleAsync(Article pArticle) =>
                DeleteItem($"{ARTICLES}/{pArticle.Id}");

        #endregion

        #region Companies

        public Task<List<Company>> GetCompaniesAsync() =>
                GetItems<List<Company>>(COMPANIES);


        #endregion

        #region CostUnits

        public Task<List<CostUnit>> GetCostUnitsAsync(string pIdCompany) =>
                GetItems<List<CostUnit>>($"{COSTUNITS}/{pIdCompany}");

        public Task<CostUnit> AddCostUnitAsync(CostUnit pCostUnit) =>
                AddItem(pCostUnit, COSTUNITS);

        public Task<CostUnit> EditCostUnitAsync(CostUnit pCostUnit) =>
                EditAndReturnItem(pCostUnit, $"{COSTUNITS}/{pCostUnit.Id}");


        public Task<bool> DeleteCostUnitAsync(CostUnit pCostUnit) =>
               DeleteItem($"{COSTUNITS}/{pCostUnit.Id}");

        #endregion 

        #region CostCenters

        public Task<List<CostCenter>> GetCostCentersAsync(string pIdCompany) =>
                GetItems<List<CostCenter>>($"{COSTCENTERS}/{pIdCompany}");

        public Task<CostCenter> AddCostCenterAsync(CostCenter pCostCenter) =>
                AddItem(pCostCenter, COSTCENTERS);

        public Task<CostCenter> EditCostCenterAsync(CostCenter pCostCenter) =>
                EditAndReturnItem(pCostCenter, $"{COSTCENTERS}/{pCostCenter.Id}");

        public Task<bool> DeleteCostCenterAsync(CostCenter pCostCenter) =>
                DeleteItem($"{COSTCENTERS}/{pCostCenter.Id}");

        #endregion

        #region DocumentActions

        public Task<List<DocumentAction>> GetDocumentActionsAsync() =>
                    GetItems<List<DocumentAction>>(DOCUMENTACTIONS);

        public Task<string> EditDocumentActionAsync(DocumentAction pDocumentAction) =>
                    EditItem(pDocumentAction, $"{DOCUMENTACTIONS}/{pDocumentAction.Id}");


        #endregion

        #region GLAccounts


        public Task<List<GLAccount>> GetGLAccountsAsync(string pIdCompany) =>
             GetItems<List<GLAccount>>($"{GLACCOUNTS}/{pIdCompany}");


        public Task<GLAccount> AddGLAccountAsync(GLAccount pGLAccount) =>
                AddItem(pGLAccount, GLACCOUNTS);


        public Task<GLAccount> EditGLAccountAsync(GLAccount pGLAccount) =>
                EditAndReturnItem(pGLAccount, $"{GLACCOUNTS}/{pGLAccount.Id}");


        public Task<bool> DeleteGLAccountAsync(GLAccount pGLAccount) =>
                DeleteItem($"{GLACCOUNTS}/{pGLAccount.Id}");
        
        
        
        #endregion 

        #region DocumentActions

        public Task<List<LogisticsDocumentAction>> GetLogisticsDocumentActionsAsync() =>
                GetItems<List<LogisticsDocumentAction>>(LOGISTICSDOCUMENTACTIONS);

        public Task<string> EditLogisticsDocumentActionAsync(LogisticsDocumentAction pLogisticsDocumentAction) =>
                EditItem(pLogisticsDocumentAction, $"{LOGISTICSDOCUMENTACTIONS}/{pLogisticsDocumentAction.Id}");

        #endregion

        #region PaymentTerms

 
        public Task<List<PaymentTerm>> GetPaymentTermsAsync(string pIdCompany) =>
                GetItems<List<PaymentTerm>>($"{PAYMENTTERMS}/{pIdCompany}");

        public Task<PaymentTerm> AddPaymentTermAsync(PaymentTerm pPaymentTerm) =>
                AddItem(pPaymentTerm, PAYMENTTERMS);

        public Task<PaymentTerm> EditPaymentTermAsync(PaymentTerm pPaymentTerm) =>
                EditAndReturnItem(pPaymentTerm, $"{PAYMENTTERMS}/{pPaymentTerm.Id}");


        public Task<bool> DeletePaymentTermAsync(PaymentTerm pPaymentTerm) =>
                DeleteItem($"{PAYMENTTERMS}/{pPaymentTerm.Id}");

        #endregion 

        #region Projects

        public Task<List<Project>> GetProjectsAsync(string pIdCompany) =>
                GetItems<List<Project>>($"{PROJECTS}/{pIdCompany}");

        public Task<Project> AddProjectAsync(Project pProject) =>
                AddItem(pProject, PROJECTS);

        public Task<Project> EditProjectAsync(Project pProject) =>
                    EditAndReturnItem(pProject, $"{PROJECTS}/{pProject.Id}");

        public Task<bool> DeleteProjectAsync(Project pProject) =>
                DeleteItem($"{PROJECTS}/{pProject.Id}");
        
        #endregion

        #region PurchaseInvoice

        public Task<PurchaseInvoice> GetPurchaseInvoiceAsync(Guid pId) =>
                    GetItem<PurchaseInvoice>($"{PURCHASEINVOICES}/{pId}");

        public async Task<byte[]> GetPurchaseInvoiceOriginalAsync(Guid pId)
        {
            var fRet = await GetItem<DocumentOriginal>($"{PURCHASEINVOICES}/{pId}/documentoriginal");
            return Base64Helper.GetBytesFromJsonResult(fRet.Content);
        }

        public Task<List<PurchaseInvoice>> GetPurchaseInvoiceWithoutPaymentDateAsync(string pIdCompany) =>
                 GetItem<List<PurchaseInvoice>>($"{PURCHASEINVOICES}/?filter[payment_date]=null&filter[id_company]={pIdCompany}");

        #endregion

        #region PurchaseOrder

        public Task<List<PurchaseOrder>> GetPurchaseOrdersAsync(string pIdCompany) =>
                GetItem<List<PurchaseOrder>>($"{PURCHASEORDERS}/{pIdCompany}");

        public Task<PurchaseOrder> AddPurchaseOrderAsync(PurchaseOrder pPurchaseOrder) =>
                    AddItem(pPurchaseOrder, PURCHASEORDERS);

        public Task<PurchaseOrder> EditPurchaseOrderAsync(PurchaseOrder pPurchaseOrder) =>
                    EditAndReturnItem(pPurchaseOrder, $"{PURCHASEORDERS}/{pPurchaseOrder.Id}");


        public Task<bool> DeletePurchaseOrderAsync(PurchaseOrder pPurchaseOrder) =>
                    DeleteItem($"{PURCHASEORDERS}/{pPurchaseOrder.Id}");

        #endregion

        #region VatCodes

        public Task<List<VatCode>> GetVatCodesAsync(string pIdCompany) =>
             GetItems<List<VatCode>>($"{VATCODES}/{pIdCompany}");

        public Task<VatCode> AddVatCodeAsync(VatCode pVatCode) =>
                 AddItem(pVatCode, VATCODES);

        public Task<VatCode> EditVatCodeAsync(VatCode pVatCode) =>
                    EditAndReturnItem(pVatCode, $"{VATCODES}/{pVatCode.Id}");

        public Task<bool> DeleteVatCodeAsync(VatCode pVatCode) =>
                DeleteItem($"{VATCODES}/{pVatCode.Id}");

        #endregion

        #region Vendors

        public Task<List<Vendor>> GetVendorsAsync(string pIdCompany) =>
                    GetItems<List<Vendor>>($"{VENDORS}/{pIdCompany}");
        
        public Task<Vendor> AddVendorAsync(Vendor pVendor) =>
                    AddItem(pVendor, VENDORS);

        public Task<Vendor> EditVendorAsync(Vendor pVendor) =>
                EditAndReturnItem(pVendor, $"{VENDORS}/{pVendor.Id}");

        public Task<bool> DeleteVendorAsync(Vendor pVendor) =>
                DeleteItem($"{VENDORS}/{pVendor.Id}");

        public Task<Company> UpdateCompanyAsync(Company pCompany) =>
                EditAndReturnItem(pCompany, $"{COMPANIES}/{pCompany.Id}");

        #endregion
        
        #region VatScenarios

        public Task<List<VatScenario>> GetVatScenariosAsync(string pIdCompany) =>
                GetItems<List<VatScenario>>($"{VATSCENARIOS}/{pIdCompany}");

        public Task<VatScenario> AddVatScenarioAsync(VatScenario pVatScenario) =>
                AddItem(pVatScenario, VATSCENARIOS);

        public Task<VatScenario> EditVatScenarioAsync(VatScenario pVatScenario) =>
                EditAndReturnItem(pVatScenario, $"{VATSCENARIOS}/{pVatScenario.Id}");

        public Task<bool> DeleteVatScenarioAsync(VatScenario pVatScenario) =>
                        DeleteItem($"{VATSCENARIOS}/{pVatScenario.Id}");

        #endregion

        #region Warehouses

        public Task<List<Warehouse>> GetWarehousesAsync(string pIdCompany) =>
                GetItems<List<Warehouse>>($"{WAREHOUSES}/{pIdCompany}");

        public Task<Warehouse> AddWarehouseAsync(Warehouse pWarehouse) =>
                AddItem(pWarehouse, WAREHOUSES);

        public Task<Warehouse> EditWarehouseAsync(Warehouse pWarehouse) =>
                EditAndReturnItem(pWarehouse, $"{WAREHOUSES}/{pWarehouse.Id}");

        public Task<bool> DeleteWarehouseAsync(Warehouse pWarehouse) =>
               DeleteItem($"{WAREHOUSES}/{pWarehouse.Id}");

        #endregion

        #region Private methods

        private async Task<T> GetItems<T>(string pPath) 
        {
            return await _mB10WebWebApi.GetAsync<T>(pPath);
            //return await _mB10WebWebApi.GetAsyncList<T>(pPath);
        }

        private async Task<T> GetItem<T>(string pPath)
        {
            return await _mB10WebWebApi.GetAsync<T>(pPath);
        }

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