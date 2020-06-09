using System;
using System.Collections.Generic;
using Blue10SDK.Models;

namespace Blue10SDK
{
    public class Blue10Desk : IBlue10Client
    {
        private readonly IBlue10AsyncClient mBlue10Async;
        public Blue10Desk(IBlue10AsyncClient pB10Async)
        {
            mBlue10Async = pB10Async;
        }


        #region Me
        public string GetMe() => mBlue10Async.GetMeAsync().Sync().EnvironmentName;
        #endregion

        #region AdministrationActions
        public List<AdministrationAction> GetAdministrationActions() => mBlue10Async.GetAdministrationActionsAsync().Sync();

        public bool FinishAdministrationAction(AdministrationAction pAdministrationAction) =>
            mBlue10Async.FinishAdministrationActionAsync(pAdministrationAction).Sync();

        #endregion

        #region Articles
        public List<Article> GetArticles(string pIdCompany) => mBlue10Async.GetArticlesAsync(pIdCompany).Sync();

        public Article AddArticle(Article pArticle) => mBlue10Async.AddArticleAsync(pArticle).Sync();

        public Article EditArticle(Article pArticle) => mBlue10Async.EditArticleAsync(pArticle).Sync();

        public bool DeleteArticle(Article pArticle) => mBlue10Async.DeleteArticleAsync(pArticle).Sync();

        #endregion 

        #region Companies

        public List<Company> GetCompanies() => mBlue10Async.GetCompaniesAsync().Sync();

        
        public Company UpdateCompany(Company pCompany) => mBlue10Async.UpdateCompanyAsync(pCompany).Sync();


        #endregion

        #region CostUnits
        public List<CostUnit> GetCostUnits(string pIdCompany) => mBlue10Async.GetCostUnitsAsync(pIdCompany).Sync();

        public CostUnit AddCostUnit(CostUnit pCostUnit) => mBlue10Async.AddCostUnitAsync(pCostUnit).Sync();

        public CostUnit EditCostUnit(CostUnit pCostUnit) => mBlue10Async.EditCostUnitAsync(pCostUnit).Sync();

        public bool DeleteCostUnit(CostUnit pCostUnit) => mBlue10Async.DeleteCostUnitAsync(pCostUnit).Sync();

        #endregion 

        #region CostCenters

        public List<CostCenter> GetCostCenters(string pIdCompany) => mBlue10Async.GetCostCentersAsync(pIdCompany).Sync();

        public CostCenter AddCostCenter(CostCenter pCostCenter) => mBlue10Async.AddCostCenterAsync(pCostCenter).Sync();

        public CostCenter EditCostCenter(CostCenter pCostCenter) => mBlue10Async.EditCostCenterAsync(pCostCenter).Sync();

        public bool DeleteCostCenter(CostCenter pCostCenter) => mBlue10Async.DeleteCostCenterAsync(pCostCenter).Sync();

        #endregion

        #region DocumentActions
        public List<DocumentAction> GetDocumentActions() =>  mBlue10Async.GetDocumentActionsAsync().Sync();

        public string EditDocumentAction(DocumentAction pDocumentAction) =>
            mBlue10Async.EditDocumentActionAsync(pDocumentAction).Sync();


        #endregion

        #region GLAccounts

        public List<GLAccount> GetGLAccounts(string pIdCompany) => mBlue10Async.GetGLAccountsAsync(pIdCompany).Sync();


        public GLAccount AddGLAccount(GLAccount pGLAccount) => mBlue10Async.AddGLAccountAsync(pGLAccount).Sync();


        public GLAccount EditGLAccount(GLAccount pGLAccount) => mBlue10Async.EditGLAccountAsync(pGLAccount).Sync();


        public bool DeleteGLAccount(GLAccount pGLAccount) => mBlue10Async.DeleteGLAccountAsync(pGLAccount).Sync();
        
        #endregion 

        #region DocumentActions

        public List<LogisticsDocumentAction> GetLogisticsDocumentActions() =>
            mBlue10Async.GetLogisticsDocumentActionsAsync().Sync();

        public string EditLogisticsDocumentAction(LogisticsDocumentAction pLogisticsDocumentAction) =>
            mBlue10Async.EditLogisticsDocumentActionAsync(pLogisticsDocumentAction).Sync();

        #endregion

        #region PaymentTerms


        public List<PaymentTerm> GetPaymentTerms(string pIdCompany) =>
            mBlue10Async.GetPaymentTermsAsync(pIdCompany).Sync();

