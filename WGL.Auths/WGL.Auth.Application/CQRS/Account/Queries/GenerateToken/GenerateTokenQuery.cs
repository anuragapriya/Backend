using WGL.Auth.Application.DTOs.Account;
using WGL.Auth.Application.Interfaces.Account;
using WGL.Auth.Application.Wrappers;
using WGL.Auth.Domain.Constants;
using LazyCache;
using MediatR;
using Serilog;
using Microsoft.Extensions.Caching.Memory;
using WGL.Auth.Domain.Entities;

namespace WGL.Auth.Application.CQRS.Account.Queries.GenerateToken
{
    public class GenerateTokenQuery : IRequest<Response<GenerateTokenResponse>>
    {
        //public required string AppId { get; set; }
        //public required string SecretKey { get; set; }
    }
    public class GenerateTokenQueryHandler : IRequestHandler<GenerateTokenQuery, Response<GenerateTokenResponse>>
    {
        private readonly IAccountRepositoryAsync _accountRepositoryAsync;
        private readonly ICacheProvider _cacheProvider;
        public GenerateTokenQueryHandler(IAccountRepositoryAsync accountRepositoryAsync,ICacheProvider cacheProvider)
        {
            _accountRepositoryAsync = accountRepositoryAsync;
            _cacheProvider = cacheProvider;
        }
        public async Task<Response<GenerateTokenResponse>> Handle(GenerateTokenQuery request, CancellationToken cancellationToken)        
        {
            //ApplicationUser applicationUser = new ApplicationUser { Id = 100, FirstName = "TEst", LastName = "Test", ComapanyName = "TEst", Email = "Test@TEst.com", Password = "Test@1234" };
            //var result = await _accountRepositoryAsync.GetAllAsync();
            var result = await _accountRepositoryAsync.sp_GetAllAsync();

            GenerateTokenResponse getToken = new();
            getToken = await _accountRepositoryAsync.GenerateTokenQuery(/*request.AppId, request.SecretKey*/);
            return new Response<GenerateTokenResponse>(getToken, message: "Token Received");
        }
    }
}
