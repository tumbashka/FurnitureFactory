using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FurnitureFactoryWebClient.Models
{
    public class PositionModel
    {
        public int Count { get; set; }
        public int Price { get; set; }
        public string TypeName { get; set; }
        public string ModelName { get; set; }
    }
}
