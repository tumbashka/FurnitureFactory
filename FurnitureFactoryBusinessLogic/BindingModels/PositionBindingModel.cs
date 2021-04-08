using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FurnitureFactoryBusinessLogic.BindingModels
{
    [DataContract]
    public class PositionBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public int OrderId { get; set; }
        [DataMember]
        public int FurnitureModelId { get; set; }
        [DataMember]
        public int Price { get; set; }
        [DataMember]
        public string FurnitureModelName { get; set; }


    }
}
