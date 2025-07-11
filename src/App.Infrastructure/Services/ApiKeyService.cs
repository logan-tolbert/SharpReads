using App.Application.DTOs.Responses;
using App.Application.Interfaces;
using App.Core.Entities;
using Microsoft.Extensions.Logging;
namespace App.Infrastructure.Services;

public class ApiKeyService : IApiKeyService
{
  private readonly Dictionary<string, ApiKeyInfo> _apiKeys = new();
  private readonly ILogger<ApiKeyService> _logger;

  public ApiKeyService(ILogger<ApiKeyService> logger)
  {
    _logger = logger;
  }

  public Task<ApiKeyResponse> GenerateApiKeyAsync(string clientName, string? description = null)
  {
    var apiKey = GenerateSecureApiKey();
    var keyInfo = new ApiKeyInfo
    {
      Id = Guid.NewGuid().ToString(),
      ApiKey = apiKey,
      ClientName = clientName,
      Description = description,
      CreatedAt = DateTime.UtcNow,
      ExpiresAt = DateTime.UtcNow.AddYears(1), // 1 year expiry
      IsActive = true
    };

    _apiKeys[apiKey] = keyInfo;
    _logger.LogInformation("API Key generated for client: {ClientName}", clientName);

    return Task.FromResult(new ApiKeyResponse
    {
      ApiKey = apiKey,
      ClientName = clientName,
      Description = description,
      CreatedAt = keyInfo.CreatedAt,
      ExpiresAt = keyInfo.ExpiresAt
    });
  }

  public Task<bool> ValidateApiKeyAsync(string apiKey)
  {
    if (!_apiKeys.TryGetValue(apiKey, out var keyInfo))
    {
      return Task.FromResult(false);
    }

    return Task.FromResult(keyInfo.IsActive && keyInfo.ExpiresAt > DateTime.UtcNow);
  }

  public Task<ApiKeyInfo?> GetApiKeyInfoAsync(string apiKey)
  {
    _apiKeys.TryGetValue(apiKey, out var keyInfo);
    return Task.FromResult(keyInfo);
  }

  public Task<bool> RevokeApiKeyAsync(string apiKey)
  {
    if (_apiKeys.TryGetValue(apiKey, out var keyInfo))
    {
      keyInfo.IsActive = false;
      _logger.LogInformation("API Key revoked for client: {ClientName}", keyInfo.ClientName);
      return Task.FromResult(true);
    }
    return Task.FromResult(false);
  }

  private static string GenerateSecureApiKey()
  {
    using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
    var bytes = new byte[32];
    rng.GetBytes(bytes);
    return Convert.ToBase64String(bytes).Replace("+", "-").Replace("/", "_").Replace("=", "");
  }
}
