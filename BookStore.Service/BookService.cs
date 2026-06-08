using BookStore.Common;
using BookStore.Model;
using BookStore.Repository;
using BookStore.Service.Common;

namespace BookStore.Service;

public class BookService : IBookService
{
    public async Task<List<Book>?> GetAllAsync(BookFilter? filter)
    {
        BookRepository repository = new BookRepository();
        return await repository.GetAllAsync(filter);
    }

    public async Task<Book?> GetAsync(int id)
    {
        BookRepository repository = new BookRepository();
        return await repository.GetAsync(id);
    }

    public async Task<bool> AddAsync(Book book)
    {
        BookRepository repository = new BookRepository();
        return await repository.SaveAsync(book);
    }

    public async Task<bool> Update(int id, Book book)
    {
        BookRepository repository = new BookRepository();
        return await repository.UpdateAsync(id, book);
    }

    public async Task<bool> Delete(int id)
    {
        BookRepository repository = new BookRepository();
        return await repository.DeleteAsync(id);
    }
}