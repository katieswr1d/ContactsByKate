using ContactBook.Core.Entity;
using ContactBook.Core.Services;
using ContactBook.DAL.Repositories;
#pragma warning disable IDE0028
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ContactBook.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            IContactService service = new ContactService(new FileRepository());
            IEnumerable<Contact> contacts;
            Assert.True(service.Clear());
            Assert.True(service.Create("Иван", "Сергеев", new List<string> { "ivan@mail.ru", "ivans@mail.ru" }, new List<string> { "7-927-4325832", "7-937-0004221" }));
            Assert.True(service.Create("Сергей", "Миронов", new List<string> { "sermir@mail.ru", "sermir2020@mail.ru" }, new List<string> { "7-901-3643658", "7-907-4221412", "8-647-3261125" }));
            Assert.True(service.Create("Артем", "Иванов", new List<string> { "artv@mail.ru" }, new List<string> { "7-911-3463464", "7-912-6146442" }));
            Assert.True(service.Create("Руслан", "Минаев", new List<string> { "minrus@mail.ru" }, new List<string> { "7-927-3643400" }));
            Assert.True(service.Create("Наиль", "Минаев", new List<string> { "nailrus@mail.ru" }, new List<string> { "7-927-3355434" }));
            Assert.True(service.Create("Михаил", "Плешаков", new List<string> { "mplesh@mail.ru" }, new List<string> { "7-987-3464611", "8-864-6464887" }));
            Assert.True(service.Create("Сергей", "Плешаков", new List<string> { "seple@mail.ru" }, new List<string> { "7-987-3464310", "8-864-6464887" }));
            Assert.True(service.Create("Артем", "Сергеев", new List<string> { "arser@mail.ru" }, new List<string> { "7-914-3755684", "7-942-4646662" }));
            Assert.True(service.Create("Роман", "Степанов", new List<string> { "roman@mail.ru" }, new List<string> { "7-925-5535581" }));
            Assert.True(service.Create("Сергей", "Степанов", new List<string> { "sergs@mail.ru" }, new List<string> { "7-925-5556344" }));
            Assert.True(service.Create("Ольга", "Миронова", new List<string> { "olmir@mail.ru", "sermir2020@mail.ru" }, new List<string> { "8-647-3261125", "7-917-4121555" }));
            Assert.True(service.Create("Олег", "Минаев", new List<string> { "minaev@mail.ru" }, new List<string> { "8-647-3261125", "7-912-5555134" }));
            contacts = service.ReadAll();
            Assert.Equal(12, contacts.Count());

            contacts = service.FindByFirstName("Сергей");
            Assert.Equal(3, contacts.Count());
            Assert.Equal("Сергей", contacts.ElementAt(0).FirstName);
            Assert.Equal("Сергей", contacts.ElementAt(1).FirstName);
            Assert.Equal("Сергей", contacts.ElementAt(2).FirstName);

            contacts = service.FindByFirstName("Артем");
            Assert.Equal(2, contacts.Count());
            Assert.Equal("Артем", contacts.ElementAt(0).FirstName);
            Assert.Equal("Артем", contacts.ElementAt(1).FirstName);

            contacts = service.FindByLastName("Минаев");
            Assert.Equal(3, contacts.Count());
            Assert.Equal("Минаев", contacts.ElementAt(0).LastName);
            Assert.Equal("Минаев", contacts.ElementAt(1).LastName);
            Assert.Equal("Минаев", contacts.ElementAt(2).LastName);

            contacts = service.FindByPhone("8-647-3261125");
            Assert.Equal(3, contacts.Count());
            Assert.Equal("Миронов", contacts.ElementAt(0).LastName);
            Assert.Equal("Миронова", contacts.ElementAt(1).LastName);
            Assert.Equal("Минаев", contacts.ElementAt(2).LastName);

            contacts = service.FindByAll("Руслан", "Минаев", "7-927-3643400" , "minrus@mail.ru");
            Assert.Single(contacts);
            Assert.Equal("Руслан", contacts.ElementAt(0).FirstName);
            Assert.Equal("Минаев", contacts.ElementAt(0).LastName);

        }
    }
}