using Azure.Core;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoHub.Core.Exceptions;
using TodoHub.Models.Dtos.UserResponses;
using TodoHub.Models.Entities;
using TodoHub.Services.Abstracts;
using TodoHub.Services.Rules;

namespace TodoHub.Services.Concretes
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly UserBusinessRules _userBusinessRules;
        public UserService(UserManager<User> userManager, UserBusinessRules userBusinessRules)
        {
            _userManager = userManager;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<string> ChangePasswordAsync(string id, ChangePasswordRequestDto dto)
        {
            var user = await _userBusinessRules.EnsureUserExistsAsync(id);
            _userBusinessRules.EnsurePasswordsMatch(dto.NewPassword, dto.NewPassword);
            var result = await _userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);
            CheckForIdentityResult(result);

            
            return "Şifre Değiştirildi.";
        }

        public async Task<User> CreateUserAsync(RegisterRequestDto registerRequestDto)
        {
            User user = new User()
            {
                Email = registerRequestDto.Email,
                UserName = registerRequestDto.Username
                
            };



            var result = await _userManager.CreateAsync(user, registerRequestDto.Password);

            var role = await _userManager.AddToRoleAsync(user, "User");
            if (!role.Succeeded)
            {
                throw new BusinessException(role.Errors.First().Description);
            }


            return user;
        }

        public async Task<string> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            UserIsPresent(user);

            await _userManager.DeleteAsync(user);

            return "Kullanıcı Silindi.";
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            UserIsPresent(user);

            return user;
        }

        public async Task<User> LoginAsync(LoginRequestDto dto)
        {
            var userExist = await _userManager.FindByEmailAsync(dto.Email);
            UserIsPresent(userExist);

            var result = await _userManager.CheckPasswordAsync(userExist, dto.Password);

            if (result is false)
            {
                throw new NotFoundException("Parolanız yanlış.");
            }

            return userExist;
        }

        public async Task<User> UpdateAsync(string id, UpdateRequestDto dto)
        {
            var user = await _userBusinessRules.EnsureUserExistsAsync(id);

            user.UserName = dto.Username;
          
          
            var result = await _userManager.UpdateAsync(user);
            CheckForIdentityResult(result);

            return user;

        }

        private void UserIsPresent(User? user)
        {
            if (user is null)
            {
                throw new NotFoundException("Kullanıcı bulunamadı.");
            }
        }
        private void CheckForIdentityResult(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                throw new BusinessException(result.Errors.ToList().First().Description);
            }
        }
    }
}
