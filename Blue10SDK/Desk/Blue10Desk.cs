using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blue10SDK
{
    public class Blue10Desk : IBlue10Client
    {
        private IWebApiAdapter _mB10WebWebApi;
        
        public Blue10Desk(IWebApiAdapter pB10WebWebApi)
        {
            _mB10WebWebApi = pB10WebWebApi;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Auto, // for Array of basetype to deserialize polymorphic types..
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        #region Me
        /// <summary>
        /// Customer Name associated with api key used by this Blue10Desk
        /// <returns>Customer Name</returns>
        /// </summary>
        public string GetMe() =>
            SyncHelper.RunAsyncAsSync(() => GetItems<Me>(ME)).environment_name;
            
        
        #endregion

        #region AdministrationActions
        /// <summary>
        /// Returns all document actions which has to be processed
        /// </summary>
        public List<AdministrationAction> GetAdministrationActions() =>
                SyncHelper.RunAsyncAsSync(() => 
                    GetItems<List<AdministrationAction>>( ADMINISTRATIONACTIONS));
                

        /// <summary>
        /// Mark AdministrationAction as finished
        /// </summary>
        public bool FinishAdministrationAction(AdministrationAction pAdministrationAction)=>
            SyncHelper.RunAsyncAsSync(() => 
                    DeleteItem( $"{ADMINISTRATIONACTIONS}/{pAdministrationAction.id}"));
        
        #endregion

        #region Companies
        /// <summary>
        /// Returns all Blue10 Companies with ErpAdapter API
        /// </summary>
        public List<Company> GetCompanies() =>
            SyncHelper.RunAsyncAsSync(() => 
                GetItems<List<Company>>(COMPANIES));

        public Company UpdateCompany(Company pCompany)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates company information and returns company as saved in blue10
        /// </summary>
        public Company EditCompany(Company pCompany) =>
           SyncHelper.RunAsyncAsSync(() => 
                EditAndReturnItem( pCompany, $"{COMPANIES}/{pCompany.id}"));
        
        #endregion

        #region CostUnits
        /// <summary>
        /// Returns all CostUnit for company. Input is id from company
        /// </summary>
        public List<CostUnit> GetCostUnits(string pIdCompany)=>
            SyncHelper.RunAsyncAsSync(() => 
                GetItems<List<CostUnit>>($"{COSTUNITS}/{pIdCompany}"));

        /// <summary>
        /// Add CostUnit and returns CostUnit as saved in blue10
        /// </summary>
        public CostUnit AddCostUnit(CostUnit pCostUnit) =>
            SyncHelper.RunAsyncAsSync(() => 
                AddItem(pCostUnit, COSTUNITS));
        /// <summary>
        /// Updates CostUnit information and returns vendor as saved in blue10
        /// </summary>
        public CostUnit EditCostUnit(CostUnit pCostUnit)=>
            SyncHelper.RunAsyncAsSync(() => 
                EditAndReturnItem<CostUnit>(pCostUnit, $"{COSTUNITS}/{pCostUnit.id}"));
        
        /// <summary>
        /// Delete CostUnit from blue10, returns true if successful
        /// </summary>
        public bool DeleteCostUnit(CostUnit pCostUnit)=>
            SyncHelper.RunAsyncAsSync(() =>
               DeleteItem($"{COSTUNITS}/{pCostUnit.id}"));
        #endregion 

        #region CostCenters
        /// <summary>
        /// Returns all CostCenter for company. Input is id from company
        /// </summary>
        public List<CostCenter> GetCostCenters(string pIdCompany) =>
            SyncHelper.RunAsyncAsSync(() => 
                GetItems<List<CostCenter>>($"{COSTCENTERS}/{pIdCompany}"));

        /// <summary>
        /// Add CostCenter and returns CostCenter as saved in blue10
        /// </summary>
        public CostCenter AddCostCenter(CostCenter pCostCenter) =>
            SyncHelper.RunAsyncAsSync(() =>
                AddItem(pCostCenter, COSTCENTERS));
        
        /// <summary>
        /// Updates CostCenterr information and returns vendor as saved in blue10
        /// </summary>
        public CostCenter EditCostCenter(CostCenter pCostCenter) =>
            SyncHelper.RunAsyncAsSync(() => 
                EditAndReturnItem<CostCenter>(pCostCenter, $"{COSTCENTERS}/{pCostCenter.id}"));
            
        /// <summary>
        /// Delete CostCenter from blue10, returns true if successful
        /// </summary>
        public bool DeleteCostCenter(CostCenter pCostCenter) =>
            SyncHelper.RunAsyncAsSync(() => 
                DeleteItem($"{COSTCENTERS}/{pCostCenter.id}"));
            
        #endregion

        #region DocumentActions

        public List<DocumentAction> GetDocumentActions()=>
                SyncHelper.RunAsyncAsSync(() => 
                    GetItems<List<DocumentAction>>(DOCUMENTACTIONS));

        /// <summary>
        /// Updates Document action
        /// </summary>
        /// <returns>Okay?</returns>
        /// <param name="pDocumentAction"> new state of existing DocumentAction record</param>
        public string EditDocumentAction(DocumentAction pDocumentAction) =>
                SyncHelper.RunAsyncAsSync(() => 
                    EditItem<DocumentAction>( pDocumentAction, $"{DOCUMENTACTIONS}/{pDocumentAction.id}"));
                

        #endregion

        #region GLAccounts
        /// <summary>
        /// Returns all GLAccount for company. Input is id from company
        /// </summary>
        public List<GLAccount> GetGLAccounts(string pIdCompany) =>
            SyncHelper.RunAsyncAsSync(() => GetItems<List<GLAccount>>($"{GLACCOUNTS}/{pIdCompany}"));

        /// <summary>
        /// Add GLAccount and returns GLAccount as saved in blue10
        /// </summary>
        public GLAccount AddGLAccount(GLAccount pGLAccount) =>
            SyncHelper.RunAsyncAsSync(() =>
                AddItem(pGLAccount, GLACCOUNTS));
        
        /// <summary>
        /// Updates GLAccount information and returns vendor as saved in blue10
        /// </summary>
        public GLAccount EditGLAccount(GLAccount pGLAccount) =>
            SyncHelper.RunAsyncAsSync(() => 
                EditAndReturnItem<GLAccount>(pGLAccount, $"{GLACCOUNTS}/{pGLAccount.id}"));
        
        
        /// <summary>
        /// Delete GLAccount from blue10, returns true if successful
        /// </summary>
        public bool DeleteGLAccount(GLAccount pGLAccount) =>
            SyncHelper.RunAsyncAsSync(() => 
                DeleteItem( $"{GLACCOUNTS}/{pGLAccount.id}"));
        #endregion 

        #region DocumentActions

        public List<LogisticsDocumentAction> GetLogisticsDocumentActions() =>
            SyncHelper.RunAsyncAsSync(() => 
                GetItems<List<LogisticsDocumentAction>>(LOGISTICSDOCUMENTACTIONS));

        public string EditLogisticsDocumentAction(LogisticsDocumentAction pLogisticsDocumentAction) =>
            SyncHelper.RunAsyncAsSync(() => 
                EditItem(pLogisticsDocumentAction, $"{LOGISTICSDOCUMENTACTIONS}/{pLogisticsDocumentAction.id}"));

        #endregion

        #region PaymentTerms
        /// <summary>
        /// Returns all PaymentTerm for company. Input is id from company
        /// </summary>
        public List<PaymentTerm> GetPaymentTerms(string pIdCompany) =>
            SyncHelper.RunAsyncAsSync(() => 
                GetItems<List<PaymentTerm>>($"{PAYMENTTERMS}/{pIdCompany}"));

        /// <summary>
        /// Add PaymentTerm and returns PaymentTerm as saved in blue10
        /// </summary>
        public PaymentTerm AddPaymentTerm(PaymentTerm pPaymentTerm) =>
            SyncHelper.RunAsyncAsSync(() => 
                AddItem( pPaymentTerm, PAYMENTTERMS));
        
        /// <summary>
        /// Updates PaymentTerm information and returns PaymentTerm as saved in blue10
        /// </summary>
        public PaymentTerm EditPaymentTerm(PaymentTerm pPaymentTerm)=>
            SyncHelper.RunAsyncAsSync(() => 
                EditAndReturnItem( pPaymentTerm, $"{PAYMENTTERMS}/{pPaymentTerm.id}"));
        
        
        /// <summary>
        /// Delete PaymentTerm from blue10, returns true if successful
        /// </summary>
        public bool DeletePaymentTerm(PaymentTerm pPaymentTerm)=>
            SyncHelper.RunAsyncAsSync(() =>
                DeleteItem($"{PAYMENTTERMS}/{pPaymentTerm.id}"));
        #endregion 

        #region Projects
        /// <summary>
        /// Returns all Projects for company. Input is id from company
        /// </summary>
        public List<Project> GetProjects(string pIdCompany) =>
            SyncHelper.RunAsyncAsSync(() =>
                GetItems<List<Project>>($"{PROJECTS}/{pIdCompany}"));

        /// <summary>
        /// Add Project and returns Project as saved in blue10
        /// </summary>
        public Project AddProject(Project pProject) =>
            SyncHelper.RunAsyncAsSync(() =>
                AddItem(pProject, PROJECTS));
        
        /// <summary>
        /// Updates Project information and returns Project as saved in blue10
        /// </summary>
        public Project EditProject(Project pProject)=>
                SyncHelper.RunAsyncAsSync(() =>
                    EditAndReturnItem(pProject, $"{PROJECTS}/{pProject.id}"));
        
        /// <summary>
        /// Delete Project from blue10, returns true if successful
        /// </summary>
        public bool DeleteProject(Project pProject) =>
            SyncHelper.RunAsyncAsSync(() => 
                DeleteItem($"{PROJECTS}/{pProject.id}"));
        
        #endregion

        #region PurchaseInvoice
        /// <summary>
        /// Returns purchase invoice with blue10 id pId
        /// </summary>
        public PurchaseInvoice GetPurchaseInvoice(Guid pId)=>
                SyncHelper.RunAsyncAsSync(() =>
                    GetItems<PurchaseInvoice>($"{PURCHASEINVOICES}/{pId}"));

        /// <summary>
        /// Returns purchase invoice original (PDF) for purchase invoice with blue10 id pId
        /// </summary>

        public byte[] GetPurchaseInvoiceOriginal(Guid pId)
        {
            var fRet = SyncHelper.RunAsyncAsSync(() => 
                GetItems<DocumentOriginal>($"{PURCHASEINVOICES}/{pId}/documentoriginal"));
            return Base64Helper.GetBytesFromJsonResult(fRet.content);
        }

        public List<PurchaseInvoice> GetPurchaseInvoiceWithoutPaymentDate(string pIdCompany)=>
             SyncHelper.RunAsyncAsSync(() =>
                 GetItems<List<PurchaseInvoice>>($"{PURCHASEINVOICES}/?filter[payment_date]=null&filter[id_company]={pIdCompany}"));

        #endregion

        #region PurchaseOrder
        /// <summary>
        /// Returns all PurchaseOrders for company. Input is id from company
        /// </summary>
        public List<PurchaseOrder> GetPurchaseOrders(string pIdCompany)=>
            SyncHelper.RunAsyncAsSync(() =>
                GetItems<List<PurchaseOrder>>($"{PURCHASEORDERS}/{pIdCompany}"));

        /// <summary>
        /// Add PurchaseOrder and returns Project as saved in blue10
        /// </summary>
        public PurchaseOrder AddPurchaseOrder(PurchaseOrder pPurchaseOrder)=>
                SyncHelper.RunAsyncAsSync(() =>
                    AddItem<PurchaseOrder>(pPurchaseOrder, PURCHASEORDERS));
        
        /// <summary>
        /// Updates PurchaseOrder information and returns PurchaseOrder as saved in blue10
        /// </summary>
        public PurchaseOrder EditPurchaseOrder(PurchaseOrder pPurchaseOrder) =>
                SyncHelper.RunAsyncAsSync(() => 
                    EditAndReturnItem<PurchaseOrder>(pPurchaseOrder, $"{PURCHASEORDERS}/{pPurchaseOrder.id}"));
                    
        /// <summary>
        /// Delete PurchaseOrder from blue10, returns true if successful
        /// </summary>
        public bool DeletePurchaseOrder(PurchaseOrder pPurchaseOrder)=>
                SyncHelper.RunAsyncAsSync(() => 
                    DeleteItem($"{PURCHASEORDERS}/{pPurchaseOrder.id}"));
                
        #endregion 

        #region VatCodes
        /// <summary>
        /// Returns all VatCodes for company. Input is id from company
        /// </summary>
        public List<VatCode> GetVatCodes(string pIdCompany)=>
            SyncHelper.RunAsyncAsSync(() => GetItems<List<VatCode>>(
                $"{VATCODES}/{pIdCompany}"));

        /// <summary>
        /// Add VatCode and returns VatCode as saved in blue10
        /// </summary>
        public VatCode AddVatCode(VatCode pVatCode) =>
                SyncHelper.RunAsyncAsSync(() => AddItem( pVatCode, VATCODES));
        
        /// <summary>
        /// Updates VatCode information and returns VatCode as saved in blue10
        /// </summary>
        public VatCode EditVatCode(VatCode pVatCode)=>
                SyncHelper.RunAsyncAsSync(() =>
                    EditAndReturnItem( pVatCode, $"{VATCODES}/{pVatCode.id}"));
        /// <summary>
        /// Delete GLAccount from blue10, returns true if successful
        /// </summary>
        public bool DeleteVatCode(VatCode pVatCode)=>
            SyncHelper.RunAsyncAsSync(() => 
                    DeleteItem($"{VATCODES}/{pVatCode.id}"));
        
        #endregion 

        #region Vendors
        /// <summary>
        /// Returns all Vendors for company. Input is id from company
        /// </summary>
        public List<Vendor> GetVendors(string pIdCompany) =>
                SyncHelper.RunAsyncAsSync(() => 
                    GetItems<List<Vendor>>($"{VENDORS}/{pIdCompany}"));

        /// <summary>
        /// Add vendor and returns vendor as saved in blue10
        /// </summary>
        public Vendor AddVendor(Vendor pVendor)=>
                SyncHelper.RunAsyncAsSync(() => 
                    AddItem<Vendor>(pVendor, VENDORS));
        
        /// <summary>
        /// Updates vendor information and returns vendor as saved in blue10
        /// </summary>
        public Vendor EditVendor(Vendor pVendor) =>
            SyncHelper.RunAsyncAsSync(() => 
                EditAndReturnItem<Vendor>( pVendor, $"{VENDORS}/{pVendor.id}"));
            
        /// <summary>
        /// Delete vendor from blue10, returns true if successful
        /// </summary>
        public bool DeleteVendor(Vendor pVendor)=>
                SyncHelper.RunAsyncAsSync(() => 
                    DeleteItem($"{VENDORS}/{pVendor.id}"));

        #endregion 

        
        #region privates

        private async Task<T> GetItems<T>(string pPath) => 
            await _mB10WebWebApi.GetAsync<T>( pPath);
        
        private async Task<T> AddItem<T>(T pItem, string path) => 
            await _mB10WebWebApi.PostAsync( pItem, path);
        
        private async Task<T> EditAndReturnItem<T>(T pItem, string pUrl) =>
            await _mB10WebWebApi.PutAndReturnAsync(pItem, pUrl);
        
        private async Task<string> EditItem<T>( T pItem, string pUrl) =>
            await _mB10WebWebApi.PutAsync( pItem, pUrl);
        
        private async Task<bool> DeleteItem(string pUrl) =>
            await _mB10WebWebApi.DeleteAsync(pUrl);
    
    
        private const string  ADMINISTRATIONACTIONS = "administrationactions";
        private const string  COMPANIES = "companies";
        private const string  COSTCENTERS = "costcenters";
        private const string  COSTUNITS = "costunits";
        private const string  DOCUMENTACTIONS = "documentactions";
        private const string  DOCUMENTORIGINALS = "documentoriginals";
        private const string  GLACCOUNTS = "glaccounts";
        private const string  LOGISTICSDOCUMENTACTIONS = "logisticsdocumentactions";
        private const string  ME = "me";
        private const string  PAYMENTTERMS = "paymentterms";
        private const string  PROJECTS = "projects";
        private const string  PURCHASEINVOICES = "purchaseinvoices";
        private const string  PURCHASEORDERS = "purchaseorders";
        private const string  VATCODES = "vatcodes";
        private const string  VATSCENARIOS = "vatscenarios";
        private const string  VENDORS = "vendor";
        #endregion
    }
}
