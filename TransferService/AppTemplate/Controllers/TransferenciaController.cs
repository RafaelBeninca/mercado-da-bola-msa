using TransferService.DTO;
using TransferService.Servicos;
using Microsoft.AspNetCore.Mvc;
using TransferService.DTO;

namespace TransferService.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class TransferenciaController : ControllerBase
    {
        private readonly TranferenciaService _transferenciaService;

        public TransferenciaController(TranferenciaService transferenciaService)
        {
            _transferenciaService = transferenciaService;
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(Guid id)
        {
            try
            {
                return Ok(_transferenciaService.BuscarTransferenciaPorId(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult Listar([FromQuery] FiltroTransferenciaDTO filtro)
        {
            try
            {
                return Ok(_transferenciaService.ListarTransferencias(filtro));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Inserir(InserirTransferenciaDTO transferencia)
        {
            try
            {
                _transferenciaService.InserirTranferencia(transferencia);

                return Ok(transferencia);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
