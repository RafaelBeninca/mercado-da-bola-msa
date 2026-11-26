using Microsoft.AspNetCore.Mvc;
using PlayerService.DTO;
using PlayerService.Servicos;

namespace PlayerService.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class JogadorController : ControllerBase
    {
        private readonly ServJogador _servJogador;

        public JogadorController(ServJogador servJogador)
        {
            _servJogador = servJogador;
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(Guid id)
        {
            try
            {
                return Ok(_servJogador.BuscarJogadorPorId(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult Listar([FromQuery] FiltroJogadorDTO filtro)
        {
            try
            {
                return Ok(_servJogador.ListarJogadores(filtro));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Inserir(InserirJogadorDTO inserirDto)
        {
            try
            {
                return Ok(_servJogador.InserirJogador(inserirDto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("{id}")]
        public IActionResult Alterar(Guid id, AlterarJogadorDTO alterarDto)
        {
            try
            {
                return Ok(_servJogador.AlterarJogador(id, alterarDto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Excluir(Guid id)
        {
            try
            {
                _servJogador.ExcluirJogador(id);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
