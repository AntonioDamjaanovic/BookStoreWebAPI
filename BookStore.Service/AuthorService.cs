using BookStore.Common;
using BookStore.Model.Entity;
using BookStore.Repository.Common;
using BookStore.Service.Common;

namespace BookStore.Service;

public class AuthorService : IAuthorService
{
    private IAuthorRepository _repository;

    public AuthorService(IAuthorRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<List<Author>?> GetAllAsync(AuthorFilter? filter)
    {
        return await _repository.GetAllAsync(filter);
    }

    public async Task<Author?> GetAsync(int id)
    {
        return await _repository.GetAsync(id);
    }

    public async Task<bool> AddAsync(Author author)
    {
        return await _repository.SaveAsync(author);
    }

    public async Task<bool> UpdateAsync(int id, Author author)
    {
        return await _repository.UpdateAsync(id, author);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}