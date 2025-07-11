using App.Application.DTOs.Responses;
using App.Core.Entities;

namespace App.Application.Interfaces;

public interface IApiKeyService
{
  Task<ApiKeyResponse> GenerateApiKeyAsync(string clientName, string? description = null);
  Task<bool> ValidateApiKeyAsync(string apiKey);
  Task<ApiKeyInfo?> GetApiKeyInfoAsync(string apiKey);
  Task<bool> RevokeApiKeyAsync(string apiKey);
}
