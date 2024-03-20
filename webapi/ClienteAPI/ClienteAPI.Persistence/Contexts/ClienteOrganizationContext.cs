using ClienteAPI.Domain.Clientes;
using ClienteAPI.Persistence.Extentions;
using ClienteAPI.Persistence.Mapping;
using Microsoft.EntityFrameworkCore;

namespace ClienteAPI.Persistence.Contexts
{
    public class ClienteOrganizationContext : DbContext
    {
        public ClienteOrganizationContext()
        {
        }

        public ClienteOrganizationContext(DbContextOptions<ClienteOrganizationContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("app");

            modelBuilder.AddConfiguration(new DefaultMap<Cliente>());

            modelBuilder.Entity<Cliente>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
