using ContactBook.Domain.Entities;

namespace ContactBook.Infrastructure.Repositories;

public class InMemoryContactRepository : IContactRepository
{
    private readonly List<Contact> _contacts = new();

    public void Add(Contact contact)
    {
        _contacts.Add(contact);
    }

    public List<Contact> GetAll()
    {
        return _contacts;
    }
}