using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Logging.Tests;

public class LoggerAdapter<TType> : ILoggerAdapter<TType>
{
  private readonly ILogger<TType> _logger;

  public LoggerAdapter(ILogger<TType> logger)
  {
    _logger = logger;
  }

  public void LogError(Exception? exception, string? message, params object?[] args)
  {
    _logger.LogError(exception, message, args);
  }

  public void LogInformation(string? message, params object?[] args)
  {
    _logger?.LogInformation(message, args);
  }

  public void LogWarning(string? message, params object?[] args)
  {
    _logger.LogWarning(message, args);
  }
}
