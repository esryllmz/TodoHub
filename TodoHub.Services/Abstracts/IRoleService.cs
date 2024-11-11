
using TodoHub.Models.Dtos.UsersRequests;

namespace TodoHub.Services.Abstracts
{
    public interface IRoleService
    {


        Task<string> AddRoleToUser(RoleAddToUserRequestDto dto);

        Task<List<string>> GetAllRolesByUserId(string userId);

        Task<string> AddRoleAsync(string name);
    }
}
