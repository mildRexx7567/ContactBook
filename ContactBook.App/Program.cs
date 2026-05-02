using ContactBook.Domain.Entities;
using ContactBook.Infrastructure.Repositories;

class Program
{
    static IContactRepository repo = new InMemoryContactRepository();

    static void Main()
    {
        ShowWelcome();
        MainLoop();
    }

    static void ShowWelcome()
    {
        Console.WriteLine("=== CONTACT BOOK ===");
    }

    static void MainLoop()
    {
        while (true)
        {
            ShowMenu();
            HandleInput();
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("\n1. Show Contacts");
        Console.WriteLine("2. Create Contact");
        Console.WriteLine("3. Find Contact");
        Console.WriteLine("4. Sort Contacts");
        Console.WriteLine("0. Exit");
    }

    static void HandleInput()
    {
        var input = Console.ReadLine();

        switch (input)
        {
            case "1": ShowContacts(); break;
            case "2": CreateContact(); break;
            case "3": FindContacts(); break;
            case "4": SortContacts(); break;
            case "0": Exit(); break;
            default: Console.WriteLine("Invalid option"); break;
        }
    }

    // -------------------------
    // SHOW CONTACTS (pagination)
    // -------------------------
    static void ShowContacts()
    {
        var contacts = repo.GetAll();

        if (!contacts.Any())
        {
            Console.WriteLine("No contacts");
            return;
        }

        int page = 1;
        int pageSize = 5;

        var pageData = contacts
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        foreach (var c in pageData)
            Console.WriteLine(c);
    }

    // -------------------------
    // CREATE CONTACT
    // -------------------------
   static void CreateContact()
{
    var name = ReadRequired("Name: ");

    Console.Write("Email: ");
    var email = Console.ReadLine() ?? "";

    Console.Write("Phone: ");
    var phone = Console.ReadLine() ?? "";

    var contact = new Contact(name, email, phone);

    repo.Add(contact);

    Console.WriteLine("✔ Contact created");
}

    // -------------------------
    // FIND CONTACT
    // -------------------------
    static void FindContacts()
    {
        Console.Write("Search: ");
        var keyword = Console.ReadLine();

        var results = repo.GetAll()
            .FindAll(c => c.Name.Contains(keyword!, StringComparison.OrdinalIgnoreCase));

        foreach (var c in results)
            Console.WriteLine(c);
    }

    // -------------------------
    // SORT CONTACTS
    // -------------------------
    static void SortContacts()
    {
        var contacts = repo.GetAll();

        contacts.Sort(new ContactNameComparer());

        Console.WriteLine("Sorted:");
        foreach (var c in contacts)
            Console.WriteLine(c);
    }

    // -------------------------
    // HELPERS
    // -------------------------
    static string ReadRequired(string message)
    {
        Console.Write(message);
        var input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Invalid input");
            return ReadRequired(message);
        }

        return input;
    }

    static void Exit()
    {
        Console.WriteLine("Goodbye!");
        Environment.Exit(0);
    }
}

// -------------------------
// COMPARER (IComparer)
// -------------------------
class ContactNameComparer : IComparer<Contact>
{
    public int Compare(Contact? x, Contact? y)
        => string.Compare(x?.Name, y?.Name);
}
