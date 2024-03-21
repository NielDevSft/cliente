using ClienteAPI.Domain.Clientes;
using ClienteAPI.Domain.Clientes.Repository;
using ClienteAPI.Domain.Clientes.Service;
using Newtonsoft.Json;

namespace ClienteAPI.Application.Services
{
    public class ClienteService(IClienteRepository _clienteRepository)
        : IClienteService
    {
        public async Task<Cliente> Create(Cliente cliente)
        {
            if (!cliente.IsValid())
                throw new Exception(JsonConvert
                    .SerializeObject(cliente
                    .ValidationResult
                    .Errors));
            
            await Task.Run(() => _clienteRepository.Add(cliente));

            _clienteRepository.SaveChanges();
            
            return cliente;
        }

        public async Task Delete(Guid uuid)
        {            
            await Task.Run(() => _clienteRepository.Remove(uuid));
            _clienteRepository.SaveChanges();
            
        }

        public async Task<List<Cliente>> GetAll()
        {
            var clienteList = new List<Cliente>();

            clienteList.AddRange(await _clienteRepository.FindAllWhereAsync(i => !i.Removed));

            
            return clienteList;
        }

        public async Task<Cliente> GetById(Guid uuid)
        {
            var clienteFound = await _clienteRepository
                .GetByIdAsync(uuid);

            if (clienteFound! == null!)
            {
                throw new Exception("Item não encontrado");
            }
            

            return clienteFound;
        }

        public async Task<Cliente> Update(Guid uuid, Cliente cliente)
        {
            var clienteExisting = _clienteRepository.GetById(uuid);

            if (!cliente.IsValid())
                throw new Exception(JsonConvert
                    .SerializeObject(cliente
                    .ValidationResult
                    .Errors));

            clienteExisting!.NomeComleto = cliente.NomeComleto;
            clienteExisting.DtaNascimento = cliente.DtaNascimento;
            clienteExisting.CPF = cliente.CPF;
            clienteExisting.ValRenda = cliente.ValRenda;

            await Task.Run(() => _clienteRepository.Update(clienteExisting));
            _clienteRepository.SaveChanges();
            
            return cliente;
        }
    }
}
