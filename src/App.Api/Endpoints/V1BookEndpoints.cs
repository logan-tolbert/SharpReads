using App.Application.DTOs.Responses;
using App.Application.Interfaces;

namespace App.Api.Endpoints;

public static class V1BookEndpoints
{
  public static void MapV1BookEndpoints(this WebApplication app)
  {
    app.MapGet("/v1/books", GetPublicBooksAsync)
        .WithName("GetV1Books")
        .WithSummary("Get public books catalog")
        .WithDescription("Returns a limited books catalog for public browsing (no authentication required)")
        .WithOpenApi()
        .Produces<PublicBooksResponse>(200)
        .AllowAnonymous();

    app.MapGet("/v1/books/{id}", GetPublicBookDetailsAsync)
        .WithName("GetV1BookDetails")
        .WithSummary("Get public book details")
        .WithDescription("Returns basic book information for public viewing (no authentication required)")
        .WithOpenApi()
        .Produces<PublicBookResponse>(200)
        .Produces(404)
        .AllowAnonymous();
  }

  private static async Task<IResult> GetPublicBooksAsync(IBookService bookService)
  {
    var books = await bookService.GetPublicBooksAsync();
    
    var response = new PublicBooksResponse
    {
      Message = "Welcome to SharpReads! Here are some featured books:",
      Books = books.Select(b => new PublicBookResponse
      { 
        Id = b.Id, 
        Title = b.Title, 
        Author = b.Author, 
        Genre = b.Genre,
        PublishedYear = b.PublishedYear
      }).ToList(),
      Note = "For complete catalog and pricing, please get an API key"
    };
    
    return Results.Ok(response);
  }

  private static async Task<IResult> GetPublicBookDetailsAsync(string id, IBookService bookService)
  {
    var book = await bookService.GetBookByIdAsync(id);
    
    if (book == null)
    {
      return Results.NotFound(new { message = "Book not found" });
    }

    var response = new PublicBookResponse
    {
      Id = book.Id,
      Title = book.Title,
      Author = book.Author,
      Genre = book.Genre,
      PublishedYear = book.PublishedYear
    };
    
    return Results.Ok(response);
  }
} 
