using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FurnitureFactoryBusinessLogic.BindingModels;
using FurnitureFactoryBusinessLogic.Interfaces;
using FurnitureFactoryWebClient.Models;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureFactoryWebClient.Controllers
{
    public class AdminController : Controller
    {
        private string adminPassword = "admin123";
        private readonly IClientLogic _client;
        public AdminController(IClientLogic client)
        {
            _client = client;
        }
        public IActionResult Index(AdminModel model)
        {
            if(model.Password == adminPassword)
            {
                Program.AdminMode = !Program.AdminMode;
                return RedirectToAction("Blocking");
            }
            if (String.IsNullOrEmpty(model.Password))
            {
                ModelState.AddModelError("","Введите пароль");
                return View(model);
            }
            else
            {
                ModelState.AddModelError("", "Введен неверный пароль");
                return View(model);
            }
        }
        public IActionResult Blocking()
        {
            ViewBag.Clients = _client.Read(null);
            return View();
        }
        public ActionResult Block(int id)
        {
            if (ModelState.IsValid)
            {
                var existClient = _client.Read(new ClientBindingModel
                {
                    Id = id
                }).FirstOrDefault();
                _client.CreateOrUpdate(new ClientBindingModel
                {
                    Id=id,
                    FirstName = existClient.FirstName,
                    SecondName = existClient.SecondName,
                    Patronymic = existClient.Patronymic,
                    Email = existClient.Email,
                    Password = existClient.Password,
                    PhoneNumber = existClient.PhoneNumber,
                    Block = !existClient.Block
                });
                return RedirectToAction("Blocking");
            }
            return RedirectToAction("Blocking");
        }
        public IActionResult Logout()
        {
            Program.AdminMode = false;
            return RedirectToAction("Index", "Home");
        }
    }
}