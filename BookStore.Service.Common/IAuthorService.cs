using BookStore.Common;
using BookStore.Model;

namespace BookStore.Service.Common;

public interface IAuthorService
{
    public Task<List<Author>?> GetAllAsync(AuthorFilter? filter);
    public Task<Author?> GetAsync(int id);
    public Task<bool> AddAsync(Author author);
    public Task<bool> UpdateAsync(int id, Author author);
    public Task<bool> DeleteAsync(int id);
}