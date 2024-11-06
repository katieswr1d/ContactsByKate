using ContactBook.Core.Entity;

namespace ContactBook.DAL;

public interface IRepositoty
{
    IEnumerable<Contact> ReadAll();
    IEnumerable<Contact> FilterContacts(Predicate<Contact> filter);
    Task Create(string FirstName, string LastName, List<string> EmailList, List<string> PhoneNumberList);
    void Clear();
}