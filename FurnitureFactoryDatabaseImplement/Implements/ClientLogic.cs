using FurnitureFactoryBusinessLogic.BindingModels;
using FurnitureFactoryBusinessLogic.Interfaces;
using FurnitureFactoryBusinessLogic.ViewModels;
using FurnitureFactoryDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FurnitureFactoryDatabaseImplement.Implements
{
    public class ClientLogic : IClientLogic
    {
        public void CreateOrUpdate(ClientBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                Client element = model.Id.HasValue ? null : new Client();
                if (model.Id.HasValue)
                {
                    element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Client();
                    context.Clients.Add(element);
                }
                element.FirstName = model.FirstName;
                element.SecondName = model.SecondName;
                element.Patronymic = model.Patronymic;
                element.PhoneNumber = model.PhoneNumber;
                element.Email = model.Email;
                element.Block = model.Block;
                element.Password = model.Password;
                context.SaveChanges();
            }
        }

        public void Delete(ClientBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                Client element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);

                if (element != null)
                {
                    context.Clients.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public List<ClientViewModel> Read(ClientBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                return context.Clients
                 .Where(rec => model == null
                   || rec.Id == model.Id
                 ||rec.Email == model.Email
                        && (model.Password == null || rec.Password == model.Password))
               .Select(rec => new ClientViewModel
               {
                   Id = rec.Id,
                   FirstName = rec.FirstName,
                   SecondName = rec.SecondName,
                   Patronymic = rec.Patronymic,
                   Email = rec.Email,
                   Password = rec.Password,
                   PhoneNumber = rec.PhoneNumber,
                   Block = rec.Block
               })
                .ToList();
            }
        }
    }
}
