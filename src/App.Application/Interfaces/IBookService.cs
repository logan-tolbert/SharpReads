using App.Core.Entities;

namespace App.Application.Interfaces;

public interface IBookService
{
    Task<IEnumerable<Book>> GetPublicBooksAsync();
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book?> GetBookByIdAsync(int id);
    Task<Book?> GetBookByTitleAsync(string title);
    Task<IEnumerable<Book>> GetBooksByGenreAsync(string genre);
    Task<IEnumerable<Book>> GetBooksByAuthorAsync(string author);
} 
