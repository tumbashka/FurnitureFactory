using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace FurnitureFactoryBusinessLogic.ViewModels
{
    [DataContract]
    public class ClientViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [DisplayName("Фамилия")]
        public string SecondName { get; set; }
        [DataMember]
        [DisplayName("Имя")]
        public string FirstName { get; set; }
        [DataMember]
        [DisplayName("Отчество")]
        public string Patronymic { get; set; }
        [DataMember]
        [DisplayName("Номер телефона")]
        public string PhoneNumber { get; set; }
        [DataMember]
        [DisplayName("Почта")]
        public string Email { get; set; }
        [DataMember]
        [DisplayName("Пароль")]
        public string Password { get; set; }
        [DataMember]
        [DisplayName("Блокировка")]
        public bool Block { get; set; }
    }
}
