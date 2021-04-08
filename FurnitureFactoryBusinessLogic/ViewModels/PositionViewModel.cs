using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace FurnitureFactoryBusinessLogic.ViewModels
{
    [DataContract]
    public class PositionViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [DisplayName("Количество")]
        public int Count { get; set; }
        [DataMember]
        public int OrderId { get; set; }
        [DataMember]
        public int Price { get; set; }
        [DataMember]
        [DisplayName("Название")]
        public string ModelName { get; set; }

        [DisplayName("Тип")]
        public string TypeName { get; set; }
        [DataMember]
        public int FurnitureModelId { get; set; }
    }
}
