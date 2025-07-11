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
        .Produces<object>(200)
        .AllowAnonymous();

    app.MapGet("/v1/books/{id}", GetPublicBookDetailsAsync)
        .WithName("GetV1BookDetails")
        .WithSummary("Get public book details")
        .WithDescription("Returns basic book information for public viewing (no authentication required)")
        .WithOpenApi()
        .Produces<object>(200)
        .Produces(404)
        .AllowAnonymous();
  }

  private static async Task<IResult> GetPublicBooksAsync(IBookService bookService)
  {
    var books = await bookService.GetPublicBooksAsync();
    
    return Results.Ok(new
    {
      message = "Welcome to SharpReads! Here are some featured books:",
      books = books.Select(b => new 
      { 
        id = b.Id, 
        title = b.Title, 
        author = b.Author, 
        genre = b.Genre,
        publishedYear = b.PublishedYear
      }),
      note = "For complete catalog and pricing, please get an API key"
    });
  }

  private static async Task<IResult> GetPublicBookDetailsAsync(string id, IBookService bookService)
  {
    var book = await bookService.GetBookByIdAsync(id);
    
    if (book == null)
    {
      return Results.NotFound(new { message = "Book not found" });
    }

    return Results.Ok(new
    {
      id = book.Id,
      title = book.Title,
      author = book.Author,
      genre = book.Genre,
      description = book.Description ?? "No description available.",
      publishedYear = book.PublishedYear
    });
  }
} 
