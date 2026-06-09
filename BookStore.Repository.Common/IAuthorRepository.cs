using BookStore.Common;
using BookStore.Model;

namespace BookStore.Repository.Common;

public interface IAuthorRepository
{
    public Task<List<Author>?> GetAllAsync(AuthorFilter? filter);
    public Task<Author?> GetAsync(int id);
    public Task<bool> SaveAsync(Author author);
    public Task<bool> UpdateAsync(int id, Author author);
    public Task<bool> DeleteAsync(int id);
}