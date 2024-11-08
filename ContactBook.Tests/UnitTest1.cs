using ContactBook.Core.Entity;
using ContactBook.Core.Services;
using ContactBook.DAL.Repositories;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: TestCollectionOrderer(ordererTypeName: "ContactBook.Tests.Orderers.DisplayNameOrderer", ordererAssemblyName: "ContactBook.Tests")]
namespace ContactBook.Tests
{
    [Collection("A Test Collection")]
    public class ContactServiceTestA
    {
        public IContactService service { get; set; }
        public ContactServiceTestA()
        {
            service = new ContactService(new FileRepository());
        }
        [Fact]
        public void TestClear()
        {
            Assert.True(service.Clear());
        }
    }
    [Collection("B Test Collection")]
    public class ContactServiceTestB
    {
        public IContactService service { get; set; }
        public ContactServiceTestB()
        {
            service = new ContactService(new FileRepository());
        }
        [Theory]
        [MemberData(nameof(PersonData))]
        public void TestCreateContact(string FirstName, string LastName, List<string> email, List<string> phoneNumber)//
        {
            Assert.True(service.Create(FirstName, LastName, email, phoneNumber));//
        }
        public static IEnumerable<object[]> PersonData =>
            new List<object[]>
            {
                new object[] {"Ivan", "Sergeev", new List<string> { "ivan@mail.ru", "ivans@mail.ru" }, new List<string> { "7-927-4325832", "7-937-0004221" }},
                new object[] {"Sergey", "Mironov", new List<string> { "sermir@mail.ru", "sermir2020@mail.ru" }, new List<string> { "7-901-3643658", "7-907-4221412", "8-647-3261125" } },
                new object[] {"Artem", "Ivanov", new List<string> { "artv@mail.ru" }, new List<string> { "7-911-3463464", "7-912-6146442" }},
                new object[] {"Ruslan", "Minaev", new List<string> { "minrus@mail.ru" }, new List<string> { "7-927-3643400" } },
                new object[] {"Nail", "Minaev", new List<string> { "nailrus@mail.ru" }, new List<string> { "7-927-3355434" }},
                new object[] {"Michael", "Pleshakov", new List<string> { "mplesh@mail.ru" }, new List<string> { "7-987-3464611", "8-864-6464887" }},
                new object[] {"Sergey", "Pleshakov", new List<string> { "seple@mail.ru" }, new List<string> { "7-987-3464310", "8-864-6464887" }},
                new object[] {"Artem", "Sergeev", new List<string> { "arser@mail.ru" }, new List<string> { "7-914-3755684", "7-942-4646662" }},
                new object[] {"Roman", "Stepanov", new List<string> { "roman@mail.ru" }, new List<string> { "7-925-5535581" }},
                new object[] {"Sergey", "Stepanov", new List<string> { "sergs@mail.ru" }, new List<string> { "7-925-5556344" }},
                new object[] {"Olga", "Mironova", new List<string> { "olmir@mail.ru", "sermir2020@mail.ru" }, new List<string> { "8-647-3261125", "7-917-4121555" }},
                new object[] {"Oleg", "Minaev", new List<string> { "minaev@mail.ru" }, new List<string> { "8-647-3261125", "7-912-5555134" }}
            };
    }
    [Collection("C Test Collection")]
    public class ContactServiceTestC
    {
        public IContactService service { get; set; }
        public ContactServiceTestC()
        {
            service = new ContactService(new FileRepository());
        }
        [Theory]
        [InlineData(12)]
        public void TestReadAll(int num)
        {
            IEnumerable<Contact> contacts = service.ReadAll();
            Assert.Equal(num, contacts.Count());
        }
        [Theory]
        [InlineData("Sergey", 3)]
        public void TestFindByFirstName(string Name, int num)
        {
            IEnumerable<Contact> contacts = service.FindByFirstName(Name);
            Assert.Equal(num, contacts.Count());
            for (int i = 0; i < num; i++)
            {
                Assert.Equal(Name, contacts.ElementAt(i).FirstName);
            }
        }
        [Theory]
        [InlineData("Minaev", 3)]
        public void TestFindByLastName(string Name, int num)
        {
            IEnumerable<Contact> contacts = service.FindByLastName(Name);
            Assert.Equal(num, contacts.Count());
            for (int i = 0; i < num; i++)
            {
                Assert.Equal(Name, contacts.ElementAt(i).LastName);
            }
        }
        [Theory]
        [InlineData("8-647-3261125",new string[3]{ "Mironov", "Mironova", "Minaev" })]
        public void TestFindByLastPhone(string Phone, string[] names)
        {
            IEnumerable<Contact> contacts = service.FindByPhone(Phone);
            Assert.Equal(names.Length, contacts.Count());
            for (int i = 0; i < names.Length; i++)
            {
                Assert.Equal(names[i], contacts.ElementAt(i).LastName);
            }
        }
        [Theory]
        [InlineData("Ruslan", "Minaev", "7-927-3643400", "minrus@mail.ru")]
        public void TestFindByAll(string Name, string LastName, string Phone, string Email)
        {
            IEnumerable<Contact> contacts = service.FindByAll(Name, LastName, Phone, Email);
            Assert.Single(contacts);
            Assert.Equal(Name, contacts.ElementAt(0).FirstName);
            Assert.Equal(LastName, contacts.ElementAt(0).LastName);
        }

    }
}
/*using ContactBook.Core.Entity;
using ContactBook.Core.Services;
using ContactBook.DAL.Repositories;
#pragma warning disable IDE0028
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Moq;

// можно было сделать несколько функций для теста,
// я одну сделала, потому что один набор данных,
// по которому делается 4 варианта поиска.
// смысла нет на 4 делить, либо надо разные наборы данных делать.

namespace ContactBook.Tests
{
    public class UnitTest1
    {
        [Fact]
        // rename
        public void Test1()
        {
            // moq File Repository
            // prepare setup -> File Repository
            // separate this shit
            IContactService service = new ContactService(new FileRepository());
            IEnumerable<Contact> contacts;
            
            
            
            
            Assert.True(service.Clear());
            
            // [Theory]
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
            
            // separete

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
}*/