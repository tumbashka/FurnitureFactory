using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FurnitureFactoryBusinessLogic.Interfaces;
using FurnitureFactoryBusinessLogic.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using FurnitureFactoryBusinessLogic.BindingModels;
using FurnitureFactoryWebClient.Models;
using FurnitureFactoryBusinessLogic.ViewModels;
using FurnitureFactoryBusinessLogic.Enums;

namespace FurnitureFactoryWebClient.Controllers
{
    public class OrderController : Controller
    {
        private readonly IPositionLogic _positionLogic;
        private readonly IOrderLogic _orderLogic;
        private readonly IFurnitureModelLogic _furnitureModelLogic;
        private readonly IPaymentLogic _paymentLogic;
        private readonly ReportLogic _reportLogic;
        public OrderController(IPositionLogic positionLogic,IOrderLogic orderLogic, IFurnitureModelLogic furnitureModelLogic, IPaymentLogic paymentLogic, ReportLogic reportLogic)
        {
            _positionLogic = positionLogic;
            _orderLogic = orderLogic;
            _furnitureModelLogic = furnitureModelLogic;
            _paymentLogic = paymentLogic;
            _reportLogic = reportLogic;
        }
        public IActionResult Order()
        {
            ViewBag.Orders = _orderLogic.Read(new OrderBindingModel
            {
                ClientId = Program.Client.Id
            });
            return View();
        }
        [HttpPost]
        public IActionResult Order(ReportModel model)
        {
            var paymentList = new List<PaymentViewModel>();
            var orders = new List<OrderViewModel>();
            orders = _orderLogic.Read(new OrderBindingModel
            {
                ClientId = Program.Client.Id,
                Date = model.From,
                DateTo = model.To
            });
            var payments = _paymentLogic.Read(null);
            foreach(var order in orders)
            {
                foreach(var payment in payments)
                {
                    if (payment.ClientId == Program.Client.Id && payment.OrderId == order.Id)
                        paymentList.Add(payment);
                }
            }
            ViewBag.Payments = paymentList;
            ViewBag.Orders = orders;
            string fileName = "C:\\Users\\Михан\\Desktop\\data\\pdfreport.pdf";
            if (model.SendMail)
            {
                _reportLogic.SaveOrderPaymentsToPdfFile(fileName, new OrderBindingModel
                {
                    ClientId = Program.Client.Id,
                    Date = model.From,
                    DateTo = model.To
                }, Program.Client.Email);
            }
            else
            {
                _reportLogic.SaveOrderPaymentsToPdfFile(fileName, new OrderBindingModel
                {
                    ClientId = Program.Client.Id,
                    Date = model.From,
                    DateTo = model.To
                }, null);
            }
            return View();
        }
        
        public IActionResult CreateOrder()
        {
            ViewBag.Positions = _furnitureModelLogic.Read(null);
            return View();
        }
        [HttpPost]
        public ActionResult CreateOrder(CreateOrderModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Positions = _furnitureModelLogic.Read(null);
                return View(model);
            }
            if(model.Positions == null)
            {
                ViewBag.Positions = _furnitureModelLogic.Read(null);
                ModelState.AddModelError("", "Мебель не выбрана");
                return View(model);
            }
            var positions = new List<PositionBindingModel>();
            foreach(var furnitureModel in model.Positions)
            {
                if (furnitureModel.Value > 0)
                {
                    positions.Add(new PositionBindingModel
                    {
                        FurnitureModelId = furnitureModel.Key,
                        Count = furnitureModel.Value
                    });
                }
            }
            if (positions.Count == 0)
            {
                ViewBag.Positions = _furnitureModelLogic.Read(null);
                ModelState.AddModelError("", "Мебель не выбрана");
                return View(model);
            }
            _orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                ClientId = Program.Client.Id,
                OrderDate = DateTime.Now,
                Status = PaymentStatus.Оформлен,
                TotalSum = CalculateSum(positions),
                Positions = positions
            });
            return RedirectToAction("Order");
        }
        private int CalculateSum(List<PositionBindingModel> positions)
        {
            int sum = 0;
            foreach(var furnitureModel in positions)
            {
                var furnitureData = _furnitureModelLogic.Read(new FurnitureModelBindingModel { Id = furnitureModel.FurnitureModelId }).FirstOrDefault();
                if(furnitureData!= null)
                {
                    for (int i = 0; i < furnitureModel.Count; i++)
                        sum += furnitureData.Price;
                }
            }
            return sum;
        }
        public IActionResult Payment(int id)
        {
            var order = _orderLogic.Read(new OrderBindingModel
            {
                Id = id
            }).FirstOrDefault();
            ViewBag.LeftSum = CalculateLeftSum(order);
            ViewBag.Order = order;
            return View();
        }
        [HttpPost]
        public ActionResult Payment(PaymentModel model)
        {
            OrderViewModel order = _orderLogic.Read(new OrderBindingModel
            {
                Id = model.OrderId
            }).FirstOrDefault();
            int leftSum = CalculateLeftSum(order);
            if (!ModelState.IsValid)
            {
                ViewBag.Order = order;
                ViewBag.LeftSum = leftSum;
                return View(model);
            }
            if (leftSum < model.PaymentAmount)
            {
                ViewBag.Order = order;
                ViewBag.LeftSum = leftSum;
                return View(model);
            }
            _paymentLogic.CreateOrUpdate(new PaymentBindingModel
            {
                OrderId = order.Id,
                ClientId = Program.Client.Id,
                PaymentDate = DateTime.Now,
                PaymentAmount = model.PaymentAmount
            });
            leftSum -= model.PaymentAmount;
            _orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                ClientId = order.ClientId,
                OrderDate = order.OrderDate,
                Status = leftSum > 0 ? PaymentStatus.Оплачен_не_полностью : PaymentStatus.Оплачен,
                TotalSum = order.TotalSum,
                Positions = order.Positions.Select(rec => new PositionBindingModel
                {
                    Id = rec.Id,
                    OrderId = rec.OrderId,
                    FurnitureModelId = rec.FurnitureModelId,
                    FurnitureModelName = rec.ModelName,
                    Count = rec.Count
                }).ToList()
            });
            return RedirectToAction("Order");
        }

        private int CalculateLeftSum(OrderViewModel order)
        {
            int sum = order.TotalSum;
            int paidSum = _paymentLogic.Read(new PaymentBindingModel
            {
                OrderId = order.Id
            }).Select(rec => rec.PaymentAmount).Sum();
            return sum - paidSum;
        }
        public IActionResult SendExcelReport(int id)
        {
            var order = _orderLogic.Read(new OrderBindingModel { Id = id }).FirstOrDefault();
            string fileName = "C:\\Users\\Михан\\Desktop\\data\\" + order.Id + ".xlsx";
            _reportLogic.SaveOrderPositionsToExcelFile(fileName, order, Program.Client.Email);
            return RedirectToAction("Order");
        }
        public IActionResult SendWordReport(int id)
        {
            var order = _orderLogic.Read(new OrderBindingModel { Id = id }).FirstOrDefault();
            string fileName = "C:\\Users\\Михан\\Desktop\\data\\" + order.Id + ".docx";
            _reportLogic.SaveOrderPositionsToWordFile(fileName, order, Program.Client.Email);
            return RedirectToAction("Order");
        }
    }
}