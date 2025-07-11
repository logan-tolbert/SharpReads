using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Api.Models;
public record ApiLinks
{
  public string Documentation { get; init; } = string.Empty;
  public string OpenApi { get; init; } = string.Empty;
  public string Health { get; init; } = string.Empty;
  public string PublicApi { get; init; } = string.Empty;
  public AuthLinks Auth { get; init; } = new();
}

