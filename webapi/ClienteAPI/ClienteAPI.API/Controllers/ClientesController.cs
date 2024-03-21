using ClienteAPI.API.Dtos.Clientes;
using ClienteAPI.Domain.Clientes;
using ClienteAPI.Domain.Clientes.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClienteAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ClientesController(IClienteService clienteService,
        ILogger<ClientesController> logger) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "CLIENTE_ADM_ROLE")]
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
        [Authorize(Roles = "CLIENTE_ADM_ROLE")]
        public async Task<ActionResult<ClienteDto>> Details(Guid uuid)
        {
            ClienteDto cliente;
            try
            {
                var clienteExisting = await clienteService.GetById(uuid);
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
        [Authorize(Roles = "CLIENTE_ADM_ROLE")]
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
        [Authorize(Roles = "CLIENTE_ADM_ROLE")]
        public async Task<ActionResult<ClienteDto>> Edit(Guid uuid, [FromBody] ClienteDto cliente)
        {
            ClienteDto usuarioDto;
            try
            {
                var usuarioCreated = await clienteService.Update(uuid, new Cliente()
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
        [Authorize(Roles = "CLIENTE_ADM_ROLE")]
        public async Task<ActionResult> Delete(Guid uuid)
        {
            try
            {
                await clienteService.Delete(uuid);
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }
            return Ok();
        }

    }
}
