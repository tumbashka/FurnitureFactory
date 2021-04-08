using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace FurnitureFactoryBusinessLogic.ViewModels
{
    [DataContract]
    public class FurnitureModelViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [DisplayName("Тип")]
        public string TypeName { get; set; }
        [DataMember]
        [DisplayName("Название")]
        public string ModelName { get; set; }
        [DataMember]
        [DisplayName("Габариты")]
        public string Dimensions { get; set; }
        [DataMember]
        [DisplayName("Цена")]
        public int Price { get; set; }

    }
}
