using FurnitureFactoryBusinessLogic.BindingModels;
using FurnitureFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureFactoryBusinessLogic.Interfaces
{
    public interface IFurnitureTypeLogic
    {
        List<FurnitureTypeViewModel> Read(FurnitureTypeBindingModel model);
    }
}
