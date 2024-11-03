using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoHub.Models.Dtos.Tokens;
using TodoHub.Models.Dtos.UserResponses;

namespace TodoHub.Services.Abstracts
{
    public interface IAuthenticationService
    {

        Task<TokenResponseDto> RegisterByTokenAsync(RegisterRequestDto dto);
        Task<TokenResponseDto> LoginByTokenAsync(LoginRequestDto dto);
    }
}
