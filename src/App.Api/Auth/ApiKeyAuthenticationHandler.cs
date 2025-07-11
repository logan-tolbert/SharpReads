using System.Security.Claims;
using System.Text.Encodings.Web;
using App.Application.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationSchemeOptions>
{
  private readonly IApiKeyService _apiKeyService;

  public ApiKeyAuthenticationHandler(IOptionsMonitor<ApiKeyAuthenticationSchemeOptions> options,
      ILoggerFactory logger, UrlEncoder encoder, IApiKeyService apiKeyService)
      : base(options, logger, encoder)
  {
    _apiKeyService = apiKeyService;
  }

  protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
  {
    // Check for API key in header
    if (!Request.Headers.TryGetValue("X-API-Key", out var apiKeyHeaderValues))
    {
      return AuthenticateResult.NoResult();
    }

    var apiKey = apiKeyHeaderValues.FirstOrDefault();
    if (string.IsNullOrEmpty(apiKey))
    {
      return AuthenticateResult.NoResult();
    }

    // Validate API key
    var keyInfo = await _apiKeyService.GetApiKeyInfoAsync(apiKey);
    if (keyInfo == null)
    {
      return AuthenticateResult.Fail("Invalid API Key");
    }

    // Create claims
    var claims = new[]
    {
            new Claim(ClaimTypes.Name, keyInfo.ClientName),
            new Claim("ClientName", keyInfo.ClientName),
            new Claim("ApiKeyId", keyInfo.Id),
            new Claim("CreatedAt", keyInfo.CreatedAt.ToString())
        };

    var identity = new ClaimsIdentity(claims, Scheme.Name);
    var principal = new ClaimsPrincipal(identity);
    var ticket = new AuthenticationTicket(principal, Scheme.Name);

    return AuthenticateResult.Success(ticket);
  }
}
