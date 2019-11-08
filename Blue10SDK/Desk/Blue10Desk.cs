using System;
using System.Collections.Generic;
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
        private readonly IBlue10AsyncClient mBlue10Async;

        #endregion

        #region Constructors

        public Blue10Desk(IWebApiAdapter pB10WebWebApi)
        {
            _mB10WebWebApi = pB10WebWebApi;
            mBlue10Async = new Blue10AsyncDesk(pB10WebWebApi);
        }

        #endregion

        #region Me

        public string GetMe() => mBlue10Async.GetMe().Sync().EnvironmentName;
        #endregion

        #region AdministrationActions
        public List<AdministrationAction> GetAdministrationActions() => mBlue10Async.GetAdministrationActions().Sync();

        public bool FinishAdministrationAction(AdministrationAction pAdministrationAction) =>
            mBlue10Async.FinishAdministrationAction(pAdministrationAction).Sync();

        #endregion

        #region Companies

        public List<Company> GetCompanies() => mBlue10Async.GetCompanies().Sync();

        public Company UpdateCompany(Company pCompany) => mBlue10Async.UpdateCompany(pCompany).Sync();

        public Company EditCompany(Company pCompany) => mBlue10Async.EditCompany(pCompany).Sync();
               


        #endregion

        #region CostUnits
        public List<CostUnit> GetCostUnits(string pIdCompany) => mBlue10Async.GetCostUnits(pIdCompany).Sync();

        public CostUnit AddCostUnit(CostUnit pCostUnit) => mBlue10Async.AddCostUnit(pCostUnit).Sync();

        public CostUnit EditCostUnit(CostUnit pCostUnit) => mBlue10Async.EditCostUnit(pCostUnit).Sync();

        public bool DeleteCostUnit(CostUnit pCostUnit) => mBlue10Async.DeleteCostUnit(pCostUnit).Sync();

        #endregion 

        #region CostCenters

        public List<CostCenter> GetCostCenters(string pIdCompany) => mBlue10Async.GetCostCenters(pIdCompany).Sync();

        public CostCenter AddCostCenter(CostCenter pCostCenter) => mBlue10Async.AddCostCenter(pCostCenter).Sync();

        public CostCenter EditCostCenter(CostCenter pCostCenter) => mBlue10Async.EditCostCenter(pCostCenter).Sync();

        public bool DeleteCostCenter(CostCenter pCostCenter) => mBlue10Async.DeleteCostCenter(pCostCenter).Sync();

        #endregion

        #region DocumentActions
        public List<DocumentAction> GetDocumentActions() =>  mBlue10Async.GetDocumentActions().Sync();

        public string EditDocumentAction(DocumentAction pDocumentAction) =>
            mBlue10Async.EditDocumentAction(pDocumentAction).Sync();


        #endregion

        #region GLAccounts

        public List<GLAccount> GetGLAccounts(string pIdCompany) => mBlue10Async.GetGLAccounts(pIdCompany).Sync();


        public GLAccount AddGLAccount(GLAccount pGLAccount) => mBlue10Async.AddGLAccount(pGLAccount).Sync();


        public GLAccount EditGLAccount(GLAccount pGLAccount) => mBlue10Async.EditGLAccount(pGLAccount).Sync();


        public bool DeleteGLAccount(GLAccount pGLAccount) => mBlue10Async.DeleteGLAccount(pGLAccount).Sync();
        
        #endregion 

        #region DocumentActions

        public List<LogisticsDocumentAction> GetLogisticsDocumentActions() =>
            mBlue10Async.GetLogisticsDocumentActions().Sync();

        public string EditLogisticsDocumentAction(LogisticsDocumentAction pLogisticsDocumentAction) =>
            mBlue10Async.EditLogisticsDocumentAction(pLogisticsDocumentAction).Sync();

        #endregion

        #region PaymentTerms


        public List<PaymentTerm> GetPaymentTerms(string pIdCompany) =>
            mBlue10Async.GetPaymentTerms(pIdCompany).Sync();

        public PaymentTerm AddPaymentTerm(PaymentTerm pPaymentTerm) => mBlue10Async.AddPaymentTerm(pPaymentTerm).Sync();

        public PaymentTerm EditPaymentTerm(PaymentTerm pPaymentTerm) =>
            mBlue10Async.EditPaymentTerm(pPaymentTerm).Sync();


        public bool DeletePaymentTerm(PaymentTerm pPaymentTerm) => mBlue10Async.DeletePaymentTerm(pPaymentTerm).Sync();

        #endregion 

        #region Projects

        public List<Project> GetProjects(string pIdCompany) => mBlue10Async.GetProjects(pIdCompany).Sync();

        public Project AddProject(Project pProject) => mBlue10Async.AddProject(pProject).Sync();

        public Project EditProject(Project pProject) => mBlue10Async.EditProject(pProject).Sync();
        public bool DeleteProject(Project pProject) => mBlue10Async.DeleteProject(pProject).Sync();

        #endregion

        #region PurchaseInvoice

        public PurchaseInvoice GetPurchaseInvoice(Guid pId) => mBlue10Async.GetPurchaseInvoice(pId).Sync();

        public byte[] GetPurchaseInvoiceOriginal(Guid pId) => mBlue10Async.GetPurchaseInvoiceOriginal(pId).Sync();

        public List<PurchaseInvoice> GetPurchaseInvoiceWithoutPaymentDate(string pIdCompany) =>
            mBlue10Async.GetPurchaseInvoiceWithoutPaymentDate(pIdCompany).Sync();

        #endregion

        #region PurchaseOrder

        public List<PurchaseOrder> GetPurchaseOrders(string pIdCompany) =>
            mBlue10Async.GetPurchaseOrders(pIdCompany).Sync();

        public PurchaseOrder AddPurchaseOrder(PurchaseOrder pPurchaseOrder) =>
            mBlue10Async.AddPurchaseOrder(pPurchaseOrder).Sync();

        public PurchaseOrder EditPurchaseOrder(PurchaseOrder pPurchaseOrder) =>
            mBlue10Async.EditPurchaseOrder(pPurchaseOrder).Sync();

        public bool DeletePurchaseOrder(PurchaseOrder pPurchaseOrder) =>
            mBlue10Async.DeletePurchaseOrder(pPurchaseOrder).Sync();

        #endregion

        #region VatCodes

        public List<VatCode> GetVatCodes(string pIdCompany) =>
            mBlue10Async.GetVatCodes(pIdCompany).Sync();

        public VatCode AddVatCode(VatCode pVatCode) =>
            mBlue10Async.AddVatCode(pVatCode).Sync();

        public VatCode EditVatCode(VatCode pVatCode) => mBlue10Async.EditVatCode(pVatCode).Sync();

        public bool DeleteVatCode(VatCode pVatCode) => mBlue10Async.DeleteVatCode(pVatCode).Sync();

        #endregion

        #region Vendors

        public List<Vendor> GetVendors(string pIdCompany) => mBlue10Async.GetVendors(pIdCompany).Sync();

        public Vendor AddVendor(Vendor pVendor) => mBlue10Async.AddVendor(pVendor).Sync();

        public Vendor EditVendor(Vendor pVendor) => mBlue10Async.EditVendor(pVendor).Sync();

        public bool DeleteVendor(Vendor pVendor) => mBlue10Async.DeleteVendor(pVendor).Sync();

        #endregion
    }
}