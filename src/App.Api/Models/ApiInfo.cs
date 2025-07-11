using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Api.Models;
public record ApiInfo
{
  public string Name { get; init; } = string.Empty;
  public string Version { get; init; } = string.Empty;
  public string Description { get; init; } = string.Empty;
  public string Environment { get; init; } = string.Empty;
  public DateTime Timestamp { get; init; }
  public ApiLinks Links { get; init; } = new();
}
