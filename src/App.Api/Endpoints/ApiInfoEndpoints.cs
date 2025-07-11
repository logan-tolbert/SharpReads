using App.Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace App.Api.Endpoints;

public static class InfoEndpoints
{
  public static void MapInfoEndpoints(this WebApplication app)
  {
    app.MapGet("/health", GetHealth)
        .WithName("HealthCheck")
        .WithSummary("API health check")
        .WithDescription("Returns the current health status of the API")
        .WithOpenApi()
        .AllowAnonymous();

    app.MapGet("/", GetApiInfo)
        .WithName("GetApiInfo")
        .WithSummary("Get SharpReads API information")
        .WithDescription("Returns information about the SharpReads bookstore API")
        .WithOpenApi()
        .Produces<ApiInfo>(200)
        .AllowAnonymous();

    app.MapGet("/v1", GetV1ApiInfo)
        .WithName("GetV1ApiInfo")
        .WithSummary("Get SharpReads API v1 information")
        .WithDescription("Returns information about the public v1 API endpoints")
        .WithOpenApi()
        .Produces<ApiInfo>(200)
        .AllowAnonymous();

    app.MapGet("/v2", GetV2ApiInfo)
        .WithName("GetV2ApiInfo")
        .WithSummary("Get SharpReads API v2 information")
        .WithDescription("Returns information about the authenticated v2 API endpoints")
        .WithOpenApi()
        .Produces<object>(200)
        .AllowAnonymous();
  }

  private static IResult GetApiInfo(IConfiguration config, IWebHostEnvironment env)
  {
    return Results.Ok(new ApiInfo
    {
      Name = "SharpReads API",
      Version = "1.0.0",
      Description = "Comprehensive REST API for SharpReads online bookstore",
      Environment = env.EnvironmentName,
      Timestamp = DateTime.UtcNow,
      Links = new ApiLinks
      {
        Documentation = "/scalar",
        OpenApi = "/openapi/v1.json",
        Health = "/health",
        PublicApi = "/v1",
        Auth = new AuthLinks
        {
          GenerateKey = "/auth/generate-key",
          ValidateKey = "/auth/validate-key"
        }
      }
    });
  }

  private static IResult GetV1ApiInfo(IConfiguration config, IWebHostEnvironment env)
  {
    return Results.Ok(new ApiInfo
    {
      Name = "SharpReads API v1",
      Version = "1.0.0",
      Description = "Public API endpoints for SharpReads bookstore - no authentication required",
      Environment = env.EnvironmentName,
      Timestamp = DateTime.UtcNow,
      Links = new ApiLinks
      {
        Documentation = "/scalar",
        OpenApi = "/openapi/v1.json",
        Health = "/health",
        Auth = new AuthLinks
        {
          GenerateKey = "/auth/generate-key",
          ValidateKey = "/auth/validate-key"
        }
      }
    });
  }

  private static IResult GetHealth()
  {
    return Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
  }

  private static IResult GetV2ApiInfo(IConfiguration config, IWebHostEnvironment env)
  {
    return Results.Ok(new
    {
      name = "SharpReads API v2",
      version = "2.0.0",
      description = "Authenticated API endpoints for SharpReads bookstore - requires valid API key",
      environment = env.EnvironmentName,
      timestamp = DateTime.UtcNow,
      features = new[]
      {
        "Complete book catalog with pricing",
        "Real-time availability", 
        "Partner discounts",
        "Advanced search and filtering",
        "Personalized responses"
      },
      authentication = new
      {
        type = "API Key",
        required = true,
        generateKey = "/auth/generate-key",
        validateKey = "/auth/validate-key"
      },
      links = new
      {
        books = "/v2/books",
        documentation = "/scalar",
        auth = "/auth/generate-key"
      },
      note = "Get an API key to access the complete catalog with pricing and availability"
    });
  }
}
