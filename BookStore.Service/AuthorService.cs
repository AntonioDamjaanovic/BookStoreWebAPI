using BookStore.Common;
using BookStore.Model;
using BookStore.Repository;
using BookStore.Service.Common;

namespace BookStore.Service;

public class AuthorService : IAuthorService
{
    public List<Author>? GetAll(AuthorFilter? filter)
    {
        AuthorRepository repository = new AuthorRepository();
        return repository.GetAll(filter);
    }

    public Author? Get(int id)
    {
        AuthorRepository repository = new AuthorRepository();
        return repository.Get(id);
    }

    public bool Add(Author author)
    {
        AuthorRepository repository = new AuthorRepository();
        return repository.Save(author);
    }

    public bool Update(int id, Author author)
    {
        AuthorRepository repository = new AuthorRepository();
        return repository.Update(id, author);
    }

    public bool Delete(int id)
    {
        AuthorRepository repository = new AuthorRepository();
        return repository.Delete(id);
    }
}