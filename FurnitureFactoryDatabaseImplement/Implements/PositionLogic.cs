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
    public class PositionLogic : IPositionLogic
    {
        public void CreateOrUpdate(PositionBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                Position element = model.Id.HasValue ? null : new Position();
                if (model.Id.HasValue)
                {
                    element = context.Positions.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Position();
                    context.Positions.Add(element);
                }
                element.Count = model.Count;
                element.FurnitureModelId = model.FurnitureModelId;
                element.OrderId = model.OrderId;
                context.SaveChanges();
            }
        }

        public List<PositionViewModel> Read(PositionBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                return context.Positions.Where(rec => rec.Id == model.Id || model == null || rec.OrderId.Equals(model.OrderId))
                .Select(rec => new PositionViewModel
                {
                    Id = rec.Id,
                    Count = rec.Count,
                    FurnitureModelId =rec.FurnitureModelId,
                    ModelName = rec.FurnitureModel.ModelName,
                    Price = rec.Count*rec.FurnitureModel.Price,
                    OrderId =rec.OrderId,
                    TypeName = rec.FurnitureModel.TypeName                    
                })
            .ToList();
            }
        }
    }
}
