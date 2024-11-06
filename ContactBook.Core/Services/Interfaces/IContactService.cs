using ContactBook.Core.Entity;

namespace ContactBook.Core.Services;

public interface IContactService
{
    IEnumerable<Contact> ReadAll();
    bool Create(string FirstName, string LastName, List<string> email, List<string> phoneNumber);
    IEnumerable<Contact> FindByAll(string firstName, string lastName, string phoneNumber, string email);
    
    IEnumerable<Contact> FindByFirstName(string firstName);
    IEnumerable<Contact> FindByLastName(string lastName);
    
    //  метод для поиска по номеру телефона
    IEnumerable<Contact> FindByPhone(string phoneNumber);
    IEnumerable<Contact> FindByEmail(string email);
    bool Clear();
}