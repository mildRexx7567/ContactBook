using ContactBook.Domain.Entities;

namespace ContactBook.Infrastructure.Repositories;

public interface IContactRepository
{
    void Add(Contact contact);
    List<Contact> GetAll();
}