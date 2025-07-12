using App.Application.Interfaces;
using App.Core.Entities;
using Microsoft.Extensions.Logging;

namespace App.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly ILogger<BookRepository> _logger;
    private readonly List<Book> _books;

    public BookRepository(ILogger<BookRepository> logger)
    {
        _logger = logger;
        
        // Initialize with sample data - this would come from a database in production
        _books =
        [
            new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Genre = "Classic", PublishedYear = 1925, Price = 12.99m, IsAvailable = true, CreatedAt = DateTime.UtcNow },
            new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", Genre = "Classic", PublishedYear = 1960, Price = 14.99m, IsAvailable = true, CreatedAt = DateTime.UtcNow },
            new Book { Id = 3, Title = "1984", Author = "George Orwell", Genre = "Dystopian", PublishedYear = 1949, Price = 11.99m, IsAvailable = true, CreatedAt = DateTime.UtcNow },
            new Book { Id = 4, Title = "Pride and Prejudice", Author = "Jane Austen", Genre = "Romance", PublishedYear = 1813, Price = 9.99m, IsAvailable = true, CreatedAt = DateTime.UtcNow },
            new Book { Id = 5, Title = "The Hobbit", Author = "J.R.R. Tolkien", Genre = "Fantasy", PublishedYear = 1937, Price = 16.99m, IsAvailable = true, CreatedAt = DateTime.UtcNow },
            new Book { Id = 6, Title = "The Catcher in the Rye", Author = "J.D. Salinger", Genre = "Coming-of-age", PublishedYear = 1951, Price = 13.99m, IsAvailable = true, CreatedAt = DateTime.UtcNow },
            new Book { Id = 7, Title = "Lord of the Flies", Author = "William Golding", Genre = "Allegory", PublishedYear = 1954, Price = 10.99m, IsAvailable = true, CreatedAt = DateTime.UtcNow },
            new Book { Id = 8, Title = "Animal Farm", Author = "George Orwell", Genre = "Satire", PublishedYear = 1945, Price = 8.99m, IsAvailable = true, CreatedAt = DateTime.UtcNow },
            new Book { Id = 9, Title = "The Lord of the Rings", Author = "J.R.R. Tolkien", Genre = "Fantasy", PublishedYear = 1954, Price = 24.99m, IsAvailable = true, CreatedAt = DateTime.UtcNow },
            new Book { Id = 10, Title = "Brave New World", Author = "Aldous Huxley", Genre = "Dystopian", PublishedYear = 1932, Price = 12.99m, IsAvailable = true, CreatedAt = DateTime.UtcNow }
        ];
    }

    public Task<IEnumerable<Book>> GetAllAsync()
    {
        return Task.FromResult(_books.AsEnumerable());
    }

    public Task<Book?> GetByIdAsync(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        return Task.FromResult(book);
    }

    public Task<Book?> GetByTitleAsync(string title)
    {
        var book = _books.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(book);
    }

    public Task<IEnumerable<Book>> GetByGenreAsync(string genre)
    {
        var books = _books.Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(books);
    }

    public Task<IEnumerable<Book>> GetByAuthorAsync(string author)
    {
        var books = _books.Where(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(books);
    }
} 
