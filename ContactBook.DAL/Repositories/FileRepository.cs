using System.Data;
using System.Text.Json;
using ContactBook.Core.Entity;

namespace ContactBook.DAL.Repositories;

public class FileRepository : IRepositoty
{
    private const string ContactsJson = "contacts.json";
    private List<Contact> _contacts;
    
    public FileRepository()
    {
        InitializeAsync().GetAwaiter().GetResult();
    }
    public void Clear()
    {
        _contacts.Clear();
        File.WriteAllTextAsync(ContactsJson, JsonSerializer.Serialize(_contacts));//запись и конвертация контакта на диск в определенном формате 
    }

    private async Task InitializeAsync()
    {
        _contacts = await LoadContactsFromFile();
    }

    private async Task<List<Contact>> LoadContactsFromFile()
    {
        if (!File.Exists(ContactsJson))
        {
            return []; // возвращаем пустой список, если файл не существует
        }
        else
        {
            var json = await File.ReadAllTextAsync(ContactsJson);
            try
            {
                return JsonSerializer.Deserialize<List<Contact>>(json)!; // десериализуем файл в коллекцию
            }
            catch
            {
                return [];
            }
        }
    }
    private int MaxID()
    {
        int max = 1;
        for (int i = 0; i< _contacts.Count; i++)
        {
            if (_contacts[i].Id > max)
            {
                max = _contacts[i].Id;
            }
        }
        return max;
    }

    public IEnumerable<Contact> ReadAll() //метод для чтения всех контактов (конкретный репозиторий, который читает данные из конкретного места)
    {
        return _contacts;
    }

    public IEnumerable<Contact> FilterContacts(Predicate<Contact> filter)
    {
        return _contacts.Where(contact => filter(contact)).ToList();
    }
    public async Task Create(string FirstName, string LastName, List<string> EmailList, List<string> PhoneNumberList)
    {
        _contacts.Add(new Contact(MaxID()+1,FirstName, LastName, EmailList, PhoneNumberList));
        await File.WriteAllTextAsync(ContactsJson, JsonSerializer.Serialize(_contacts));//запись и конвертация контакта на диск в определенном формате 
        
    }
}