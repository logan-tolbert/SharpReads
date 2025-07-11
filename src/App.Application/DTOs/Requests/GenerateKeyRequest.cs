using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.DTOs.Requests;
public record GenerateKeyRequest(string ClientName, string? Description = null);
