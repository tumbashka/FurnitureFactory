using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FurnitureFactoryBusinessLogic.BindingModels
{
    [DataContract]
    public class ClientBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public string SecondName { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string Patronymic { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public bool Block { get; set; }
    }
}
