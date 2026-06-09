using BookStore.Common;
using BookStore.Model.Entity;
using BookStore.Repository.Common;
using BookStore.Service.Common;

namespace BookStore.Service;

public class BookService : IBookService
{
    private IBookRepository _repository;
    
    public BookService(IBookRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<List<Book>?> GetAllAsync(BookFilter? filter)
    {
        return await _repository.GetAllAsync(filter);
    }

    public async Task<Book?> GetAsync(int id)
    {
        return await _repository.GetAsync(id);
    }

    public async Task<bool> AddAsync(Book book)
    {
        return await _repository.SaveAsync(book);
    }

    public async Task<bool> Update(int id, Book book)
    {
        return await _repository.UpdateAsync(id, book);
    }

    public async Task<bool> Delete(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}