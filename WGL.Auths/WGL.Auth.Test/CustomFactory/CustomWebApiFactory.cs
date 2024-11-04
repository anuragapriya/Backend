using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using WGL.Auth.Application.Interfaces.Account;

namespace WGL.Auth.Test.CustomFactory
{
    public class CustomWebApiFactory : WebApplicationFactory<Program>
    {
        public Mock<IAccountRepositoryAsync> AccountRepositoryMock { get; }
        public CustomWebApiFactory() 
        {
            AccountRepositoryMock = new Mock<IAccountRepositoryAsync>();
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton(AccountRepositoryMock.Object);
            });
        }
    }
}
