using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.DTOs.Responses;
public record ApiKeyResponse
{
  public string ApiKey { get; init; } = string.Empty;
  public string ClientName { get; init; } = string.Empty;
  public string? Description { get; init; }
  public DateTime CreatedAt { get; init; }
  public DateTime ExpiresAt { get; init; }
}
