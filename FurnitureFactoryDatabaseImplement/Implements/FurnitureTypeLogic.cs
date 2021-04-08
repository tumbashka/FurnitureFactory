using FurnitureFactoryBusinessLogic.BindingModels;
using FurnitureFactoryBusinessLogic.Interfaces;
using FurnitureFactoryBusinessLogic.ViewModels;
using FurnitureFactoryDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace FurnitureFactoryDatabaseImplement.Implements
{
    public class FurnitureTypeLogic : IFurnitureTypeLogic
    {
        public List<FurnitureTypeViewModel> Read(FurnitureTypeBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                return context.FurnitureTypes.Where(rec => rec.Id == model.Id)
                .Select(rec => new FurnitureTypeViewModel
                {
                    Id = rec.Id,
                    TypeName = rec.TypeName
                })
            .ToList();
            }
        }
    }
}
