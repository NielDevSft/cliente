using ClienteAPI.Application.Services;
using ClienteAPI.Domain.Clientes.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClienteAPI.Common.IoC
{
    public class InjectorServices
    {
        public static void AddServices(IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IClienteService, ClienteService>();
            //services.AddScoped<IPedidoService, PedidoService>();
        }
    }
}
