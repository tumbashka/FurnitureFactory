using FurnitureFactoryBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FurnitureFactoryDatabaseImplement.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        [Required]
        public int TotalSum { get; set; }
        public PaymentStatus Status { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        public Client Client { get; set; }
        [ForeignKey("OrderId")]
        public virtual List<Position> Positions { get; set; }
        [Required]
        [ForeignKey("OrderId")]
        public virtual List<Payment> Payments { get; set; }

    }
}
