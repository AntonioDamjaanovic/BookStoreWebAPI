using BookStore.Common;
using BookStore.Model.Entity;

namespace BookStore.Repository.Common;

public interface IBookRepository
{
    public Task<List<Book>?> GetAllAsync(BookFilter? filter);
    public Task<Book?> GetAsync(int id);
    public Task<bool> SaveAsync(Book book);
    public Task<bool> UpdateAsync(int id, Book book);
    public Task<bool> DeleteAsync(int id);
}