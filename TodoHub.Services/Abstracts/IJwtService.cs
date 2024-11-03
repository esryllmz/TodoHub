using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoHub.Core.Responses;
using TodoHub.Models.Dtos.Todo.Requests;
using TodoHub.Models.Dtos.Todo.Responses;
using TodoHub.Models.Dtos.Tokens;
using TodoHub.Models.Entities;

namespace TodoHub.Services.Abstracts
{
    public interface IJwtService
    {
        Task<TokenResponseDto> CreateToken(User user);
    }
}
