using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FurnitureFactoryDatabaseImplement.Models
{
    public class FurnitureModel
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        [Required]
        public string ModelName { get; set; }
        [Required]
        public string Dimensions { get; set; }
        [Required]
        public int Price { get; set; }
    }
}
