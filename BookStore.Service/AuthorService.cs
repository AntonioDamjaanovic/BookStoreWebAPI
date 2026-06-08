using BookStore.Common;
using BookStore.Model;
using BookStore.Repository;
using BookStore.Service.Common;

namespace BookStore.Service;

public class AuthorService : IAuthorService
{
    public async Task<List<Author>?> GetAllAsync(AuthorFilter? filter)
    {
        AuthorRepository repository = new AuthorRepository();
        return await repository.GetAllAsync(filter);
    }

    public async Task<Author?> GetAsync(int id)
    {
        AuthorRepository repository = new AuthorRepository();
        return await repository.GetAsync(id);
    }

    public async Task<bool> AddAsync(Author author)
    {
        AuthorRepository repository = new AuthorRepository();
        return await repository.SaveAsync(author);
    }

    public async Task<bool> UpdateAsync(int id, Author author)
    {
        AuthorRepository repository = new AuthorRepository();
        return await repository.UpdateAsync(id, author);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        AuthorRepository repository = new AuthorRepository();
        return await repository.DeleteAsync(id);
    }
}