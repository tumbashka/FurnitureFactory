using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FurnitureFactoryBusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureFactoryWebClient.Controllers
{
    public class FurnitureController : Controller
    {
        private readonly IFurnitureModelLogic _furnitureModelLogic;
        public FurnitureController(IFurnitureModelLogic furnitureModelLogic)
        {
            _furnitureModelLogic = furnitureModelLogic;
        }
        public IActionResult Furniture()
        {
            ViewBag.Furnitures = _furnitureModelLogic.Read(null);
            return View();
        }

    }
}