using FurnitureFactoryBusinessLogic.BindingModels;
using FurnitureFactoryBusinessLogic.Interfaces;
using FurnitureFactoryBusinessLogic.ViewModels;
using FurnitureFactoryDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FurnitureFactoryDatabaseImplement.Implements
{
    public class OrderLogic : IOrderLogic
    {
        public void CreateOrUpdate(OrderBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Order element = model.Id.HasValue ? null : new Order();
                        if (model.Id.HasValue)
                        {
                            element = context.Orders.FirstOrDefault(rec => rec.Id ==
                           model.Id);
                            if (element == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                            element.ClientId = model.ClientId;
                            element.OrderDate = model.OrderDate;
                            element.Status = model.Status;
                            element.TotalSum = model.TotalSum;
                            context.SaveChanges();
                        }
                        else
                        {
                            element.ClientId = model.ClientId;
                            element.OrderDate = model.OrderDate;
                            element.TotalSum = model.TotalSum;
                            element.Status = model.Status;
                            context.Orders.Add(element);
                            context.SaveChanges();
                            var groupPositions = model.Positions
                               .GroupBy(rec => rec.FurnitureModelId)
                               .Select(rec => new
                               {
                                   FurnitureModelId = rec.Key,
                                   Count = rec.Sum(r => r.Count)
                               });

                            foreach (var groupPosition in groupPositions)
                            {
                                context.Positions.Add(new Position
                                {
                                    OrderId = element.Id,
                                    FurnitureModelId = groupPosition.FurnitureModelId,
                                    Count = groupPosition.Count
                                });
                                context.SaveChanges();
                            }
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Delete(OrderBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id.Value);

                if (element != null)
                {
                    context.Orders.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                return context.Orders.Where(rec => rec.Id == model.Id
                || (rec.ClientId == model.ClientId)
                && (model.Date == null
                && model.DateTo == null
                || rec.OrderDate >= model.Date
                && rec.OrderDate <= model.DateTo))
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    ClientId = rec.ClientId,
                    FirstName = rec.Client.FirstName,
                    SecondName = rec.Client.SecondName,
                    Patronymic = rec.Client.Patronymic,
                    TotalSum = rec.TotalSum,
                    OrderDate = rec.OrderDate,
                    LeftSum = rec.TotalSum - context.Payments.Where(recP => recP.OrderId == rec.Id).Select(recP => recP.PaymentAmount).Sum(),
                    Status = rec.Status,
                    Positions = GetOrderPositionsViewModel(rec)
                })
            .ToList();
            }
        }

        public static List<PositionViewModel> GetOrderPositionsViewModel(Order order)
        {
            using(var context = new FurnitureFactoryDatabase())
            {
                var Positions = context.Positions
                    .Where(rec => rec.OrderId == order.Id)
                    .Include(rec => rec.FurnitureModel)
                    .Select(rec => new PositionViewModel
                    {
                        Id = rec.Id,
                        OrderId = rec.OrderId,
                        FurnitureModelId = rec.FurnitureModelId,
                        Count = rec.Count
                    }).ToList();
                foreach(var position in Positions)
                {
                    var positionData = context.FurnitureModels.Where(rec => rec.Id == position.FurnitureModelId).FirstOrDefault();
                    position.TypeName = positionData.TypeName;
                    position.ModelName = positionData.ModelName;
                    position.Price = positionData.Price;
                }
                return Positions;
            }
        }
    }
}

