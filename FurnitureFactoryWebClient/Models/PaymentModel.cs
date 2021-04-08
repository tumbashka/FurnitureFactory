using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FurnitureFactoryWebClient.Models
{
    public class PaymentModel
    {
        [Required]
        public int PaymentAmount { get; set; }
        public int OrderId { get; set; }
    }
}
