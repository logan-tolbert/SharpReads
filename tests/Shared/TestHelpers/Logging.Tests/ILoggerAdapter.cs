using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Tests;
public interface ILoggerAdapter<TType>
{
  void LogInformation(string? message, params object?[] args);
  void LogWarning(string? message, params object?[] args);
  void LogError(Exception? exception, string? message, params object?[] args);

}
