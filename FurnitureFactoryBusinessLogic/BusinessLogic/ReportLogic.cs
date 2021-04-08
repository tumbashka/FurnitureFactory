using FurnitureFactoryBusinessLogic.BindingModels;
using FurnitureFactoryBusinessLogic.HelperModels;
using FurnitureFactoryBusinessLogic.Interfaces;
using FurnitureFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace FurnitureFactoryBusinessLogic.BusinessLogic
{
    public class ReportLogic
    {

        private readonly IPositionLogic positionLogic;
        private readonly IOrderLogic orderLogic;
        private readonly IFurnitureModelLogic furnitureModelLogic;
        private readonly IPaymentLogic paymentLogic;
        public ReportLogic(IPositionLogic positionLogic, IOrderLogic orderLogic, IFurnitureModelLogic furnitureModelLogic, IPaymentLogic paymentLogic)
        {
            this.positionLogic = positionLogic;
            this.orderLogic = orderLogic;
            this.furnitureModelLogic = furnitureModelLogic;
            this.paymentLogic = paymentLogic;
        }
        public List<FurnitureModelViewModel> GetPositions(OrderViewModel order)
        {
            var furnitureModel = new List<FurnitureModelViewModel>();

            foreach (var model in order.Positions)
            {
                furnitureModel.Add(furnitureModelLogic.Read(new FurnitureModelBindingModel
                {
                    Id = model.FurnitureModelId
                }).FirstOrDefault());

            }
            return furnitureModel;
        }
        public Dictionary<int, FurnitureModelViewModel> GetPositionsForExcel(OrderViewModel order)
        {
            var furnitureModel = new Dictionary<int, FurnitureModelViewModel>();
            var positions = positionLogic.Read(new PositionBindingModel { OrderId = order.Id });
            foreach (var pos in positions)
            {
                foreach (var model in order.Positions)
                {
                    if (pos.FurnitureModelId == model.FurnitureModelId)
                        furnitureModel.Add(pos.Count,furnitureModelLogic.Read(new FurnitureModelBindingModel
                        {
                            Id = model.FurnitureModelId
                        }).FirstOrDefault());

                }
            }
            return furnitureModel;
        }
        public Dictionary<int, List<PaymentViewModel>> GetOrderPayments(OrderBindingModel model)
        {
            var orders = orderLogic.Read(model).ToList();
            Dictionary<int, List<PaymentViewModel>> payments = new Dictionary<int, List<PaymentViewModel>>();
            foreach (var order in orders)
            {
                var orderPayments = paymentLogic.Read(new PaymentBindingModel { OrderId = order.Id }).ToList();
                payments.Add(order.Id, orderPayments);
            }
            return payments;
        }
        public void SaveOrderPaymentsToPdfFile(string fileName, OrderBindingModel order, string email)
        {
            string title = "Список платежей в период с " + order.Date.ToString() + " по " + order.DateTo.ToString();
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = fileName,
                Title = title,
                Orders = orderLogic.Read(order).ToList(),
                Payments = GetOrderPayments(order)
            });
            if (email != null)
                SendMail(email, fileName, title);
        }
        public void SaveOrderPositionsToWordFile(string fileName, OrderViewModel order, string email)
        {
            string title = "Список мебели в заказе №" + order.Id;
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = fileName,
                Title = title,
                Models = GetPositionsForExcel(order)
            });
            SendMail(email, fileName, title);
        }
        public void SaveOrderPositionsToExcelFile(string fileName, OrderViewModel order, string email)
        {
            string title = "Список мебели в заказе №" + order.Id;
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = fileName,
                Title = title,
                Models = GetPositionsForExcel(order)
            });
            SendMail(email, fileName, title);
        }
        public void SendMail(string email, string fileName, string subject)
        {
            MailAddress from = new MailAddress("iselabapost@gmail.com", "Фабрика мебелия «Мягкое место»");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = subject;
            m.Attachments.Add(new Attachment(fileName));
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("iselabapost@gmail.com", "12345Misha!");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}
