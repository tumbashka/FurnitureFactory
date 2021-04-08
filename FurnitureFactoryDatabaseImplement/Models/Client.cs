using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FurnitureFactoryDatabaseImplement.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        public string SecondName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Patronymic { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool Block { get; set; }
        [ForeignKey("ClientId")]
        public virtual List<Order> Orders { get; set; }
        [ForeignKey("ClientId")]
        public virtual List<Payment> Payments { get; set; }

    }
}
