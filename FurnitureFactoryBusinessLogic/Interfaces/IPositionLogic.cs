using FurnitureFactoryBusinessLogic.BindingModels;
using FurnitureFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureFactoryBusinessLogic.Interfaces
{
    public interface IPositionLogic
    {
        List<PositionViewModel> Read(PositionBindingModel model);
        void CreateOrUpdate(PositionBindingModel model);
    }
}
