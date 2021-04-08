using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FurnitureFactoryBusinessLogic.BindingModels
{
    [DataContract]
    public class FurnitureModelBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public string TypeName { get; set; }
        [DataMember]
        public string ModelName { get; set; }
        [DataMember]
        public string Dimensions { get; set; }
        [DataMember]
        public int Price { get; set; }
    }
}
