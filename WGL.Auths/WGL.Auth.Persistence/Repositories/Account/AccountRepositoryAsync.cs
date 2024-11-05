using Dapper;
using LazyCache;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Serilog;
using System.Data;
using System.Text.Json;
using WGL.Auth.Application.DTOs.Account;
using WGL.Auth.Application.Exceptions;
using WGL.Auth.Application.Interfaces.Account;
using WGL.Auth.Domain.Constants;
using WGL.Auth.Domain.Entities;
using WGL.Auth.Domain.Settings;
using WGL.Auth.Persistence.Contexts;
using WGL.Auth.Persistence.Repositories.Generic;

namespace WGL.Auth.Persistence.Repositories.Account
{
    public class AccountRepositoryAsync(IOptions<OneLoginSettings> oneLoginSettings, ICacheProvider cacheProvider, DapperContext dapperContext) : GenericRepositoryAsync<ApplicationUser>(dapperContext), IAccountRepositoryAsync
    {
        private readonly OneLoginSettings _oneLoginSettings = oneLoginSettings.Value;
        private readonly ICacheProvider _cacheProvider = cacheProvider;
        private readonly DapperContext _dapperContext = dapperContext;
        private readonly IDbConnection _dbconnection = dapperContext.CreateConnection();

        public async Task<GenerateTokenResponse> GenerateTokenQuery(/*string AppKey, string SecretKey*/)
        {   
            if (!_cacheProvider.TryGetValue(Constants.getOneLoginToken, out GenerateTokenResponse getToken))
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, _oneLoginSettings.Endpoint + Constants.getTokenMethod);
                request.Headers.Add(Constants.Authorization, _oneLoginSettings.EncryptedCreds);
                var collection = new List<KeyValuePair<string, string>>();
                collection.Add(new(Constants.grantType, _oneLoginSettings.GrantType));
                collection.Add(new(Constants.userName, _oneLoginSettings.AppId));
                collection.Add(new(Constants.password, _oneLoginSettings.SecretKey));
                var content = new FormUrlEncodedContent(collection);
                request.Content = content;
                var response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    Log.Error("Error occured while getting Token => {@StatusCode},{@EndPoint},{@UserName},{@Password}",
                                                        response.StatusCode,
                                                        _oneLoginSettings.Endpoint + Constants.getTokenMethod,
                                                        _oneLoginSettings.AppId, _oneLoginSettings.SecretKey);
                    throw new ApiException($"{response.Content.ReadAsStringAsync().Result}");

                }
                getToken = JsonSerializer.Deserialize<GenerateTokenResponse>(response.Content.ReadAsStringAsync().Result);
                if (getToken !=null && getToken.access_token != null)
                {
                    var cacheEntryOption = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddSeconds(getToken.expires_in),
                        SlidingExpiration = TimeSpan.FromSeconds(getToken.expires_in),
                        Size = 1024
                    };
                    _cacheProvider.Set(Constants.getOneLoginToken, getToken, cacheEntryOption);
                }
            }
            Log.ForContext("UniqueId", Guid.NewGuid().ToString("N").Substring(0, 20).ToUpper())
               .Information("OneLogin-GenerateTokenQuery: Received Token => {@Token}", getToken.access_token);
            return getToken;
        }

        public async Task<UserLoginTokenResponse> GenerateUserTokenQuery(string UserName, string Password)
        {
            if (!_cacheProvider.TryGetValue(Constants.getOneLoginToken, out GenerateTokenResponse getToken))
            {
                getToken = await GenerateTokenQuery();
            }
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, _oneLoginSettings.Endpoint + Constants.getUserTokenMethod);
            request.Headers.Add(Constants.Authorization, Constants.Bearer + getToken.access_token);
            string jsonString = JsonSerializer.Serialize(new
            {
                username_or_email = UserName,
                password = Password,
                subdomain = _oneLoginSettings.SubDomainName
            });
            request.Content = new StringContent(jsonString,null, Constants.MediaType);
            var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                Log.Error("Error occured while getting User Session Token => {@StatusCode},{@EndPoint},{@UserName},{@Password}",
                                                    response.StatusCode,
                                                    _oneLoginSettings.Endpoint + Constants.getUserTokenMethod,
                                                    UserName, Password);
                throw new ApiException($"{response.Content.ReadAsStringAsync().Result}");
            }
            return JsonSerializer.Deserialize<UserLoginTokenResponse>(response.Content.ReadAsStringAsync().Result);
        }

        public Task<bool> IsDuplicateUser(string EmailAddress)
        {
            throw new NotImplementedException();
        }

        public Task<int> Sp_CreateUserAsync(ApplicationUser applicationUser)
        {
            DynamicParameters Params = new ();
            Params.Add("@FirstName", applicationUser.FirstName);
            Params.Add("@LastName", applicationUser.LastName);
            Params.Add("@CompanyName", applicationUser.CompanyName);
            Params.Add("@MobileNumber", applicationUser.MobileNumber);
            Params.Add("@EmailID", applicationUser.EmailID);
            Params.Add("@Password", applicationUser.Password);
            Params.Add("@CreatedBy", applicationUser.CreatedBy);
            Params.Add("@StatementType", "Insert");
            return _dbconnection.ExecuteAsync("Sp_User_CRUD", Params, commandType: CommandType.StoredProcedure);
        }

        public Task<int> Sp_DeleteUserAsync(int UserId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerator<ApplicationUser>> Sp_GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> Sp_GetUserByIdAsync(int UserId)
        {
            throw new NotImplementedException();
        }

        public Task<int> Sp_UpdateUserAsync(ApplicationUser applicationUser)
        {
            throw new NotImplementedException();
        }
    }
}
