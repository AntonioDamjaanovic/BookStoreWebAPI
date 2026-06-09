using BookStore.Common;
using BookStore.Model;

namespace BookStore.Service.Common;

public interface IBookService
{
    public Task<List<Book>?> GetAllAsync(BookFilter? filter);
    public Task<Book?> GetAsync(int id);
    public Task<bool> AddAsync(Book book);
    public Task<bool> Update(int id, Book book);
    public Task<bool> Delete(int id);
}