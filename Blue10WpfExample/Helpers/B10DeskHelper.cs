using Blue10SDK;
using Blue10SDK.Models;
using Blue10SDK.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue10SdkWpfExample
{
    public class B10DeskHelper
    {
        private IBlue10AsyncClient mAsyncCLient;
        private object pPaymentTerm;

        public async Task<string> ConnectToApiAsync(string fApiKey, string fApiUrl)
        {
            mAsyncCLient = Blue10.CreateAsyncClient(fApiKey, fApiUrl);
            var fMe = await mAsyncCLient.GetMeAsync();
            return fMe.EnvironmentName;
        }

        public async Task<List<AdministrationAction>> GetAdministrationActions()
        {
            return await mAsyncCLient.GetAdministrationActionsAsync();
        }

        public async Task<bool> FinishAdministrationAction(AdministrationAction pAdministrationAction)
        {
            await mAsyncCLient.FinishAdministrationActionAsync(pAdministrationAction);
            return true;
        }

        public async Task<List<Article>> GetArticles(string pIdCompany)
        {
            return await mAsyncCLient.GetArticlesAsync(pIdCompany);
        }

        public async Task<Article> SaveArticle(Article pArticle)
        {
            if (pArticle.Id == Guid.Empty) return await mAsyncCLient.AddArticleAsync(pArticle);
            return await mAsyncCLient.EditArticleAsync(pArticle);
        }

        public async Task<bool> DeleteArticle(Article pArticle)
        {
            return await mAsyncCLient.DeleteArticleAsync(pArticle);
        }

        public async Task<List<DocumentAction>> GetDocumentActions()
        {
            return await mAsyncCLient.GetDocumentActionsAsync();
        }

        public async Task<string> SaveDocumentAction(DocumentAction pDocumentAction)
        {
            return await mAsyncCLient.EditDocumentActionAsync(pDocumentAction);
        }

        public async Task<List<LogisticsDocumentAction>> GetLogisticsDocumentActions()
        {
            return await mAsyncCLient.GetLogisticsDocumentActionsAsync();
        }

        public async Task<string> SaveLogisticsDocumentAction(LogisticsDocumentAction pLogisticsDocumentAction)
        {
            return await mAsyncCLient.EditLogisticsDocumentActionAsync(pLogisticsDocumentAction);
        }

        public async Task<List<Company>> GetCompanies()
        {
            return await mAsyncCLient.GetCompaniesAsync();
        }

        public async Task<Company> SaveCompany(Company pCompany)
        {
            return await mAsyncCLient.UpdateCompanyAsync(pCompany);
        }

        public async Task<List<CostCenter>> GetCostCenters(string pIdCompany)
        {
            return await mAsyncCLient.GetCostCentersAsync(pIdCompany);
        }

        public async Task<CostCenter> SaveCostCenter(CostCenter pCostCenter)
        {
            if(pCostCenter.Id == Guid.Empty) return await mAsyncCLient.AddCostCenterAsync(pCostCenter);
            return await mAsyncCLient.EditCostCenterAsync(pCostCenter);
        }

        public async Task<bool> DeleteCostCenter(CostCenter pCostCenter)
        {
            return await mAsyncCLient.DeleteCostCenterAsync(pCostCenter);
        }

        public async Task<List<CostUnit>> GetCostUnits(string pIdCompany)
        {
            return await mAsyncCLient.GetCostUnitsAsync(pIdCompany);
        }

        public async Task<CostUnit> SaveCostUnit(CostUnit pCostUnit)
        {
            if (pCostUnit.Id == Guid.Empty) return await mAsyncCLient.AddCostUnitAsync(pCostUnit);
            return await mAsyncCLient.EditCostUnitAsync(pCostUnit);
        }

        public async Task<bool> DeleteCostUnit(CostUnit pCostUnit)
        {
            return await mAsyncCLient.DeleteCostUnitAsync(pCostUnit);
        }

        public async Task<List<Project>> GetProjects(string pIdCompany)
        {
            return await mAsyncCLient.GetProjectsAsync(pIdCompany);
        }

        public async Task<Project> SaveProject(Project pProject)
        {
            if (pProject.Id == Guid.Empty) return await mAsyncCLient.AddProjectAsync(pProject);
            return await mAsyncCLient.EditProjectAsync(pProject);
        }

        public async Task<bool> DeleteProject(Project pProject)
        {
            return await mAsyncCLient.DeleteProjectAsync(pProject);
        }

        public async Task<List<GLAccount>> GetGLAccounts(string pIdCompany)
        {
            return await mAsyncCLient.GetGLAccountsAsync(pIdCompany);
        }

        public async Task<GLAccount> SaveGLAccount(GLAccount pGLAccount)
        {
            if (pGLAccount.Id == Guid.Empty) return await mAsyncCLient.AddGLAccountAsync(pGLAccount);
            return await mAsyncCLient.EditGLAccountAsync(pGLAccount);
        }

        public async Task<bool> DeleteGLAccount(GLAccount pGLAccount)
        {
            return await mAsyncCLient.DeleteGLAccountAsync(pGLAccount);
        }

        public async Task<List<PaymentTerm>> GetPaymentTerms(string pIdCompany)
        {
            return await mAsyncCLient.GetPaymentTermsAsync(pIdCompany);
        }

        public async Task<PaymentTerm> SavePaymentTerm(PaymentTerm pPaymentTerm)
        {
            if (pPaymentTerm.Id == Guid.Empty) return await mAsyncCLient.AddPaymentTermAsync(pPaymentTerm);
            return await mAsyncCLient.EditPaymentTermAsync(pPaymentTerm);
        }

        public async Task<bool> DeletePaymentTerm(PaymentTerm pPaymentTerm)
        {
            return await mAsyncCLient.DeletePaymentTermAsync(pPaymentTerm);
        }

        public async Task<List<VatCode>> GetVatCodes(string pIdCompany)
        {
            return await mAsyncCLient.GetVatCodesAsync(pIdCompany);
        }

        public async Task<VatCode> SaveVatCode(VatCode pVatCode)
        {
            if (pVatCode.Id == Guid.Empty) return await mAsyncCLient.AddVatCodeAsync(pVatCode);
            return await mAsyncCLient.EditVatCodeAsync(pVatCode);
        }

        public async Task<bool> DeleteVatCode(VatCode pVatCode)
        {
            return await mAsyncCLient.DeleteVatCodeAsync(pVatCode);
        }

        public async Task<List<VatScenario>> GetVatScenarios(string pIdCompany)
        {
            return await mAsyncCLient.GetVatScenariosAsync(pIdCompany);
        }

        public async Task<VatScenario> SaveVatScenario(VatScenario pVatScenario)
        {
            if (pVatScenario.Id == Guid.Empty) return await mAsyncCLient.AddVatScenarioAsync(pVatScenario);
            return await mAsyncCLient.EditVatScenarioAsync(pVatScenario);
        }

        public async Task<bool> DeleteVatScenario(VatScenario pVatScenario)
        {
            return await mAsyncCLient.DeleteVatScenarioAsync(pVatScenario);
        }

        public async Task<List<Vendor>> GetVendors(string pIdCompany)
        {
            return await mAsyncCLient.GetVendorsAsync(pIdCompany);
        }

        public async Task<Vendor> SaveVendor(Vendor pVendor)
        {
            if (pVendor.Id == Guid.Empty) return await mAsyncCLient.AddVendorAsync(pVendor);
            return await mAsyncCLient.EditVendorAsync(pVendor);
        }

        public async Task<bool> DeleteVendor(Vendor pVendor)
        {
            return await mAsyncCLient.DeleteVendorAsync(pVendor);
        }

        public async Task<List<Warehouse>> GetWarehouses(string pIdCompany)
        {
            return await mAsyncCLient.GetWarehousesAsync(pIdCompany);
        }

        public async Task<Warehouse> SaveWarehouse(Warehouse pWarehouse)
        {
            if (pWarehouse.Id == Guid.Empty) return await mAsyncCLient.AddWarehouseAsync(pWarehouse);
            return await mAsyncCLient.EditWarehouseAsync(pWarehouse);
        }

        public async Task<bool> DeleteWarehouse(Warehouse pWarehouse)
        {
            return await mAsyncCLient.DeleteWarehouseAsync(pWarehouse);
        }

        public async Task<List<PurchaseOrder>> GetPurchaseOrders(string pIdCompany)
        {
            return await mAsyncCLient.GetPurchaseOrdersAsync(pIdCompany);
        }

        public async Task<bool> DeletePurchaseOrder(PurchaseOrder pPurchaseOrder)
        {
            return await mAsyncCLient.DeletePurchaseOrderAsync(pPurchaseOrder);
        }
        public async Task<PurchaseOrder> SavePurchaseOrder(PurchaseOrder pPurchaseOrder)
        {
            if (pPurchaseOrder.Id == Guid.Empty) return await mAsyncCLient.AddPurchaseOrderAsync(pPurchaseOrder);
            return await mAsyncCLient.EditPurchaseOrderAsync(pPurchaseOrder);
        }

        public async Task<List<PurchaseInvoice>> GetUnpaidInvoices(string pIdCompany)
        {
            return await mAsyncCLient.GetPurchaseInvoiceWithoutPaymentDateAsync(pIdCompany);
        }
        public async Task<PurchaseInvoice> SavePurchaseInvoice(PurchaseInvoice pPurchaseInvoice)
        {
            return await mAsyncCLient.EditPurchaseInvoiceAsync(pPurchaseInvoice);
        }

    }
}
