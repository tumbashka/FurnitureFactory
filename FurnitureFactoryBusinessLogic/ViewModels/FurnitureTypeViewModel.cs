using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace FurnitureFactoryBusinessLogic.ViewModels
{
    [DataContract]
    public class FurnitureTypeViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [DisplayName("Вид мебели")]
        public string TypeName { get; set; }
    }
}
