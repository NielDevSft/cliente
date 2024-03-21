using ClienteAPI.Domain.Clientes.Repository;
using ClienteAPI.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ClienteAPI.Common.IoC
{
    public class InjectorRepositories
    {
        public static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IClienteRepository, ClienteRepository>();
        }
    }
}
