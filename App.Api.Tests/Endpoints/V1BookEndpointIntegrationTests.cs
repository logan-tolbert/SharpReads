using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace App.Api.Tests.Endpoints;

public class V1BookEndpointIntegrationTests(WebApplicationFactory<IApiMarker> factory)
  : IClassFixture<WebApplicationFactory<IApiMarker>>
{
  [Fact]
  public async Task GetPublicBooks_ReturnsSuccess()
  {
    var client = factory.CreateClient();
    var response = await client.GetAsync("/v1/books", TestContext.Current.CancellationToken);
    response.EnsureSuccessStatusCode();
    var responseString = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
    response.IsSuccessStatusCode.Should().BeTrue();
  }
}
