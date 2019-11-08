
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blue10SDK.Models;

namespace Blue10SDK
{
    public interface IBlue10AsyncClient
    {
        /// <summary>
        /// Gets the environment name for the current API key (if valid).
        /// </summary>
        /// <returns>
        /// The environment name for the current API key.
        /// </returns>
        Task<Me> GetMe();

        /// <summary>
        /// Gets all actions for the administration.
        /// </summary>
        /// <returns>
        /// A <see cref="List{T}"/> with all <see cref="AdministrationAction"/>s for the administration.
        /// </returns>
        Task<List<AdministrationAction>> GetAdministrationActions();

        /// <summary>
        /// Marks an <see cref="AdministrationAction"/> as finished.
        /// </summary>
        /// <param name="pAdministrationAction">The <see cref="AdministrationAction"/> to mark as finished.</param>
        /// <returns>A boolean indicating if marking the <see cref="AdministrationAction"/> as finished was successful.</returns>
        Task<bool> FinishAdministrationAction(AdministrationAction pAdministrationAction);

        /// <summary>
        /// Returns all Blue10 Companies with ErpAdapter API
        /// </summary>
        Task<List<Company>> GetCompanies();

        /// <summary>
        /// Updates company record in blue10
        /// </summary>
        /// <param name="pCompany">The <see cref="Company"/> to update in blue10.</param>
        /// <returns>
        /// The <see cref="Company"/> as it is saved in blue10.
        /// </returns>
        Task<Company> UpdateCompany(Company pCompany);

        /// <summary>
        /// Get all <see cref="CostUnit"/>s for the specified company
        /// </summary>
        /// <param name="pIdCompany">Id of the <see cref="Company"/> to get <see cref="CostUnit"/>s for.</param>
        /// <returns>
        /// A <see cref="List{CostUnit}"/> containing all <see cref="CostUnit"/>s for the
        /// <see cref="Company"/> with id <paramref name="pIdCompany"/>.
        /// </returns>
        Task<List<CostUnit>> GetCostUnits(string pIdCompany);

        /// <summary>
        /// Add CostUnit and returns CostUnit as saved in blue10
        /// </summary>
        Task<CostUnit> AddCostUnit(CostUnit pCostUnit);

        /// <summary>
        /// Updates CostUnit information and returns vendor as saved in blue10
        /// </summary>
        Task<CostUnit> EditCostUnit(CostUnit pCostUnit);

        /// <summary>
        /// Delete CostUnit from blue10, returns true if successful
        /// </summary>
        Task<bool> DeleteCostUnit(CostUnit pCostUnit);

        /// <summary>
        /// Returns all CostCenter for company. Input is id from company
        /// </summary>
        Task<List<CostCenter>> GetCostCenters(string pIdCompany);

        /// <summary>
        /// Add CostCenter and returns CostCenter as saved in blue10
        /// </summary>
        Task<CostCenter> AddCostCenter(CostCenter pCostCenter);

        /// <summary>
        /// Updates CostCenterr information and returns vendor as saved in blue10
        /// </summary>
        Task<CostCenter> EditCostCenter(CostCenter pCostCenter);

        /// <summary>
        /// Delete CostCenter from blue10, returns true if successful
        /// </summary>
        Task<bool> DeleteCostCenter(CostCenter pCostCenter);

        Task<List<DocumentAction>> GetDocumentActions();

        /// <summary>
        /// Updates Document action
        /// </summary>
        /// <param name="pDocumentAction"> new state of existing DocumentAction record</param>
        /// <returns>Okay?</returns>
        Task<string> EditDocumentAction(DocumentAction pDocumentAction);

        /// <summary>
        /// Returns all GLAccount for company. Input is id from company
        /// </summary>
        Task<List<GLAccount>> GetGLAccounts(string pIdCompany);

        /// <summary>
        /// Add GLAccount and returns GLAccount as saved in blue10
        /// </summary>
        Task<GLAccount> AddGLAccount(GLAccount pGLAccount);

        /// <summary>
        /// Updates GLAccount information and returns vendor as saved in blue10
        /// </summary>
        Task<GLAccount> EditGLAccount(GLAccount pGLAccount);

        /// <summary>
        /// Delete GLAccount from blue10, returns true if successful
        /// </summary>
        Task<bool> DeleteGLAccount(GLAccount pGLAccount);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<LogisticsDocumentAction>> GetLogisticsDocumentActions();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pLogisticsDocumentAction"></param>
        /// <returns></returns>
        Task<string> EditLogisticsDocumentAction(LogisticsDocumentAction pLogisticsDocumentAction);

