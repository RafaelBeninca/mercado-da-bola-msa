using ContractService.DTO;
using ContractService.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace ContractService.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class ContratoController : ControllerBase
    {
        private readonly ContratoService _contratoService;

        public ContratoController(ContratoService contratoService)
        {
            _contratoService = contratoService;
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(Guid id)
        {
            try
            {
                return Ok(_contratoService.BuscarContratoPorId(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult Listar([FromQuery] FiltroContratoDTO filtro)
        {
            try
            {
                return Ok(_contratoService.ListarContratos(filtro));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Inserir(InserirContratoDTO contrato)
        {
            try
            { 
                return Ok(_contratoService.InserirContrato(contrato));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("{id}")]
        public IActionResult Alterar(Guid id, AtualizarContratoDTO contrato)
        {
            try
            {
                return Ok(_contratoService.AtualizarContrato(id, contrato));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("rescindir/{id}")]
        public IActionResult RescindirContrato(Guid id)
        {
            try
            {
                return Ok(_contratoService.RescindirContrato(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
