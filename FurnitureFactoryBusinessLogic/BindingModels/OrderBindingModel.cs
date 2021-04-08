using FurnitureFactoryBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FurnitureFactoryBusinessLogic.BindingModels
{
    [DataContract]
    public class OrderBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public DateTime OrderDate { get; set; }
        [DataMember]
        public PaymentStatus Status { get; set; }
        [DataMember]
        public int TotalSum { get; set; }
        [DataMember]
        public int LeftSum { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public DateTime? Date { get; set; }
        [DataMember]
        public DateTime? DateTo { get; set; }
        [DataMember]
        public List<PositionBindingModel> Positions { get; set; }
    }
}
