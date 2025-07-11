namespace App.Core.Entities;

public class ApiKeyInfo
{
  public string Id { get; set; } = string.Empty;
  public string ApiKey { get; set; } = string.Empty;
  public string ClientName { get; set; } = string.Empty;
  public string? Description { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime ExpiresAt { get; set; }
  public bool IsActive { get; set; }
}
