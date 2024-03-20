namespace ClienteAPI.Domain.Clientes.Service
{
    public interface IClienteService
    {
        public Task<Cliente> Create(Cliente cliente);
        public Task Delete(Guid uuid);
        public Task<List<Cliente>> GetAll();
        public Task<Cliente> GetById(Guid uuid);
        public Task<Cliente> Update(Guid uuid, Cliente cliente);
    }
}
