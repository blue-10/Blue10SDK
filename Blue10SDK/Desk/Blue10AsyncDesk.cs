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

        public Blue10AsyncDesk(IWebApiAdapter pB10WebWebApi)
        {
            _mB10WebWebApi = pB10WebWebApi;
        }

        #endregion

        #region Me

        public Task<Me> GetMe() =>
                GetItems<Me>(ME);

        public Task<List<AdministrationAction>> GetAdministrationActions() => 
                GetItems<List<AdministrationAction>>(ADMINISTRATIONACTIONS);

        #endregion

        #region AdministrationActions

        public Task<List<AdministrationAction>> GetAdministrationActionsAsync() => 
            GetItems<List<AdministrationAction>>(ADMINISTRATIONACTIONS);

        public Task<bool> FinishAdministrationAction(AdministrationAction pAdministrationAction) =>
                    DeleteItem($"{ADMINISTRATIONACTIONS}/{pAdministrationAction.Id}");

        #endregion

        #region Companies

        public Task<List<Company>> GetCompanies() =>
                GetItems<List<Company>>(COMPANIES);

        public Task<Company> UpdateCompany(Company pCompany)
        {
                throw new NotImplementedException();
        }

        #endregion

        #region CostUnits

        public Task<List<CostUnit>> GetCostUnits(string pIdCompany) =>
                GetItems<List<CostUnit>>($"{COSTUNITS}/{pIdCompany}");

        public Task<CostUnit> AddCostUnit(CostUnit pCostUnit) =>
                AddItem(pCostUnit, COSTUNITS);

        public Task<CostUnit> EditCostUnit(CostUnit pCostUnit) =>
                EditAndReturnItem(pCostUnit, $"{COSTUNITS}/{pCostUnit.Id}");


        public Task<bool> DeleteCostUnit(CostUnit pCostUnit) =>
               DeleteItem($"{COSTUNITS}/{pCostUnit.Id}");

        #endregion 

        #region CostCenters

        public Task<List<CostCenter>> GetCostCenters(string pIdCompany) =>
                GetItems<List<CostCenter>>($"{COSTCENTERS}/{pIdCompany}");

        public Task<CostCenter> AddCostCenter(CostCenter pCostCenter) =>
                AddItem(pCostCenter, COSTCENTERS);

        public Task<CostCenter> EditCostCenter(CostCenter pCostCenter) =>
                EditAndReturnItem(pCostCenter, $"{COSTCENTERS}/{pCostCenter.Id}");

        public Task<bool> DeleteCostCenter(CostCenter pCostCenter) =>
                DeleteItem($"{COSTCENTERS}/{pCostCenter.Id}");

        #endregion

        #region DocumentActions

        public Task<List<DocumentAction>> GetDocumentActions() =>
                    GetItems<List<DocumentAction>>(DOCUMENTACTIONS);

        public Task<string> EditDocumentAction(DocumentAction pDocumentAction) =>
                    EditItem(pDocumentAction, $"{DOCUMENTACTIONS}/{pDocumentAction.Id}");


        #endregion

        #region GLAccounts


        public Task<List<GLAccount>> GetGLAccounts(string pIdCompany) =>
             GetItems<List<GLAccount>>($"{GLACCOUNTS}/{pIdCompany}");


        public Task<GLAccount> AddGLAccount(GLAccount pGLAccount) =>
                AddItem(pGLAccount, GLACCOUNTS);


        public Task<GLAccount> EditGLAccount(GLAccount pGLAccount) =>
                EditAndReturnItem(pGLAccount, $"{GLACCOUNTS}/{pGLAccount.Id}");


        public Task<bool> DeleteGLAccount(GLAccount pGLAccount) =>
                DeleteItem($"{GLACCOUNTS}/{pGLAccount.Id}");
        
        
        
        #endregion 

        #region DocumentActions

        public Task<List<LogisticsDocumentAction>> GetLogisticsDocumentActions() =>
                GetItems<List<LogisticsDocumentAction>>(LOGISTICSDOCUMENTACTIONS);

        public Task<string> EditLogisticsDocumentAction(LogisticsDocumentAction pLogisticsDocumentAction) =>
                EditItem(pLogisticsDocumentAction, $"{LOGISTICSDOCUMENTACTIONS}/{pLogisticsDocumentAction.Id}");

        #endregion

        #region PaymentTerms

 
        public Task<List<PaymentTerm>> GetPaymentTerms(string pIdCompany) =>
                GetItems<List<PaymentTerm>>($"{PAYMENTTERMS}/{pIdCompany}");

        public Task<PaymentTerm> AddPaymentTerm(PaymentTerm pPaymentTerm) =>
                AddItem(pPaymentTerm, PAYMENTTERMS);

        public Task<PaymentTerm> EditPaymentTerm(PaymentTerm pPaymentTerm) =>
                EditAndReturnItem(pPaymentTerm, $"{PAYMENTTERMS}/{pPaymentTerm.Id}");


        public Task<bool> DeletePaymentTerm(PaymentTerm pPaymentTerm) =>
                DeleteItem($"{PAYMENTTERMS}/{pPaymentTerm.Id}");

        #endregion 

        #region Projects

        public Task<List<Project>> GetProjects(string pIdCompany) =>
                GetItems<List<Project>>($"{PROJECTS}/{pIdCompany}");

        public Task<Project> AddProject(Project pProject) =>
                AddItem(pProject, PROJECTS);

        public Task<Project> EditProject(Project pProject) =>
                    EditAndReturnItem(pProject, $"{PROJECTS}/{pProject.Id}");

        public Task<bool> DeleteProject(Project pProject) =>
                DeleteItem($"{PROJECTS}/{pProject.Id}");
        
        #endregion

        #region PurchaseInvoice

        public Task<PurchaseInvoice> GetPurchaseInvoice(Guid pId) =>
                    GetItems<PurchaseInvoice>($"{PURCHASEINVOICES}/{pId}");

        public async Task<byte[]> GetPurchaseInvoiceOriginal(Guid pId)
        {
            var fRet = await GetItems<DocumentOriginal>($"{PURCHASEINVOICES}/{pId}/documentoriginal");
            return Base64Helper.GetBytesFromJsonResult(fRet.Content);
        }

        public Task<List<PurchaseInvoice>> GetPurchaseInvoiceWithoutPaymentDate(string pIdCompany) =>
                 GetItems<List<PurchaseInvoice>>($"{PURCHASEINVOICES}/?filter[payment_date]=null&filter[id_company]={pIdCompany}");

        #endregion

        #region PurchaseOrder

        public Task<List<PurchaseOrder>> GetPurchaseOrders(string pIdCompany) =>
                GetItems<List<PurchaseOrder>>($"{PURCHASEORDERS}/{pIdCompany}");

        public Task<PurchaseOrder> AddPurchaseOrder(PurchaseOrder pPurchaseOrder) =>
                    AddItem(pPurchaseOrder, PURCHASEORDERS);

        public Task<PurchaseOrder> EditPurchaseOrder(PurchaseOrder pPurchaseOrder) =>
                    EditAndReturnItem(pPurchaseOrder, $"{PURCHASEORDERS}/{pPurchaseOrder.Id}");


        public Task<bool> DeletePurchaseOrder(PurchaseOrder pPurchaseOrder) =>
                    DeleteItem($"{PURCHASEORDERS}/{pPurchaseOrder.Id}");

        #endregion

        #region VatCodes

        public Task<List<VatCode>> GetVatCodes(string pIdCompany) =>
             GetItems<List<VatCode>>($"{VATCODES}/{pIdCompany}");

        public Task<VatCode> AddVatCode(VatCode pVatCode) =>
                 AddItem(pVatCode, VATCODES);

        public Task<VatCode> EditVatCode(VatCode pVatCode) =>
                    EditAndReturnItem(pVatCode, $"{VATCODES}/{pVatCode.Id}");

        public Task<bool> DeleteVatCode(VatCode pVatCode) =>
                DeleteItem($"{VATCODES}/{pVatCode.Id}");

        #endregion

        #region Vendors

        public Task<List<Vendor>> GetVendors(string pIdCompany) =>
                    GetItems<List<Vendor>>($"{VENDORS}/{pIdCompany}");
        
        public Task<Vendor> AddVendor(Vendor pVendor) =>
                    AddItem(pVendor, VENDORS);

        public Task<Vendor> EditVendor(Vendor pVendor) =>
                EditAndReturnItem(pVendor, $"{VENDORS}/{pVendor.Id}");

        public Task<bool> DeleteVendor(Vendor pVendor) =>
                DeleteItem($"{VENDORS}/{pVendor.Id}");

        public Task<Company> EditCompany(Company pCompany) =>
                EditAndReturnItem(pCompany, $"{COMPANIES}/{pCompany.Id}");

        #endregion
        
        #region Private methods

        private Task<T> GetItems<T>(string pPath) =>
            _mB10WebWebApi.GetAsync<T>(pPath);

        private Task<T> AddItem<T>(T pItem, string path) =>
            _mB10WebWebApi.PostAsync(pItem, path);

        private Task<T> EditAndReturnItem<T>(T pItem, string pUrl) =>
            _mB10WebWebApi.PutAndReturnAsync(pItem, pUrl);

        private Task<string> EditItem<T>(T pItem, string pUrl) =>
            _mB10WebWebApi.PutAsync(pItem, pUrl);

        private Task<bool> DeleteItem(string pUrl) =>
            _mB10WebWebApi.DeleteAsync(pUrl);

        #endregion
    }
}