using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FurnitureFactoryBusinessLogic.BindingModels
{
    [DataContract]
    public class PaymentBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int PaymentAmount { get; set; }
        [DataMember]
        public DateTime PaymentDate { get; set; }
        [DataMember]
        public int OrderId { get; set; }
        [DataMember]
        public int ClientId { get; set; }

    }
}
