using CT.Core.Domain;
using CT.Core.Shared.ModelsViews;
using CT.Core.Shared.ModelsViews.Cliente;
using CT.Manager.Interfaces;
using CT.Manager.Interfaces.Managers;
using CT.Manager.Validator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SerilogTimings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CT.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ClientesController : ControllerBase
{
    private readonly IClienteManager _clienteManager;
    private readonly ILogger<ClientesController> _logger;

    public ClientesController(IClienteManager clienteManager, ILogger<ClientesController> logger)
    {
        _clienteManager = clienteManager;
        _logger = logger;
    }

    /// <summary>
    /// Retorna todos clientes cadastrados na base.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ClienteView), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        var clientes = await _clienteManager.GetClientesAsync();
        if (clientes.Any())
        {
            return Ok(clientes);
        }
        return NotFound();
    }

    /// <summary>
    /// Retorna um cliente consultado pelo id.
    /// </summary>
    /// <param name="id" example="123">Id do cliente.</param>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ClienteView), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(int id)
    {
        var cliente = await _clienteManager.GetClienteAsync(id);
        if (cliente.Id == 0)
        {
            return NotFound();
        }
        return Ok(cliente);
    }

    /// <summary>
    /// Insere um novo cliente
    /// </summary>
    /// <param name="novoCliente"></param>
    [HttpPost]
    [ProducesResponseType(typeof(ClienteView), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(NovoCliente novoCliente)
    {
        _logger.LogInformation("Objeto recebido {@novoCliente}", novoCliente);

        ClienteView clienteInserido;
        using (Operation.Time("Tempo de adição de um novo cliente."))
        {
            _logger.LogInformation("Foi requisitada a inserção de um novo cliente.");
            clienteInserido = await _clienteManager.InsertClienteAsync(novoCliente);
        }
        return CreatedAtAction(nameof(Get), new { id = clienteInserido.Id }, clienteInserido);
    }

    /// <summary>
    /// Altera um cliente.
    /// </summary>
    /// <param name="alteraCliente"></param>
    [HttpPut]
    [ProducesResponseType(typeof(ClienteView), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Put(AlteraCliente alteraCliente)
    {
        var clienteAtualizado = await _clienteManager.UpdateClienteAsync(alteraCliente);
        if (clienteAtualizado == null)
        {
            return NotFound();
        }
        return Ok(clienteAtualizado);
    }

    /// <summary>
    /// Exclui um cliente.
    /// </summary>
    /// <param name="id" example="123">Id do cliente</param>
    /// <remarks>Ao excluir um cliente o mesmo será removido permanentemente da base.</remarks>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        var clienteExcliudo = await _clienteManager.DeleteClienteAsync(id);
        if (clienteExcliudo == null)
        {
            return NotFound();
        }
        return NoContent();
    }
}
