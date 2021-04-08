using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FurnitureFactoryWebClient.Models
{
    public class AdminModel
    {
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
