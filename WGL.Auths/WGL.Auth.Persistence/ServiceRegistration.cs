using Microsoft.Extensions.DependencyInjection;
using WGL.Auth.Application.Interfaces.Account;
using WGL.Auth.Application.Interfaces.Generic;
using WGL.Auth.Persistence.Contexts;
using WGL.Auth.Persistence.Repositories.Account;
using WGL.Auth.Persistence.Repositories.Generic;

namespace WGL.Auth.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceLayer(this IServiceCollection services)
        {
            #region Repositories
            /// Dependency Register for Classes....             
            /// 
            services.AddSingleton(typeof(DapperContext));
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient(typeof(IAccountRepositoryAsync), typeof(AccountRepositoryAsync));
            
            #endregion
        }
    }
}
