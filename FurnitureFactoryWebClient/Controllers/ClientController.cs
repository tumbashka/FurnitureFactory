using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FurnitureFactoryBusinessLogic.BindingModels;
using FurnitureFactoryBusinessLogic.Interfaces;
using FurnitureFactoryWebClient.Models;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureFactoryWebClient.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientLogic _client;
        public ClientController(IClientLogic client)
        {
            _client = client;
        }
        public IActionResult Profile()
        {
            ViewBag.User = Program.Client;
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel client)
        {
            if (String.IsNullOrEmpty(client.Email))
            {
                ModelState.AddModelError("", "Введите E-Mail");
                return View(client);
            }
            if (String.IsNullOrEmpty(client.Password))
            {
                ModelState.AddModelError("", "Введите пароль");
                return View(client);
            }
            var clientView = _client.Read(new ClientBindingModel
            {
                Email = client.Email,
                Password = client.Password
            }).FirstOrDefault();
            if(clientView == null)
            {
                ModelState.AddModelError("", "Введен неверный пароль, либо пользователь не найден");
                return View(client);
            }
            if(clientView.Block == true)
            {
                ModelState.AddModelError("", "Пользователь заблокирован");
                return View(client);
            }
            Program.Client = clientView;
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Logout()
        {
            Program.Client = null;
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Registration(ClientRegistrationModel client)
        {
            if (String.IsNullOrEmpty(client.SecondName))
            {
                ModelState.AddModelError("", "Введите фамилию");
                return View(client);
            }
            if (String.IsNullOrEmpty(client.FirstName))
            {
                ModelState.AddModelError("", "Введите имя");
                return View(client);
            }
            if (String.IsNullOrEmpty(client.Patronymic))
            {
                ModelState.AddModelError("", "Введите отчество");
                return View(client);
            }
            if (String.IsNullOrEmpty(client.PhoneNumber))
            {
                ModelState.AddModelError("", "Введите телефон");
                return View(client);
            }
            if (!Regex.IsMatch(client.PhoneNumber, @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$"))
            {
                ModelState.AddModelError("", "Номер телефона введен некорректно");
                return View(client);
            }
            if (String.IsNullOrEmpty(client.Email))
            {
                ModelState.AddModelError("", "Введите E-Mail");
                return View(client);
            }
            if (!Regex.IsMatch(client.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                ModelState.AddModelError("", "E-Mail введен некорректно");
                return View(client);
            }        
            if (String.IsNullOrEmpty(client.Password))
            {
                ModelState.AddModelError("", "Введите пароль");
                return View(client);
            }
            if (client.Password.Length< 5 || client.Password.Length>40)
            {
                ModelState.AddModelError("", "Пароль должен быть не менее 5 и не более 40 символов");
                return View(client);
            }
            var existClientEmail = _client.Read(new ClientBindingModel
            {
                Email = client.Email
            }).FirstOrDefault();
            if(existClientEmail != null)
            {
                ModelState.AddModelError("", "Данный E-Mail уже занят");
                return View(client);
            }
            var existClientPhone = _client.Read(new ClientBindingModel
            {
                PhoneNumber = client.PhoneNumber
            }).FirstOrDefault();
            if (existClientPhone != null)
            {
                ModelState.AddModelError("", "Данный номер телефона уже занят");
                return View(client);
            }
            _client.CreateOrUpdate(new ClientBindingModel
            {
                FirstName = client.FirstName,
                SecondName = client.SecondName,
                Patronymic = client.Patronymic,
                Email = client.Email,
                Block = false,
                Password = client.Password,
                PhoneNumber = client.PhoneNumber
            });
            ModelState.AddModelError("", "Регистрация успешна");
            return View("Registration", client);
        }
    }
}