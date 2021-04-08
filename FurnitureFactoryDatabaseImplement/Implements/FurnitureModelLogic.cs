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
    public class FurnitureModelLogic : IFurnitureModelLogic
    {
        private readonly string ModelFileName = "C://Users//Михан//Documents//FurnitureFactory//FurnitureModels.xml";
        public List<FurnitureModel> FurnitureModels { get; set; }
        public FurnitureModelLogic()
        {
            FurnitureModels = LoadFurnitureModels();
        }

        private List<FurnitureModel> LoadFurnitureModels()
        {
            var list = new List<FurnitureModel>();
            if (File.Exists(ModelFileName))
            {
                XDocument xDocument = XDocument.Load(ModelFileName);
                var xElements = xDocument.Root.Elements("FurnitureModel").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new FurnitureModel
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        Dimensions = elem.Element("Dimensions").Value,
                        ModelName = elem.Element("ModelName").Value,
                        Price = Convert.ToInt32(elem.Element("Price").Value),
                        TypeName = elem.Element("Type").Value
                    });
                }
            }
            return list;
        }
        public void SaveToDatabase()
        {
            var Models = LoadFurnitureModels();
            using(var context = new FurnitureFactoryDatabase())
            {
                foreach(var model in Models)
                {
                    FurnitureModel element = context.FurnitureModels.FirstOrDefault(rec => rec.Id == model.Id);
                    if(element!= null)
                    {
                        break;
                    }
                    else
                    {
                        element = new FurnitureModel();
                        context.FurnitureModels.Add(element);
                    }
                    element.ModelName = model.ModelName;
                    element.Price = model.Price;
                    element.TypeName = model.TypeName;
                    element.Dimensions = model.Dimensions;
                    context.SaveChanges();
                }
            }
        }

        public List<FurnitureModelViewModel> Read(FurnitureModelBindingModel model)
        {
            SaveToDatabase();
            return FurnitureModels
                .Where(rec => model == null || rec.Id == model.Id)
                .Select(rec => new FurnitureModelViewModel
                {
                    Id = rec.Id,
                    Dimensions = rec.Dimensions,
                    ModelName = rec.ModelName,
                    Price = rec.Price,
                    TypeName = rec.TypeName
                })
                .ToList();
        }
    }
}
