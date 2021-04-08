using FurnitureFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureFactoryBusinessLogic.HelperModels
{
    public class PdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<OrderViewModel> Orders { get; set; }
        public Dictionary<int, List<PaymentViewModel>> Payments { get; set; }
    }
}
