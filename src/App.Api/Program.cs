using System.Text.Json;
using App.Api.Endpoints;
using App.Application.Interfaces;
using App.Infrastructure.Services;
using App.Infrastructure.Repositories;
using Microsoft.AspNetCore.WebUtilities;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.Configure<ScalarOptions>(options =>
{
  options.Title = "SharpReads API";
  options.WithTheme(ScalarTheme.Saturn);
});



// Add API Key authentication
builder.Services.AddAuthentication("ApiKey")
    .AddScheme<ApiKeyAuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("ApiKey", options => { });

builder.Services.AddAuthorization();
builder.Services.AddSingleton<IApiKeyService, ApiKeyService>();
builder.Services.AddSingleton<IBookRepository, BookRepository>();
builder.Services.AddSingleton<IBookService, BookService>();

var app = builder.Build();

// Api Documentation
app.MapOpenApi();
app.MapScalarApiReference();

app.UseAuthentication();
app.UseAuthorization();

// Map endpoints
app.MapInfoEndpoints();
app.MapAuthEndpoints();
app.MapV1BookEndpoints();
app.MapV2BookEndpoints();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;
    if (response.StatusCode >= 400 && response.ContentType == null)
    {
        response.ContentType = "application/problem+json";
        var problem = new
        {
            type = "about:blank",
            title = ReasonPhrases.GetReasonPhrase(response.StatusCode),
            status = response.StatusCode
        };
        await response.WriteAsync(JsonSerializer.Serialize(problem));
    }
});

app.Run();
