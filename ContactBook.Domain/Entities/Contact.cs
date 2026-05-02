using System.Diagnostics;

namespace ContactBook.Domain.Entities;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public class Contact : IEquatable<Contact>
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";

    public Contact(string name, string email = "", string phone = "")
    {
        Name = name;
        Email = email;
        Phone = phone;
    }

    public override string ToString()
        => $"{Name} | {Email} | {Phone}";

    public override bool Equals(object? obj)
        => Equals(obj as Contact);

    public bool Equals(Contact? other)
        => other != null &&
           Email == other.Email &&
           Phone == other.Phone;

    public override int GetHashCode()
        => HashCode.Combine(Email, Phone);

    public static bool operator ==(Contact? a, Contact? b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (a is null || b is null) return false;
        return a.Equals(b);
    }

    public static bool operator !=(Contact? a, Contact? b)
        => !(a == b);

    private string GetDebuggerDisplay()
        => ToString();
}