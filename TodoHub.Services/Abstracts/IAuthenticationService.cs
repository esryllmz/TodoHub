using TodoHub.Models.Dtos.Tokens;
using TodoHub.Models.Dtos.UserResponses;

namespace TodoHub.Services.Abstracts;

public interface IAuthenticationService
{
    Task<TokenResponseDto> RegisterAsync(RegisterRequestDto dto);
    Task<TokenResponseDto> LoginAsync(LoginRequestDto dto);
}
