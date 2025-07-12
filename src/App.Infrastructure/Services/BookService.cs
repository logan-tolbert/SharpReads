using App.Application.Interfaces;
using App.Core.Entities;
using Logging;
using Microsoft.Extensions.Logging;

namespace App.Infrastructure.Services;

public class BookService : IBookService
{
    private readonly ILoggerAdapter<BookService> _logger;
    private readonly IBookRepository _repo;

  public BookService(ILoggerAdapter<BookService> logger, IBookRepository repo)
    {
    _logger = logger;
        _repo = repo;
    }

  public async Task<IEnumerable<Book>> GetPublicBooksAsync()
    {
        // Return a limited set for public viewing (no pricing info)
        var allBooks = await _repo.GetAllAsync();
        var publicBooks = allBooks.Take(5).Select(b => new Book
        {
            Id = b.Id,
            Title = b.Title,
            Author = b.Author,
            Genre = b.Genre,
            PublishedYear = b.PublishedYear,
            Description = b.Description,
            IsAvailable = b.IsAvailable,
            CreatedAt = b.CreatedAt
            // Note: Price is excluded for public view
        });
        
        return publicBooks;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        
        try
        {
            _logger.LogInformation("Info: Retrieving all books.");
            
            var books = await _repo.GetAllAsync();
            
            stopwatch.Stop();
            _logger.LogInformation("Info: Books retrieved in {0}ms", stopwatch.ElapsedMilliseconds);
            
            return books;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error: An error occurred while retrieving all books.");
            throw;
        }
    }

    public Task<Book?> GetBookByIdAsync(int id)
    {
        return _repo.GetByIdAsync(id);
    }

    public Task<Book?> GetBookByTitleAsync(string title)
    {
        return _repo.GetByTitleAsync(title);
    }

    public Task<IEnumerable<Book>> GetBooksByGenreAsync(string genre)
    {
        return _repo.GetByGenreAsync(genre);
    }

    public Task<IEnumerable<Book>> GetBooksByAuthorAsync(string author)
    {
        return _repo.GetByAuthorAsync(author);
    }
} 
