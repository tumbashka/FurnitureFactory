using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FurnitureFactoryBusinessLogic.BindingModels
{
    [DataContract]
    public class FurnitureTypeBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public string TypeName { get; set; }

    }
}
