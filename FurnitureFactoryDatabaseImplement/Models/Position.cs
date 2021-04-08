using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FurnitureFactoryDatabaseImplement.Models
{
    public class Position
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int FurnitureModelId { get; set; }
        public int Count { get; set; }
        public virtual Order Order { get; set; }
        public virtual FurnitureModel FurnitureModel { get; set; }
    }
}
