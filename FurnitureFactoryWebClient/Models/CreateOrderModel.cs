using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FurnitureFactoryWebClient.Models
{
    public class CreateOrderModel
    {
        public Dictionary<int,int> Positions { get; set; }
    }
}
