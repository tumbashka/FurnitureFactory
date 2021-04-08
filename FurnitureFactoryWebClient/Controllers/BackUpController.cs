using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FurnitureFactoryBusinessLogic.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureFactoryWebClient.Controllers
{
    public class BackUpController : Controller
    {
        private readonly BackUpAbstractLogic _backUp;
        public BackUpController(BackUpAbstractLogic backUp)
        {
            _backUp = backUp;
        }
        public IActionResult BackUp()
        {
            return View();
        }
        public IActionResult BackUpToJson()
        {
            string fileName = "C:\\Users\\Михан\\Desktop\\backUp\\backUp";
            if (Directory.Exists(fileName))
            {
                _backUp.CreateArchive(fileName);
                return RedirectToAction("BackUp");
            }
            else
            {
                DirectoryInfo di = Directory.CreateDirectory(fileName);
                _backUp.CreateArchive(fileName);
                return RedirectToAction("BackUp");
            }
        }
    }
}