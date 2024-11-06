namespace ContactBook.Core.Entity;

public class PhoneNumber
{
    public string Value { get; set; }
    public PhoneNumber(string Value)
    {
        this.Value = Value;
    }
    public override string ToString() {
        return Value;
    }
}