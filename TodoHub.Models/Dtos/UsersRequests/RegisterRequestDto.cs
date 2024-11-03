using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoHub.Models.Dtos.UserResponses;

public sealed record RegisterRequestDto(
     string Name,
     string Lastname,
     string Username,
     string Email,
     string Password
     );
