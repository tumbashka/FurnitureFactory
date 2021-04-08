using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FurnitureFactoryDatabaseImplement.Models
{
    public class Payment
    {
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        public int ClientId { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        [Required]
        public int PaymentAmount { get; set; }
        public virtual Client Client { get; set; }
        public virtual Order Order { get; set; }

    }
}
