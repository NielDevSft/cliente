using ClienteAPI.API.Dtos.Clientes;
using ClienteAPI.Domain.Clientes;
using ClienteAPI.Domain.Clientes.Service;
using Microsoft.AspNetCore.Mvc;

namespace ClienteAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController(IClienteService clienteService,
        ILogger<ClientesController> logger) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ICollection<ClienteDto>>> Index()
        {
            try
            {
                var usuarioList = await clienteService.GetAll();
                return Ok(usuarioList.Select(u =>
                new ClienteDto(u.NomeComleto,
                u.DtaNascimento,
                u.ValRenda,
                u.CPF,
                u.UsuarioUuid.ToString(),
                u.CreateAt,
                u.UpdateAt,
                u.Uuid.ToString()
                )));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{uuid}")]
        public async Task<ActionResult<ClienteDto>> Details(string uuid)
        {
            ClienteDto cliente;
            try
            {
                var clienteExisting = await clienteService.GetById(Guid.Parse(uuid));
                cliente = new ClienteDto(
                    clienteExisting.NomeComleto,
                    clienteExisting.DtaNascimento,
                    clienteExisting.ValRenda,
                    clienteExisting.CPF,
                    clienteExisting.UsuarioUuid.ToString(),
                    clienteExisting.CreateAt,
                    clienteExisting.UpdateAt
                );
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDto>> Create([FromBody] ClienteDto cliente)
        {
            ClienteDto usuarioDto;
            try
            {
                var usuarioCreated = await clienteService.Create(new Cliente()
                {
                    NomeComleto = cliente.NomeComleto,
                    DtaNascimento = cliente.DtaNascimento,
                    UsuarioUuid = Guid.Parse(cliente.UserUuid),
                    ValRenda = cliente.ValRenda,
                    CPF = cliente.CPF,
                });
                usuarioDto = cliente with
                {
                    Uuid = usuarioCreated.Uuid.ToString(),
                    CreateAt = usuarioCreated.CreateAt,
                    UpdateAt = usuarioCreated.UpdateAt,
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            return Created("success", usuarioDto);
        }

        // GET: ClienteControllers/Edit/5
        [HttpPut("{uuid}")]
        public async Task<ActionResult<ClienteDto>> Edit(string uuid, [FromBody] ClienteDto cliente)
        {
            ClienteDto usuarioDto;
            try
            {
                var usuarioCreated = await clienteService.Update(Guid.Parse(uuid), new Cliente()
                {
                    NomeComleto = cliente.NomeComleto,
                    DtaNascimento = cliente.DtaNascimento,
                    ValRenda = cliente.ValRenda,
                    CPF = cliente.CPF,
                });
                usuarioDto = cliente with
                {
                    Uuid = usuarioCreated.Uuid.ToString(),
                    CreateAt = usuarioCreated.CreateAt,
                    UpdateAt = usuarioCreated.UpdateAt,
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            return Created("success", usuarioDto);
        }


        [HttpDelete("{uuid}")]
        public async Task<ActionResult> Delete(string uuid)
        {
            try
            {
                await clienteService.Delete(Guid.Parse(uuid));
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }
            return Ok();
        }

    }
}
