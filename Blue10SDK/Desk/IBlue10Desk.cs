using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blue10SDK
{
    public interface IBlue10Desk
    {
      

        /// <summary>
        /// 
        /// <returns>
        /// Returns environment name of API key if valid
        /// </returns>
        /// </summary>
        string GetMe();

        /// <summary>
        /// Returns all document actions which has to be processed
        /// </summary>
        List<AdministrationAction> GetAdministrationActions();

        /// <summary>
        /// Mark AdministrationAction as finished
        /// </summary>
        bool FinishAdministrationAction(AdministrationAction pAdministrationAction);

        /// <summary>
        /// Returns all Blue10 Companies with ErpAdapter API
        /// </summary>
        List<Company> GetCompanies();

        /// <summary>
        /// Updates company record in blue10 
        ///
        /// <returns>
        ///  company as saved in blue10
        /// </returns>
        /// </summary>
        Company UpdateCompany(Company pCompany);

        /// <summary>
        /// Returns all CostUnit for company. Input is id from company
        /// </summary>
        List<CostUnit> GetCostUnits(string pIdCompany);

        /// <summary>
        /// Add CostUnit and returns CostUnit as saved in blue10
        /// </summary>
        CostUnit AddCostUnit(CostUnit pCostUnit);

        /// <summary>
        /// Updates CostUnit information and returns vendor as saved in blue10
        /// </summary>
        CostUnit EditCostUnit(CostUnit pCostUnit);

        /// <summary>
        /// Delete CostUnit from blue10, returns true if successful
        /// </summary>
        bool DeleteCostUnit(CostUnit pCostUnit);

        /// <summary>
        /// Returns all CostCenter for company. Input is id from company
        /// </summary>
        List<CostCenter> GetCostCenters(string pIdCompany);

        /// <summary>
        /// Add CostCenter and returns CostCenter as saved in blue10
        /// </summary>
        CostCenter AddCostCenter(CostCenter pCostCenter);

        /// <summary>
        /// Updates CostCenterr information and returns vendor as saved in blue10
        /// </summary>
        CostCenter EditCostCenter(CostCenter pCostCenter);

        /// <summary>
        /// Delete CostCenter from blue10, returns true if successful
        /// </summary>
        bool DeleteCostCenter(CostCenter pCostCenter);

        List<DocumentAction> GetDocumentActions();
        string EditDocumentAction(DocumentAction pDocumentAction);

        /// <summary>
        /// Returns all GLAccount for company. Input is id from company
        /// </summary>
        List<GLAccount> GetGLAccounts(string pIdCompany);

        /// <summary>
        /// Add GLAccount and returns GLAccount as saved in blue10
        /// </summary>
        GLAccount AddGLAccount(GLAccount pGLAccount);

        /// <summary>
        /// Updates GLAccount information and returns vendor as saved in blue10
        /// </summary>
        GLAccount EditGLAccount(GLAccount pGLAccount);

        /// <summary>
        /// Delete GLAccount from blue10, returns true if successful
        /// </summary>
        bool DeleteGLAccount(GLAccount pGLAccount);

        List<LogisticsDocumentAction> GetLogisticsDocumentActions();
        string EditLogisticsDocumentAction(LogisticsDocumentAction pLogisticsDocumentAction);

        /// <summary>
        /// Returns all PaymentTerm for company. Input is id from company
        /// </summary>
        List<PaymentTerm> GetPaymentTerms(string pIdCompany);

        /// <summary>
        /// Add PaymentTerm and returns PaymentTerm as saved in blue10
        /// </summary>
        PaymentTerm AddPaymentTerm(PaymentTerm pPaymentTerm);

        /// <summary>
        /// Updates PaymentTerm information and returns PaymentTerm as saved in blue10
        /// </summary>
        PaymentTerm EditPaymentTerm(PaymentTerm pPaymentTerm);

        /// <summary>
        /// Delete PaymentTerm from blue10, returns true if successful
        /// </summary>
        bool DeletePaymentTerm(PaymentTerm pPaymentTerm);

        /// <summary>
        /// Returns all Projects for company. Input is id from company
        /// </summary>
        List<Project> GetProjects(string pIdCompany);

        /// <summary>
        /// Add Project and returns Project as saved in blue10
        /// </summary>
        Project AddProject(Project pProject);

        /// <summary>
        /// Updates Project information and returns Project as saved in blue10
        /// </summary>
        Project EditProject(Project pProject);

        /// <summary>
        /// Delete Project from blue10, returns true if successful
        /// </summary>
        bool DeleteProject(Project pProject);

        /// <summary>
        /// Returns purchase invoice with blue10 id pId
        /// </summary>
        PurchaseInvoice GetPurchaseInvoice(Guid pId);

        /// <summary>
        /// Returns purchase invoice original (PDF) for purchase invoice with blue10 id pId
        /// </summary>
        byte[] GetPurchaseInvoiceOriginal(Guid pId);

        List<PurchaseInvoice> GetPurchaseInvoiceWithoutPaymentDate(string pIdCompany);

        /// <summary>
        /// Returns all PurchaseOrders for company. Input is id from company
        /// </summary>
        List<PurchaseOrder> GetPurchaseOrders(string pIdCompany);

        /// <summary>
        /// Add PurchaseOrder and returns Project as saved in blue10
        /// </summary>
        PurchaseOrder AddPurchaseOrder(PurchaseOrder pPurchaseOrder);

        /// <summary>
        /// Updates PurchaseOrder information and returns PurchaseOrder as saved in blue10
        /// </summary>
        PurchaseOrder EditPurchaseOrder(PurchaseOrder pPurchaseOrder);

        /// <summary>
        /// Delete PurchaseOrder from blue10, returns true if successful
        /// </summary>
        bool DeletePurchaseOrder(PurchaseOrder pPurchaseOrder);

        /// <summary>
        /// Returns all VatCodes for company. Input is id from company
        /// </summary>
        List<VatCode> GetVatCodes(string pIdCompany);

        /// <summary>
        /// Add VatCode and returns VatCode as saved in blue10
        /// </summary>
        VatCode AddVatCode(VatCode pVatCode);

        /// <summary>
        /// Updates VatCode information and returns VatCode as saved in blue10
        /// </summary>
        VatCode EditVatCode(VatCode pVatCode);

        /// <summary>
        /// Delete GLAccount from blue10, returns true if successful
        /// </summary>
        bool DeleteVatCode(VatCode pVatCode);

        /// <summary>
        /// Returns all Vendors for company. Input is id from company
        /// </summary>
        List<Vendor> GetVendors(string pIdCompany);

        /// <summary>
        /// Add vendor and returns vendor as saved in blue10
        /// </summary>
        Vendor AddVendor(Vendor pVendor);

        /// <summary>
        /// Updates vendor information and returns vendor as saved in blue10
        /// </summary>
        Vendor EditVendor(Vendor pVendor);

        /// <summary>
        /// Delete vendor record from blue10
        ///
        /// <param name="pVendor">a </param> 
        /// <returns>
        /// true if successful
        /// </returns>
        /// </summary>
        bool DeleteVendor(Vendor pVendor);

    }
}