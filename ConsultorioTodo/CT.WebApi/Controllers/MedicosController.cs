using CT.Core.Shared.ModelsViews;
using CT.Core.Shared.ModelsViews.Medico;
using CT.Manager.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SerilogTimings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CT.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MedicosController : ControllerBase
    {
        private readonly IMedicoManager _medicoManager;
        private readonly ILogger<MedicosController> _logger;

        public MedicosController(IMedicoManager medicoManager, ILogger<MedicosController> logger)
        {
            _medicoManager = medicoManager;
            _logger = logger;
        }

        /// <summary>
        /// Retorna todos os médicos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MedicoView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var medicos = await _medicoManager.GetMedicosAsync();
            if (medicos.Any())
            {
                return Ok(medicos);
            }
            return NotFound();
        }

        /// <summary>
        /// Retorna um médico consultado via ID
        /// </summary>
        /// <param name="id" example="123">Id do médico</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MedicoView), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            var medico = await _medicoManager.GetMedicoAsync(id);
            if (medico.Id == 0)
            {
                return NotFound();
            }
            return Ok(medico);
        }

        /// <summary>
        /// Insere um novo médico.
        /// </summary>
        /// <param name="medico"></param>
        [HttpPost]
        [ProducesResponseType(typeof(MedicoView), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(NovoMedico medico)
        {
            _logger.LogInformation("Objeto recebido {@medico}", medico);

            MedicoView medicoInserido;
            using (Operation.Time("Tempo de adição de um novo médico."))
            {
                _logger.LogInformation("Foi requisitada a inserção de um novo médico.");
                medicoInserido = await _medicoManager.InsertMedicoAsync(medico);
            }
            return CreatedAtAction(nameof(Get), new { id = medicoInserido.Id }, medicoInserido);
        }

        /// <summary>
        /// Altera um médico
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(MedicoView), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(AlteraMedico medico)
        {
            var medicoAtualizado = await _medicoManager.UpdateMedicoAsync(medico);
            if (medicoAtualizado == null)
            {
                return NotFound();
            }
            return Ok(medicoAtualizado);
        }

        /// <summary>
        /// Exclui um médico.
        /// </summary>
        /// <param name="id" example="123">Id do médico</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var medicoExcluido = await _medicoManager.DeleteMedicoAsync(id);
            if (medicoExcluido == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
