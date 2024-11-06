namespace ContactBook.Core.Entity;

public class Contact
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;//чтобы была статическая пустая строка а не создавалось миллион этих строк
    public string LastName { get; set; } = string.Empty;
    public List<Email> EmailList { get; set; } = [];
    public List<PhoneNumber> PhoneNumberList { get; set; } = [];
    public Contact() { }
    public Contact(int Id, string FirstName, string LastName, List<string> email, List<string> phoneNumber)
    {
        this.Id = Id;
        this.FirstName = FirstName;
        this.LastName = LastName;
        for (int i=0;i< email.Count; i++)
        {
            EmailList.Add(new Email(i + 1, email[i]));
        }
        for (int i = 0; i < phoneNumber.Count; i++)
        {
            PhoneNumberList.Add(new PhoneNumber(phoneNumber[i]));
        }
    }
    public override string ToString() {
        return "First Name: " + FirstName + "\nLast Name: " + LastName + 
               "\nEmails: " + string.Join("\n", EmailList) + "\nPhoneNumbers: " + 
               string.Join("\n", PhoneNumberList);
    }
}