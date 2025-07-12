using App.Application.Interfaces;
using App.Core.Entities;
using App.Infrastructure.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace App.Infrastructure.Tests.Services;

public class BookServiceTests
{
  private readonly BookService _sut;
  private readonly IBookRepository _repo = Substitute.For<IBookRepository>();
  public BookServiceTests()
  {
    _sut = new BookService(_repo);
  }

  [Fact]
  public async Task GetAllBooks_ShouldReturnEmptyEnumerable_WhenNoBooksExistAsync()
  {
    // Arrange
    _repo.GetAllAsync().Returns([]);

    // Act
    var books = await _sut.GetAllBooksAsync();

    // Assert
    books.Should().BeEmpty();
  }

  [Fact]
  public async Task GetAllBooks_ShouldReturnEnumerableOfBooks_WhenBooksExistAsync()
  {
    // Arrange
    var expected = new[]
    {
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
    };

    _repo.GetAllAsync().Returns(expected);

    // Act
    var books = await _repo.GetAllAsync();

    // Assert
    books.Should().NotBeEmpty();
    books.Should().HaveCount(10);
  }
}
