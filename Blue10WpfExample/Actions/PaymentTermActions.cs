using Blue10SDK;
using Blue10SDK.Models;
using System;
using System.Collections.Generic;

namespace Blue10SdkWpfExample
{
    internal class PaymentTermActions
    {
        private IBlue10Client MBlue10Client { get; set; }
        public PaymentTermActions(IBlue10Client pBlue10Client)
        {
            MBlue10Client = pBlue10Client;
        }

        public List<PaymentTerm> GetAll(string pIdCompany)
        {
            var fRet = MBlue10Client.GetPaymentTerms(pIdCompany);
            return fRet;
        }

        public void Save(PaymentTerm pPaymentTerm)
        {
            if (pPaymentTerm.Id == Guid.Empty) pPaymentTerm = MBlue10Client.AddPaymentTerm(pPaymentTerm);
            else pPaymentTerm = MBlue10Client.EditPaymentTerm(pPaymentTerm);
        }

        public void Delete(PaymentTerm pPaymentTerm)
        {
            MBlue10Client.DeletePaymentTerm(pPaymentTerm);
        }
    }
}