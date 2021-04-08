using FurnitureFactoryBusinessLogic.BindingModels;
using FurnitureFactoryBusinessLogic.Interfaces;
using FurnitureFactoryBusinessLogic.ViewModels;
using FurnitureFactoryDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FurnitureFactoryDatabaseImplement.Implements
{
    public class PaymentLogic : IPaymentLogic
    {
        public void CreateOrUpdate(PaymentBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                Payment element = model.Id.HasValue ? null : new Payment();
                if (model.Id.HasValue)
                {
                    element = context.Payments.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Payment();
                    context.Payments.Add(element);
                }
                element.OrderId = model.OrderId;
                element.ClientId = model.ClientId;
                element.PaymentAmount = model.PaymentAmount;
                element.PaymentDate = model.PaymentDate;
                context.SaveChanges();
            }
        }

        public void Delete(PaymentBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                Payment element = context.Payments.FirstOrDefault(rec => rec.Id == model.Id.Value);

                if (element != null)
                {
                    context.Payments.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public List<PaymentViewModel> Read(PaymentBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                return context.Payments
                .Where(rec => model == null ||rec.Id == model.Id || rec.OrderId.Equals(model.OrderId))
                .Select(rec => new PaymentViewModel
                {
                    Id = rec.Id,
                    ClientId = rec.ClientId,
                    OrderId = rec.OrderId,
                    PaymentAmount = rec.PaymentAmount,
                    PaymentDate = rec.PaymentDate
                })
            .ToList();
            }
        }
    }
}
