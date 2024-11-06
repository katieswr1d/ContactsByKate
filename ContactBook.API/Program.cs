using ContactBook.Core.Entity;
using ContactBook.Core.Services;
using ContactBook.DAL.Repositories;
using System.Numerics;

namespace ContactBook.API;

class Program
{
    static void Main(string[] args)
    {
        IContactService service = new ContactService(new FileRepository());
        
        string option = string.Empty;
        while (option != "0")
        {
            Console.WriteLine("Выберите действие:\n" +
                              "1. Вывести все контакты\n" +
                              "2. Поиск контакта\n" +
                              "3. Добавить новый контакт\n" +
                              "0. Завершить работу");

            option = Console.ReadLine()!;
            switch (option)
            {
                case "1":
                    var contacts = service.ReadAll();
                    Console.WriteLine(string.Join("\n", contacts));
                    break;

                case "2":
                    Console.WriteLine("Выберите опцию:\n" +
                                      "1. Поиск по всем параметрам\n" +
                                      "2. Поиск по конкретному полю");
                    var findOption = Console.ReadLine();
                    if (findOption == "1")
                    {
                        Console.WriteLine("Введите имя, фамилию,  номер телефона  и email контакта  ");
                        var name = Console.ReadLine()!;
                        var surname = Console.ReadLine()!;
                        var number = Console.ReadLine()!;
                        var mail = Console.ReadLine()!;
                        Console.WriteLine(
                            $"Found contacts:\n {string.Join('\n', service.FindByAll(name, surname, number, mail!))}");
                    }

                    if (findOption == "2")
                    {
                        Console.WriteLine("Выберите действие:\n" +
                                          "1. Поиск по имени\n" +
                                          "2. Поиск по фамилии\n" +
                                          "3. Поиск по email\n" +
                                          "4. Поиск по номеру телефона");
                        var parameterOption = Console.ReadLine();
                        switch (parameterOption)
                        {
                            case "1":
                                Console.WriteLine("Введите имя контакта  ");
                                Console.WriteLine(
                                    $"Found contacts:\n {string.Join('\n', service.FindByFirstName(Console.ReadLine()!))}");
                                break;
                            case "2":
                                Console.WriteLine("Введите фамилию контакта  ");
                                Console.WriteLine(
                                    $"Found contacts:\n {string.Join('\n', service.FindByLastName(Console.ReadLine()!))}");
                                break;
                            case "3":
                                Console.WriteLine("Введите email ");
                                Console.WriteLine(
                                    $"Found contacts:\n {string.Join('\n', service.FindByEmail(Console.ReadLine()!))}");
                                break;
                            case "4":
                                Console.WriteLine("Введите номер телефона  ");
                                Console.WriteLine(
                                    $"Found contacts:\n {string.Join('\n', service.FindByPhone(Console.ReadLine()!))}");
                                break;
                        }
                    }

                    break;

                case "3":
                    Console.WriteLine("Введите имя контакта ");
                    string FirstName = Console.ReadLine()!;

                    Console.WriteLine("Введите фамилию контакта ");
                    string LastName = Console.ReadLine()!;

                    Console.WriteLine("Введите email(0 если закончить) ");
                    List<string> EmailList = new();
                    string email = Console.ReadLine()!;
                    while (email != "0")
                    {
                        if (email.Length > 0) { EmailList.Add(email); }
                        email = Console.ReadLine()!;
                    }

                    Console.WriteLine("Введите номер телефона (0 если закончить) ");
                    List<string> PhoneList = new();
                    string phone = Console.ReadLine()!;
                    while (phone != "0")
                    {
                        if (phone.Length > 0) { PhoneList.Add(phone); }
                        phone = Console.ReadLine()!;
                    }

                    service.Create(FirstName!, LastName!, EmailList, PhoneList);
                    break;

                case "0":

                    break;
                default:
                    break;
            }

            option = Console.ReadLine()!;
        }
    }
}