using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blue10SDK
{
    public class Desk
    {
        IHttpClientFactory mHttpClientFactory;

        public Desk(IHttpClientFactory pHttpClientFactory)
        {
            mHttpClientFactory = pHttpClientFactory;

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Auto, // for Array of basetype to deserialize polymorphic types..
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        #region AdministrationActions
        /// <summary>
        /// Returns all document actions which has to be processed
        /// </summary>
        public List<AdministrationAction> GetAdministrationActions()
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.administrationactions}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => GetItems<List<AdministrationAction>>(fClient, fUrl));
                return fRet;
            }
        }

        /// <summary>
        /// Mark AdministrationAction as finished
        /// </summary>
        public bool FinishAdministrationAction(AdministrationAction pAdministrationAction)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.administrationactions}/{pAdministrationAction.id}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => DeleteItem(fClient, fUrl));
                return fRet;
            }
        }
        #endregion

        #region Companies
        /// <summary>
        /// Returns all Blue10 Companies with ErpAdapter API
        /// </summary>
        public List<Company> GetCompanies()
        {          
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.companies}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(()=> GetItems<List<Company>>(fClient, fUrl));
                return fRet;
            }
        }

        /// <summary>
        /// Updates company information and returns company as saved in blue10
        /// </summary>
        public Company EditCompany(Company pCompany)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.companies}/{pCompany.id}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => EditAndReturnItem<Company>(fClient, pCompany, fUrl));
                return fRet;
            }
        }
        #endregion

        #region CostUnits
        /// <summary>
        /// Returns all CostUnit for company. Input is id from company
        /// </summary>
        public List<CostUnit> GetCostUnits(string pIdCompany)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.costunits}/{pIdCompany}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => GetItems<List<CostUnit>>(fClient, fUrl));
                return fRet;
            }
        }

        /// <summary>
        /// Add CostUnit and returns CostUnit as saved in blue10
        /// </summary>
        public CostUnit AddCostUnit(CostUnit pCostUnit)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.costunits}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => AddItem<CostUnit>(fClient, pCostUnit, fUrl));
                return fRet;
            }
        }
        /// <summary>
        /// Updates CostUnit information and returns vendor as saved in blue10
        /// </summary>
        public CostUnit EditCostUnit(CostUnit pCostUnit)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.costunits}/{pCostUnit.id}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => EditAndReturnItem<CostUnit>(fClient, pCostUnit, fUrl));
                return fRet;
            }
        }
        /// <summary>
        /// Delete CostUnit from blue10, returns true if successful
        /// </summary>
        public bool DeleteCostUnit(CostUnit pCostUnit)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.costunits}/{pCostUnit.id}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => DeleteItem(fClient, fUrl));
                return fRet;
            }
        }
        #endregion 

        #region CostCenters
        /// <summary>
        /// Returns all CostCenter for company. Input is id from company
        /// </summary>
        public List<CostCenter> GetCostCenters(string pIdCompany)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.costcenters}/{pIdCompany}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => GetItems<List<CostCenter>>(fClient, fUrl));
                return fRet;
            }
        }

        /// <summary>
        /// Add CostCenter and returns CostCenter as saved in blue10
        /// </summary>
        public CostCenter AddCostCenter(CostCenter pCostCenter)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.costcenters}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => AddItem<CostCenter>(fClient, pCostCenter, fUrl));
                return fRet;
            }
        }
        /// <summary>
        /// Updates CostCenterr information and returns vendor as saved in blue10
        /// </summary>
        public CostCenter EditCostCenter(CostCenter pCostCenter)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.costcenters}/{pCostCenter.id}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => EditAndReturnItem<CostCenter>(fClient, pCostCenter, fUrl));
                return fRet;
            }
        }
        /// <summary>
        /// Delete CostCenter from blue10, returns true if successful
        /// </summary>
        public bool DeleteCostCenter(CostCenter pCostCenter)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.costcenters}/{pCostCenter.id}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => DeleteItem(fClient, fUrl));
                return fRet;
            }
        }
        #endregion

        #region DocumentActions

        public List<DocumentAction> GetDocumentActions()
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.documentactions}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => GetItems<List<DocumentAction>>(fClient, fUrl));
                return fRet;
            }
        }

        public string EditDocumentAction(DocumentAction pDocumentAction)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.documentactions}/{pDocumentAction.id}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => EditItem<DocumentAction>(fClient, pDocumentAction, fUrl));
                return fRet;
            }
        }

        #endregion

        #region GLAccounts
        /// <summary>
        /// Returns all GLAccount for company. Input is id from company
        /// </summary>
        public List<GLAccount> GetGLAccounts(string pIdCompany)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.glaccounts}/{pIdCompany}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => GetItems<List<GLAccount>>(fClient, fUrl));
                return fRet;
            }
        }

        /// <summary>
        /// Add GLAccount and returns GLAccount as saved in blue10
        /// </summary>
        public GLAccount AddGLAccount(GLAccount pGLAccount)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.glaccounts}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => AddItem<GLAccount>(fClient, pGLAccount, fUrl));
                return fRet;
            }
        }
        /// <summary>
        /// Updates GLAccount information and returns vendor as saved in blue10
        /// </summary>
        public GLAccount EditGLAccount(GLAccount pGLAccount)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.glaccounts}/{pGLAccount.id}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => EditAndReturnItem<GLAccount>(fClient, pGLAccount, fUrl));
                return fRet;
            }
        }
        /// <summary>
        /// Delete GLAccount from blue10, returns true if successful
        /// </summary>
        public bool DeleteGLAccount(GLAccount pGLAccount)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.glaccounts}/{pGLAccount.id}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => DeleteItem(fClient, fUrl));
                return fRet;
            }
        }
        #endregion 

        #region DocumentActions

        public List<LogisticsDocumentAction> GetLogisticsDocumentActions()
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.logisticsdocumentactions}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => GetItems<List<LogisticsDocumentAction>>(fClient, fUrl));
                return fRet;
            }
        }

        public string EditLogisticsDocumentAction(LogisticsDocumentAction pLogisticsDocumentAction)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.logisticsdocumentactions}/{pLogisticsDocumentAction.id}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => EditItem<LogisticsDocumentAction>(fClient, pLogisticsDocumentAction, fUrl));
                return fRet;
            }
        }

        #endregion

        #region PaymentTerms
        /// <summary>
        /// Returns all PaymentTerm for company. Input is id from company
        /// </summary>
        public List<PaymentTerm> GetPaymentTerms(string pIdCompany)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.paymentterms}/{pIdCompany}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => GetItems<List<PaymentTerm>>(fClient, fUrl));
                return fRet;
            }
        }

        /// <summary>
        /// Add PaymentTerm and returns PaymentTerm as saved in blue10
        /// </summary>
        public PaymentTerm AddPaymentTerm(PaymentTerm pPaymentTerm)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.paymentterms}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => AddItem<PaymentTerm>(fClient, pPaymentTerm, fUrl));
                return fRet;
            }
        }
        /// <summary>
        /// Updates PaymentTerm information and returns PaymentTerm as saved in blue10
        /// </summary>
        public PaymentTerm EditPaymentTerm(PaymentTerm pPaymentTerm)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.paymentterms}/{pPaymentTerm.id}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => EditAndReturnItem<PaymentTerm>(fClient, pPaymentTerm, fUrl));
                return fRet;
            }
        }
        /// <summary>
        /// Delete PaymentTerm from blue10, returns true if successful
        /// </summary>
        public bool DeletePaymentTerm(PaymentTerm pPaymentTerm)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.paymentterms}/{pPaymentTerm.id}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => DeleteItem(fClient, fUrl));
                return fRet;
            }
        }
        #endregion 

        #region Projects
        /// <summary>
        /// Returns all Projects for company. Input is id from company
        /// </summary>
        public List<Project> GetProjects(string pIdCompany)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.projects}/{pIdCompany}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => GetItems<List<Project>>(fClient, fUrl));
                return fRet;
            }
        }

        /// <summary>
        /// Add Project and returns Project as saved in blue10
        /// </summary>
        public Project AddProject(Project pProject)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.projects}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => AddItem<Project>(fClient, pProject, fUrl));
                return fRet;
            }
        }
        /// <summary>
        /// Updates Project information and returns Project as saved in blue10
        /// </summary>
        public Project EditProject(Project pProject)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.projects}/{pProject.id}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => EditAndReturnItem<Project>(fClient, pProject, fUrl));
                return fRet;
            }
        }
        /// <summary>
        /// Delete Project from blue10, returns true if successful
        /// </summary>
        public bool DeleteProject(Project pProject)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.projects}/{pProject.id}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => DeleteItem(fClient, fUrl));
                return fRet;
            }
        }
        #endregion

        #region PurchaseInvoice
        /// <summary>
        /// Returns purchase invoice with blue10 id pId
        /// </summary>
        public PurchaseInvoice GetPurchaseInvoice(Guid pId)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.purchaseinvoices}/{pId}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => GetItems<PurchaseInvoice>(fClient, fUrl));
                return fRet;
            }
        }

        /// <summary>
        /// Returns purchase invoice original (PDF) for purchase invoice with blue10 id pId
        /// </summary>

        public byte[] GetPurchaseInvoiceOriginal(Guid pId)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.purchaseinvoices}/{pId}/documentoriginal";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => GetItems<DocumentOriginal>(fClient, fUrl));
                return Base64Helper.GetBytesFromJsonResult(fRet.content);
            }
        }

        public List<PurchaseInvoice> GetPurchaseInvoiceWithoutPaymentdate(string pIdCompany)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.purchaseinvoices}/?filter[payment_date]=null&filter[id_company]={pIdCompany}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => GetItems<List<PurchaseInvoice>>(fClient, fUrl));
                return fRet;
            }
        }

        #endregion

        #region PurchaseOrder
        /// <summary>
        /// Returns all PurchaseOrders for company. Input is id from company
        /// </summary>
        public List<PurchaseOrder> GetPurchaseOrders(string pIdCompany)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.purchaseorders}/{pIdCompany}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => GetItems<List<PurchaseOrder>>(fClient, fUrl));
                return fRet;
            }
        }

        /// <summary>
        /// Add PurchaseOrder and returns Project as saved in blue10
        /// </summary>
        public PurchaseOrder AddPurchaseOrder(PurchaseOrder pPurchaseOrder)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.purchaseorders}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => AddItem<PurchaseOrder>(fClient, pPurchaseOrder, fUrl));
                return fRet;
            }
        }
        /// <summary>
        /// Updates PurchaseOrder information and returns PurchaseOrder as saved in blue10
        /// </summary>
        public PurchaseOrder EditPurchaseOrder(PurchaseOrder pPurchaseOrder)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.purchaseorders}/{pPurchaseOrder.id}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => EditAndReturnItem<PurchaseOrder>(fClient, pPurchaseOrder, fUrl));
                return fRet;
            }
        }
        /// <summary>
        /// Delete PurchaseOrder from blue10, returns true if successful
        /// </summary>
        public bool DeletePurchaseOrder(PurchaseOrder pPurchaseOrder)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.purchaseorders}/{pPurchaseOrder.id}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => DeleteItem(fClient, fUrl));
                return fRet;
            }
        }
        #endregion 

        #region VatCodes
        /// <summary>
        /// Returns all VatCodes for company. Input is id from company
        /// </summary>
        public List<VatCode> GetVatCodes(string pIdCompany)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.vatcodes}/{pIdCompany}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => GetItems<List<VatCode>>(fClient, fUrl));
                return fRet;
            }
        }

        /// <summary>
        /// Add VatCode and returns VatCode as saved in blue10
        /// </summary>
        public VatCode AddVatCode(VatCode pVatCode)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.vatcodes}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => AddItem<VatCode>(fClient, pVatCode, fUrl));
                return fRet;
            }
        }
        /// <summary>
        /// Updates VatCode information and returns VatCode as saved in blue10
        /// </summary>
        public VatCode EditVatCode(VatCode pVatCode)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.vatcodes}/{pVatCode.id}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => EditAndReturnItem<VatCode>(fClient, pVatCode, fUrl));
                return fRet;
            }
        }
        /// <summary>
        /// Delete GLAccount from blue10, returns true if successful
        /// </summary>
        public bool DeleteVatCode(VatCode pVatCode)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.vatcodes}/{pVatCode.id}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => DeleteItem(fClient, fUrl));
                return fRet;
            }
        }
        #endregion 

        #region Vendors
        /// <summary>
        /// Returns all Vendors for company. Input is id from company
        /// </summary>
        public List<Vendor> GetVendors(string pIdCompany)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.vendors}/{pIdCompany}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => GetItems<List<Vendor>>(fClient, fUrl));
                return fRet;
            }
        }

        /// <summary>
        /// Add vendor and returns vendor as saved in blue10
        /// </summary>
        public Vendor AddVendor(Vendor pVendor)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.vendors}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => AddItem<Vendor>(fClient, pVendor, fUrl));
                return fRet;
            }
        }
        /// <summary>
        /// Updates vendor information and returns vendor as saved in blue10
        /// </summary>
        public Vendor EditVendor(Vendor pVendor)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.vendors}/{pVendor.id}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => EditAndReturnItem<Vendor>(fClient, pVendor, fUrl));
                return fRet;
            }
        }
        /// <summary>
        /// Delete vendor from blue10, returns true if successful
        /// </summary>
        public bool DeleteVendor(Vendor pVendor)
        {
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}{B10Endpoint.vendors}/{pVendor.id}";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => DeleteItem(fClient, fUrl));
                return fRet;
            }
        }

        #endregion 

        private async Task<T> GetItems<T>(HttpClient pClient, string pUrl) =>
            await Blue10ApiHelper.GetAsync<T>(pClient, pUrl);
        private async Task<T> AddItem<T>(HttpClient pClient, T pItem, string pUrl) =>
            await Blue10ApiHelper.PostAsync<T>(pClient, pItem, pUrl);
        private async Task<T> EditAndReturnItem<T>(HttpClient pClient, T pItem, string pUrl) =>
            await Blue10ApiHelper.PutAndReturnAsync<T>(pClient, pItem, pUrl);
        private async Task<string> EditItem<T>(HttpClient pClient, T pItem, string pUrl) =>
            await Blue10ApiHelper.PutAsync<T>(pClient, pItem, pUrl);
        private async Task<bool> DeleteItem(HttpClient pClient, string pUrl) =>
            await Blue10ApiHelper.DeleteAsync(pClient, pUrl);
    }
}
