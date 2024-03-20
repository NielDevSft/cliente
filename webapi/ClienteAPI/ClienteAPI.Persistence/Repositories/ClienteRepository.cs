using ClienteAPI.Domain.Clientes;
using ClienteAPI.Domain.Clientes.Repository;
using ClienteAPI.Persistence.Abstractions;
using ClienteAPI.Persistence.Contexts;
using Microsoft.Extensions.Logging;

namespace ClienteAPI.Persistence.Repositories
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(ClienteOrganizationContext context, ILogger<Repository<Cliente>> logger) : base(context, logger)
        {
        }
    }
}
