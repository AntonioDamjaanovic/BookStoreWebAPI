using BookStore.Common;
using BookStore.Model;
using BookStore.Repository;
using BookStore.Service.Common;

namespace BookStore.Service;

public class BookService : IBookService
{
    public List<Book>? GetAll(BookFilter? filter)
    {
        BookRepository repository = new BookRepository();
        return repository.GetAll(filter);
    }

    public Book? Get(int id)
    {
        BookRepository repository = new BookRepository();
        return repository.Get(id);
    }

    public bool Add(Book book)
    {
        BookRepository repository = new BookRepository();
        return repository.Save(book);
    }

    public bool Update(int id, Book book)
    {
        BookRepository repository = new BookRepository();
        return repository.Update(id, book);
    }

    public bool Delete(int id)
    {
        BookRepository repository = new BookRepository();
        return repository.Delete(id);
    }
}