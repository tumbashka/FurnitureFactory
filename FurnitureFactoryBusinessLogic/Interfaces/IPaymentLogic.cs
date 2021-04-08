using FurnitureFactoryBusinessLogic.BindingModels;
using FurnitureFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureFactoryBusinessLogic.Interfaces
{
    public interface IPaymentLogic
    {
        List<PaymentViewModel> Read(PaymentBindingModel model);
        void CreateOrUpdate(PaymentBindingModel model);
        void Delete(PaymentBindingModel model);
    }
}
