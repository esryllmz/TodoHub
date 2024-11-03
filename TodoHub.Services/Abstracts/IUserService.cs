using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoHub.Models.Dtos.UserResponses;
using TodoHub.Models.Entities;

namespace TodoHub.Services.Abstracts
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(RegisterRequestDto registerRequestDto);
        Task<User> GetByEmailAsync(string email);

        Task<User> LoginAsync(LoginRequestDto dto);

        Task<string> DeleteAsync(string id);

        Task<User> UpdateAsync(string id, UpdateRequestDto dto);
        Task<string> ChangePasswordAsync(string id, ChangePasswordRequestDto dto);


    }
}
