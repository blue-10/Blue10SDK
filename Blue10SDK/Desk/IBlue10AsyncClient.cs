
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
        Task<Me> GetMeAsync();

        /// <summary>
        /// Gets all actions for the administration.
        /// </summary>
        /// <returns>
        /// A <see cref="List{T}"/> with all <see cref="AdministrationAction"/>s for the administration.
        /// </returns>
        Task<List<AdministrationAction>> GetAdministrationActionsAsync();

        /// <summary>
        /// Marks an <see cref="AdministrationAction"/> as finished.
        /// </summary>
        /// <param name="pAdministrationAction">The <see cref="AdministrationAction"/> to mark as finished.</param>
        /// <returns>A boolean indicating if marking the <see cref="AdministrationAction"/> as finished was successful.</returns>
        Task<bool> FinishAdministrationActionAsync(AdministrationAction pAdministrationAction);

        /// <summary>
        /// Get all <see cref="Article"/>s for the specified company
        /// </summary>
        /// <param name="pIdCompany">Id of the <see cref="Company"/> to get <see cref="Article"/>s for.</param>
        /// <returns>
        /// A <see cref="List{Article}"/> containing all <see cref="Article"/>s for the
        /// <see cref="Company"/> with id <paramref name="pIdCompany"/>.
        /// </returns>
        Task<List<Article>> GetArticlesAsync(string pIdCompany);

        /// <summary>
        /// Add Article and returns Article as saved in blue10
        /// </summary>
        Task<Article> AddArticleAsync(Article pArticle);

        /// <summary>
        /// Updates Article information and returns Article as saved in blue10
        /// </summary>
        Task<Article> EditArticleAsync(Article pArticle);

        /// <summary>
        /// Delete Article from blue10, returns true if successful
        /// </summary>
        Task<bool> DeleteArticleAsync(Article pArticle);

        /// <summary>
        /// Returns all Blue10 Companies with ErpAdapter API
        /// </summary>
        Task<List<Company>> GetCompaniesAsync();

        /// <summary>
        /// Updates company record in blue10
        /// </summary>
        /// <param name="pCompany">The <see cref="Company"/> to update in blue10.</param>
        /// <returns>
        /// The <see cref="Company"/> as it is saved in blue10.
        /// </returns>
        Task<Company> UpdateCompanyAsync(Company pCompany);

        /// <summary>
        /// Get all <see cref="CostUnit"/>s for the specified company
        /// </summary>
        /// <param name="pIdCompany">Id of the <see cref="Company"/> to get <see cref="CostUnit"/>s for.</param>
        /// <returns>
        /// A <see cref="List{CostUnit}"/> containing all <see cref="CostUnit"/>s for the
        /// <see cref="Company"/> with id <paramref name="pIdCompany"/>.
        /// </returns>
        Task<List<CostUnit>> GetCostUnitsAsync(string pIdCompany);

        /// <summary>
        /// Add CostUnit and returns CostUnit as saved in blue10
        /// </summary>
        Task<CostUnit> AddCostUnitAsync(CostUnit pCostUnit);

        /// <summary>
        /// Updates CostUnit information and returns vendor as saved in blue10
        /// </summary>
        Task<CostUnit> EditCostUnitAsync(CostUnit pCostUnit);

        /// <summary>
        /// Delete CostUnit from blue10, returns true if successful
        /// </summary>
        Task<bool> DeleteCostUnitAsync(CostUnit pCostUnit);

        /// <summary>
        /// Returns all CostCenter for company. Input is id from company
        /// </summary>
        Task<List<CostCenter>> GetCostCentersAsync(string pIdCompany);

        /// <summary>
        /// Add CostCenter and returns CostCenter as saved in blue10
        /// </summary>
        Task<CostCenter> AddCostCenterAsync(CostCenter pCostCenter);

        /// <summary>
        /// Updates CostCenterr information and returns vendor as saved in blue10
        /// </summary>
        Task<CostCenter> EditCostCenterAsync(CostCenter pCostCenter);

        /// <summary>
        /// Delete CostCenter from blue10, returns true if successful
        /// </summary>
        Task<bool> DeleteCostCenterAsync(CostCenter pCostCenter);

        Task<List<DocumentAction>> GetDocumentActionsAsync();

        /// <summary>
        /// Updates Document action
        /// </summary>
        /// <param name="pDocumentAction"> new state of existing DocumentAction record</param>
        /// <returns>Okay?</returns>
        Task<string> EditDocumentActionAsync(DocumentAction pDocumentAction);

        /// <summary>
        /// Returns all GLAccount for company. Input is id from company
        /// </summary>
        Task<List<GLAccount>> GetGLAccountsAsync(string pIdCompany);

        /// <summary>
        /// Add GLAccount and returns GLAccount as saved in blue10
        /// </summary>
        Task<GLAccount> AddGLAccountAsync(GLAccount pGLAccount);

        /// <summary>
        /// Updates GLAccount information and returns vendor as saved in blue10
        /// </summary>
        Task<GLAccount> EditGLAccountAsync(GLAccount pGLAccount);

        /// <summary>
        /// Delete GLAccount from blue10, returns true if successful
        /// </summary>
        Task<bool> DeleteGLAccountAsync(GLAccount pGLAccount);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<LogisticsDocumentAction>> GetLogisticsDocumentActionsAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pLogisticsDocumentAction"></param>
        /// <returns></returns>
        Task<string> EditLogisticsDocumentActionAsync(LogisticsDocumentAction pLogisticsDocumentAction);

        /// <summary>
        /// Returns all PaymentTerm for company. Input is id from company
        /// </summary>
        Task<List<PaymentTerm>> GetPaymentTermsAsync(string pIdCompany);

        /// <summary>
        /// Add PaymentTerm and returns PaymentTerm as saved in blue10
        /// </summary>
        Task<PaymentTerm> AddPaymentTermAsync(PaymentTerm pPaymentTerm);

        /// <summary>
        /// Updates PaymentTerm information and returns PaymentTerm as saved in blue10
        /// </summary>
        Task<PaymentTerm> EditPaymentTermAsync(PaymentTerm pPaymentTerm);

        /// <summary>
        /// Delete PaymentTerm from blue10, returns true if successful
        /// </summary>
        Task<bool> DeletePaymentTermAsync(PaymentTerm pPaymentTerm);

        /// <summary>
        /// Returns all Projects for company. Input is id from company
        /// </summary>
        Task<List<Project>> GetProjectsAsync(string pIdCompany);

        /// <summary>
        /// Add Project and returns Project as saved in blue10
        /// </summary>
        Task<Project> AddProjectAsync(Project pProject);

        /// <summary>
        /// Updates Project information and returns Project as saved in blue10
        /// </summary>
        Task<Project> EditProjectAsync(Project pProject);

        /// <summary>
        /// Delete Project from blue10, returns true if successful
        /// </summary>
        Task<bool> DeleteProjectAsync(Project pProject);

        /// <summary>
        /// Returns purchase invoice with blue10 id pId
        /// </summary>
        Task<PurchaseInvoice> GetPurchaseInvoiceAsync(Guid pId);
        /// <summary>
        /// Updates PurchaseInvoice information and returns PurchaseInvoice as saved in blue10
        /// </summary>
        Task<PurchaseInvoice> EditPurchaseInvoiceAsync(PurchaseInvoice pPurchaseInvoice);

        /// <summary>
        /// Returns purchase invoice original (PDF) for purchase invoice with blue10 id pId
        /// </summary>
        Task<byte[]> GetPurchaseInvoiceOriginalAsync(Guid pId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pIdCompany"></param>
        /// <returns></returns>
        Task<List<PurchaseInvoice>> GetPurchaseInvoiceWithoutPaymentDateAsync(string pIdCompany);

        /// <summary>
        /// Returns all PurchaseOrders for company. Input is id from company
        /// </summary>
        Task<List<PurchaseOrder>> GetPurchaseOrdersAsync(string pIdCompany);

        /// <summary>
        /// Add PurchaseOrder and returns Project as saved in blue10
        /// </summary>
        Task<PurchaseOrder> AddPurchaseOrderAsync(PurchaseOrder pPurchaseOrder);

        /// <summary>
        /// Updates PurchaseOrder information and returns PurchaseOrder as saved in blue10
        /// </summary>
        Task<PurchaseOrder> EditPurchaseOrderAsync(PurchaseOrder pPurchaseOrder);

        /// <summary>
        /// Delete PurchaseOrder from blue10, returns true if successful
        /// </summary>
        Task<bool> DeletePurchaseOrderAsync(PurchaseOrder pPurchaseOrder);

        /// <summary>
        /// Returns all VatCodes for company. Input is id from company
        /// </summary>
        Task<List<VatCode>> GetVatCodesAsync(string pIdCompany);

        /// <summary>
        /// Add VatCode and returns VatCode as saved in blue10
        /// </summary>
        Task<VatCode> AddVatCodeAsync(VatCode pVatCode);

        /// <summary>
        /// Updates VatCode information and returns VatCode as saved in blue10
        /// </summary>
        Task<VatCode> EditVatCodeAsync(VatCode pVatCode);

        /// <summary>
        /// Delete GLAccount from blue10, returns true if successful
        /// </summary>
        Task<bool> DeleteVatCodeAsync(VatCode pVatCode);

        /// <summary>
        /// Returns all Vendors for company. Input is id from company
        /// </summary>
        Task<List<Vendor>> GetVendorsAsync(string pIdCompany);

        /// <summary>
        /// Add vendor and returns vendor as saved in blue10
        /// </summary>
        Task<Vendor> AddVendorAsync(Vendor pVendor);

        /// <summary>
        /// Updates vendor information and returns vendor as saved in blue10
        /// </summary>
        Task<Vendor> EditVendorAsync(Vendor pVendor);

        /// <summary>
        /// Delete vendor record from blue10
        ///
        /// <param name="pVendor">a </param> 
        /// <returns>
        /// true if successful
        /// </returns>
        /// </summary>
        Task<bool> DeleteVendorAsync(Vendor pVendor);


        Task<List<VatScenario>> GetVatScenariosAsync(string pIdCompany);
        Task<VatScenario> AddVatScenarioAsync(VatScenario pVatScenario);
        Task<VatScenario> EditVatScenarioAsync(VatScenario pVatScenario);
        Task<bool> DeleteVatScenarioAsync(VatScenario pVatScenario);

        Task<List<Warehouse>> GetWarehousesAsync(string pIdCompany);
        Task<Warehouse> AddWarehouseAsync(Warehouse pWarehouse);
        Task<Warehouse> EditWarehouseAsync(Warehouse pWarehouse);
        Task<bool> DeleteWarehouseAsync(Warehouse pWarehouse);

    }
}