using App.Application.Interfaces;
using System.Security.Claims;

namespace App.Api.Endpoints;

public static class V2BookEndpoints
{
  public static void MapV2BookEndpoints(this WebApplication app)
  {
    app.MapGet("/v2/books", GetBooksAsync)
        .WithName("GetV2Books")
        .WithSummary("Get books catalog (authenticated)")
        .WithDescription("Returns the complete books catalog with pricing and availability (requires valid API key)")
        .WithOpenApi()
        .Produces<object>(200)
        .Produces(401)
        .RequireAuthorization();
  }

  private static async Task<IResult> GetBooksAsync(ClaimsPrincipal user, IBookService bookService)
  {
    var clientName = user.FindFirst("ClientName")?.Value;
    var books = await bookService.GetAllBooksAsync();
    
    return Results.Ok(new
    {
      message = $"Hello {clientName}, here are your books!",
      books = books.Select(b => new 
      { 
        id = b.Id, 
        title = b.Title, 
        author = b.Author, 
        genre = b.Genre,
        publishedYear = b.PublishedYear,
        price = b.Price,
        isAvailable = b.IsAvailable,
        description = b.Description
      }),
      pricing = new { currency = "USD", discount = "10%" },
      totalCount = books.Count()
    });
  }
}
