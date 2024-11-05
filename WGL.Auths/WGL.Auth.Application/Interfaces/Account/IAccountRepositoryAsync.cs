using WGL.Auth.Application.DTOs.Account;
using WGL.Auth.Application.Interfaces.Generic;
using WGL.Auth.Domain.Entities;

namespace WGL.Auth.Application.Interfaces.Account
{
    public interface IAccountRepositoryAsync : IGenericRepositoryAsync<ApplicationUser>
    {
        Task<GenerateTokenResponse> GenerateTokenQuery(/*string AppKey, string SecretKey*/);

        Task<UserLoginTokenResponse> GenerateUserTokenQuery(string UserName, string Password);

        Task<bool> IsDuplicateUser(string EmailAddress);

        Task<int> Sp_CreateUserAsync(ApplicationUser applicationUser);
        Task<int> Sp_UpdateUserAsync(ApplicationUser applicationUser);
        Task<int> Sp_DeleteUserAsync(int UserId);
        Task<IEnumerator<ApplicationUser>> Sp_GetAllUsersAsync();
        Task<ApplicationUser> Sp_GetUserByIdAsync(int UserId);

    }
}
