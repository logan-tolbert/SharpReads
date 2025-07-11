using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Api.Models;
public record AuthLinks
{
  public string GenerateKey { get; init; } = string.Empty;
  public string ValidateKey { get; init; } = string.Empty;
}
