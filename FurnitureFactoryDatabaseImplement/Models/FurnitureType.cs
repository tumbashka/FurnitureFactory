using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FurnitureFactoryDatabaseImplement.Models
{
    public class FurnitureType
    {
        public int Id { get; set; }
        [Required]
        public string TypeName { get; set; }
    }
}
