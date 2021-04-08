using FurnitureFactoryBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace FurnitureFactoryBusinessLogic.ViewModels
{
    [DataContract]
    public class OrderViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [DisplayName("Отчество")]
        public string Patronymic { get; set; }
        [DataMember]
        [DisplayName("Имя")]
        public string FirstName { get; set; }
        [DataMember]
        [DisplayName("Фамилия")]
        public string SecondName { get; set; }
        [DataMember]
        [DisplayName("Дата заказа")]
        public DateTime OrderDate { get; set; }
        [DataMember]
        [DisplayName("Статус оплаты")]
        public PaymentStatus Status { get; set; }
        [DataMember]
        [DisplayName("Сумма")]
        public int TotalSum { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        [DisplayName("К оплате")]
        public int LeftSum { get; set; }
        [DataMember]
        public List<PositionViewModel> Positions { get; set; }
    }
}