        /// <summary>
        /// Returns all PaymentTerm for company. Input is id from company
        /// </summary>
        Task<List<PaymentTerm>> GetPaymentTerms(string pIdCompany);

        /// <summary>
        /// Add PaymentTerm and returns PaymentTerm as saved in blue10
        /// </summary>
        Task<PaymentTerm> AddPaymentTerm(PaymentTerm pPaymentTerm);

        /// <summary>
        /// Updates PaymentTerm information and returns PaymentTerm as saved in blue10
        /// </summary>
        Task<PaymentTerm> EditPaymentTerm(PaymentTerm pPaymentTerm);

        /// <summary>
        /// Delete PaymentTerm from blue10, returns true if successful
        /// </summary>
        Task<bool> DeletePaymentTerm(PaymentTerm pPaymentTerm);

        /// <summary>
        /// Returns all Projects for company. Input is id from company
        /// </summary>
        Task<List<Project>> GetProjects(string pIdCompany);

        /// <summary>
        /// Add Project and returns Project as saved in blue10
        /// </summary>
        Task<Project> AddProject(Project pProject);

        /// <summary>
        /// Updates Project information and returns Project as saved in blue10
        /// </summary>
        Task<Project> EditProject(Project pProject);

        /// <summary>
        /// Delete Project from blue10, returns true if successful
        /// </summary>
        Task<bool> DeleteProject(Project pProject);

        /// <summary>
        /// Returns purchase invoice with blue10 id pId
        /// </summary>
        Task<PurchaseInvoice> GetPurchaseInvoice(Guid pId);

        /// <summary>
        /// Returns purchase invoice original (PDF) for purchase invoice with blue10 id pId
        /// </summary>
        Task<byte[]> GetPurchaseInvoiceOriginal(Guid pId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pIdCompany"></param>
        /// <returns></returns>
        Task<List<PurchaseInvoice>> GetPurchaseInvoiceWithoutPaymentDate(string pIdCompany);

        /// <summary>
        /// Returns all PurchaseOrders for company. Input is id from company
        /// </summary>
        Task<List<PurchaseOrder>> GetPurchaseOrders(string pIdCompany);

        /// <summary>
        /// Add PurchaseOrder and returns Project as saved in blue10
        /// </summary>
        Task<PurchaseOrder> AddPurchaseOrder(PurchaseOrder pPurchaseOrder);

        /// <summary>
        /// Updates PurchaseOrder information and returns PurchaseOrder as saved in blue10
        /// </summary>
        Task<PurchaseOrder> EditPurchaseOrder(PurchaseOrder pPurchaseOrder);

        /// <summary>
        /// Delete PurchaseOrder from blue10, returns true if successful
        /// </summary>
        Task<bool> DeletePurchaseOrder(PurchaseOrder pPurchaseOrder);

        /// <summary>
        /// Returns all VatCodes for company. Input is id from company
        /// </summary>
        Task<List<VatCode>> GetVatCodes(string pIdCompany);

        /// <summary>
        /// Add VatCode and returns VatCode as saved in blue10
        /// </summary>
        Task<VatCode> AddVatCode(VatCode pVatCode);

        /// <summary>
        /// Updates VatCode information and returns VatCode as saved in blue10
        /// </summary>
        Task<VatCode> EditVatCode(VatCode pVatCode);

        /// <summary>
        /// Delete GLAccount from blue10, returns true if successful
        /// </summary>
        Task<bool> DeleteVatCode(VatCode pVatCode);

        /// <summary>
        /// Returns all Vendors for company. Input is id from company
        /// </summary>
        Task<List<Vendor>> GetVendors(string pIdCompany);

        /// <summary>
        /// Add vendor and returns vendor as saved in blue10
        /// </summary>
        Task<Vendor> AddVendor(Vendor pVendor);

        /// <summary>
        /// Updates vendor information and returns vendor as saved in blue10
        /// </summary>
        Task<Vendor> EditVendor(Vendor pVendor);

        /// <summary>
        /// Delete vendor record from blue10
        ///
        /// <param name="pVendor">a </param> 
        /// <returns>
        /// true if successful
        /// </returns>
        /// </summary>
        Task<bool> DeleteVendor(Vendor pVendor);

        Task<Company> EditCompany(Company pCompany);
    }
}