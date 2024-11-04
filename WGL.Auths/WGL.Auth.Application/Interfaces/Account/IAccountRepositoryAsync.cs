using WGL.Auth.Application.DTOs.Account;
using WGL.Auth.Application.Interfaces.Generic;
using WGL.Auth.Domain.Entities;

namespace WGL.Auth.Application.Interfaces.Account
{
    public interface IAccountRepositoryAsync : IGenericRepositoryAsync<ApplicationUser>
    {
        Task<GenerateTokenResponse> GenerateTokenQuery(/*string AppKey, string SecretKey*/);

        Task<UserLoginTokenResponse> GenerateUserTokenQuery(string UserName, string Password);
    }
}
