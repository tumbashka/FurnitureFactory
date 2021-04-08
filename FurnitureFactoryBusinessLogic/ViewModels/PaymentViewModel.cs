using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace FurnitureFactoryBusinessLogic.ViewModels
{
    [DataContract]
    public class PaymentViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [DisplayName("Сумма оплаты")]
        public int PaymentAmount { get; set; }
        [DataMember]
        [DisplayName("Дата оплаты")]
        public DateTime PaymentDate { get; set; }
        [DataMember]
        public int OrderId { get; set; }
        [DataMember]
        public int ClientId { get; set; }
    }
}
