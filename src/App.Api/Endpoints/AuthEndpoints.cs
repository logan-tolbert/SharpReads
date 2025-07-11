using App.Application.DTOs.Requests;
using App.Application.DTOs.Responses;
using App.Application.Interfaces;

namespace App.Api.Endpoints;

public static class AuthEndpoints
{
  public static void MapAuthEndpoints(this WebApplication app)
  {
    app.MapPost("/auth/generate-key", GenerateApiKeyAsync)
        .WithName("GenerateApiKey")
        .WithSummary("Generate a new API key")
        .WithDescription("Generates a new API key for client authentication")
        .WithOpenApi()
        .Produces<ApiKeyResponse>(200)
        .AllowAnonymous();

    app.MapPost("/auth/validate-key", ValidateApiKeyAsync)
        .WithName("ValidateApiKey")
        .WithSummary("Validate an API key")
        .WithDescription("Validates whether an API key is active and valid")
        .WithOpenApi()
        .Produces<object>(200)
        .AllowAnonymous();
  }

  private static async Task<IResult> GenerateApiKeyAsync(GenerateKeyRequest request, IApiKeyService apiKeyService)
  {
    var result = await apiKeyService.GenerateApiKeyAsync(request.ClientName, request.Description);
    return Results.Ok(result);
  }

  private static async Task<IResult> ValidateApiKeyAsync(ValidateKeyRequest request, IApiKeyService apiKeyService)
  {
    var isValid = await apiKeyService.ValidateApiKeyAsync(request.ApiKey);
    return Results.Ok(new { isValid, timestamp = DateTime.UtcNow });
  }
}
