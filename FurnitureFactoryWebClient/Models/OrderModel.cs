using FurnitureFactoryBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FurnitureFactoryWebClient.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public decimal TotalSum { get; set; }
        public int LeftSum { get; set; }
        public DateTime OrderDate { get; set; }
        public PaymentStatus Status { get; set; }
        public int ClientId { get; set; }
        public virtual List<PositionModel> Positions { get; set; }
    }
}
