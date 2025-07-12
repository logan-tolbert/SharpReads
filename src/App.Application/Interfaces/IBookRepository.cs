using App.Core.Entities;

namespace App.Application.Interfaces;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(int id);
    Task<Book?> GetByTitleAsync(string title);
    Task<IEnumerable<Book>> GetByGenreAsync(string genre);
    Task<IEnumerable<Book>> GetByAuthorAsync(string author);
} 
