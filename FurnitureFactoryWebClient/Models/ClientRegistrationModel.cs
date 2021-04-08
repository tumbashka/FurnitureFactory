using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FurnitureFactoryDatabaseImplement.Models;

namespace FurnitureFactoryWebClient.Models
{
    public class ClientRegistrationModel
    {
        [Required(ErrorMessage ="Введите имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Введите отчество")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Введите E-Mail")]
        [EmailAddress(ErrorMessage = "Введен некорректный E-Mail"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите номер телефона")]
        [RegularExpression(@"^([\+]?(?:00)?[0-9]{1,3}[\s.-]?[0-9]{1,12})([\s.-]?[0-9]{1,4}?)$", ErrorMessage = "Введен некорректный номер телефона")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(40, ErrorMessage = "Пароль должен содержать от 5 до 40 символов", MinimumLength = 5), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
