using LazyCache;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using Serilog.Parsing;
using WGL.Auth.Application.CQRS.Account.Queries.GenerateToken;
using WGL.Auth.Application.DTOs.Account;
using WGL.Auth.Application.Interfaces.Account;
using WGL.Auth.Application.Wrappers;
using WGL.Auth.Domain.Constants;

namespace WGL.Auth.Application.CQRS.Account.Queries.GetUserLoginToken
{
    public class GenerateUserLoginTokenQuery : IRequest<Response<UserLoginTokenResponse>>
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
    public class GenerateUserLoginTokenQueryHandler : IRequestHandler<GenerateUserLoginTokenQuery, Response<UserLoginTokenResponse>>
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly IAccountRepositoryAsync _accountRepositoryAsync;
        public GenerateUserLoginTokenQueryHandler(ICacheProvider cacheProvider, IAccountRepositoryAsync accountRepositoryAsync)
        {
            _cacheProvider = cacheProvider;
            _accountRepositoryAsync = accountRepositoryAsync;
        }
        public async Task<Response<UserLoginTokenResponse>> Handle(GenerateUserLoginTokenQuery request, CancellationToken cancellationToken)
        {
            if (!_cacheProvider.TryGetValue(Constants.getOneLoginUserToken, out UserLoginTokenResponse getUserToken))
            {
                getUserToken = await _accountRepositoryAsync.GenerateUserTokenQuery(request.UserName, request.Password);
                if (getUserToken.data != null)
                {
                    var cacheEntryOption = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = Convert.ToDateTime(getUserToken.data[0].expires_at) , 
                        //SlidingExpiration = Convert.ToDateTime(getUserToken.data[0].expires_at),                        
                        Size = 1024
                    };
                    _cacheProvider.Set(Constants.getOneLoginUserToken, getUserToken, cacheEntryOption);
                }                
            }
            Log.ForContext("UniqueId", Guid.NewGuid().ToString("N").Substring(0, 20).ToUpper())
              .Information("OneLogin-GenerateTokenQuery: Received Token => {@Token}", getUserToken.data[0].session_token);
            return new Response<UserLoginTokenResponse>(getUserToken, message: "Token Received");
        }
    }
}