        public PaymentTerm AddPaymentTerm(PaymentTerm pPaymentTerm) => mBlue10Async.AddPaymentTermAsync(pPaymentTerm).Sync();

        public PaymentTerm EditPaymentTerm(PaymentTerm pPaymentTerm) =>
            mBlue10Async.EditPaymentTermAsync(pPaymentTerm).Sync();


        public bool DeletePaymentTerm(PaymentTerm pPaymentTerm) => mBlue10Async.DeletePaymentTermAsync(pPaymentTerm).Sync();

        #endregion 

        #region Projects

        public List<Project> GetProjects(string pIdCompany) => mBlue10Async.GetProjectsAsync(pIdCompany).Sync();

        public Project AddProject(Project pProject) => mBlue10Async.AddProjectAsync(pProject).Sync();

        public Project EditProject(Project pProject) => mBlue10Async.EditProjectAsync(pProject).Sync();
        public bool DeleteProject(Project pProject) => mBlue10Async.DeleteProjectAsync(pProject).Sync();

        #endregion

        #region PurchaseInvoice

        public PurchaseInvoice GetPurchaseInvoice(Guid pId) => mBlue10Async.GetPurchaseInvoiceAsync(pId).Sync();

        public byte[] GetPurchaseInvoiceOriginal(Guid pId) => mBlue10Async.GetPurchaseInvoiceOriginalAsync(pId).Sync();

        public List<PurchaseInvoice> GetPurchaseInvoiceWithoutPaymentDate(string pIdCompany) =>
            mBlue10Async.GetPurchaseInvoiceWithoutPaymentDateAsync(pIdCompany).Sync();

        public List<PurchaseInvoice> GetPurchaseInvoiceWithoutPaymentDateA(string pIdCompany) =>
            mBlue10Async.GetPurchaseInvoiceWithoutPaymentDateAsync(pIdCompany).Sync();

        #endregion

        #region PurchaseOrder

        public List<PurchaseOrder> GetPurchaseOrders(string pIdCompany) =>
            mBlue10Async.GetPurchaseOrdersAsync(pIdCompany).Sync();

        public PurchaseOrder AddPurchaseOrder(PurchaseOrder pPurchaseOrder) =>
            mBlue10Async.AddPurchaseOrderAsync(pPurchaseOrder).Sync();

        public PurchaseOrder EditPurchaseOrder(PurchaseOrder pPurchaseOrder) =>
            mBlue10Async.EditPurchaseOrderAsync(pPurchaseOrder).Sync();

        public bool DeletePurchaseOrder(PurchaseOrder pPurchaseOrder) =>
            mBlue10Async.DeletePurchaseOrderAsync(pPurchaseOrder).Sync();

        #endregion

        #region VatCodes

        public List<VatCode> GetVatCodes(string pIdCompany) =>
            mBlue10Async.GetVatCodesAsync(pIdCompany).Sync();

        public VatCode AddVatCode(VatCode pVatCode) =>
            mBlue10Async.AddVatCodeAsync(pVatCode).Sync();

        public VatCode EditVatCode(VatCode pVatCode) => mBlue10Async.EditVatCodeAsync(pVatCode).Sync();

        public bool DeleteVatCode(VatCode pVatCode) => mBlue10Async.DeleteVatCodeAsync(pVatCode).Sync();

        public List<VatScenario> GetVatScenarios(string pIdCompany) => mBlue10Async.GetVatScenariosAsync(pIdCompany).Sync();

        public VatScenario AddVatScenario(VatScenario pVatScenario) => mBlue10Async.AddVatScenarioAsync(pVatScenario).Sync();

        public VatScenario EditVatScenario(VatScenario pVatScenario) => mBlue10Async.EditVatScenarioAsync(pVatScenario).Sync();

        public bool DeleteVatScenario(VatScenario pVatScenario) => mBlue10Async.DeleteVatScenarioAsync(pVatScenario).Sync();
        

        #endregion

        #region Vendors

        public List<Vendor> GetVendors(string pIdCompany) => mBlue10Async.GetVendorsAsync(pIdCompany).Sync();

        public Vendor AddVendor(Vendor pVendor) => mBlue10Async.AddVendorAsync(pVendor).Sync();

        public Vendor EditVendor(Vendor pVendor) => mBlue10Async.EditVendorAsync(pVendor).Sync();

        public bool DeleteVendor(Vendor pVendor) => mBlue10Async.DeleteVendorAsync(pVendor).Sync();

        #endregion
    }
}