using App.Application.DTOs.Responses;
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
        .Produces<BooksCatalogResponse>(200)
        .Produces(401)
        .RequireAuthorization();
  }

  private static async Task<IResult> GetBooksAsync(ClaimsPrincipal user, IBookService bookService)
  {
    var clientName = user.FindFirst("ClientName")?.Value;
    var books = await bookService.GetAllBooksAsync();
    
    var response = new BooksCatalogResponse
    {
      Message = $"Hello {clientName}, here are your books!",
      Books = books.Select(b => new BookResponse
      { 
        Id = b.Id, 
        Title = b.Title, 
        Author = b.Author, 
        Genre = b.Genre,
        PublishedYear = b.PublishedYear,
        Price = b.Price,
        IsAvailable = b.IsAvailable,
        Description = b.Description,
        CreatedAt = b.CreatedAt,
        UpdatedAt = b.UpdatedAt
      }).ToList(),
      Pricing = new PricingInfo { Currency = "USD", Discount = "10%" },
      TotalCount = books.Count()
    };
    
    return Results.Ok(response);
  }
}
