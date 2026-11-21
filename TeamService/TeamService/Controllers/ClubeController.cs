using Microsoft.AspNetCore.Mvc;
using TeamService.DTO;
using TeamService.Servicos;

namespace TeamService.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class ClubeController : ControllerBase
    {
        private readonly ServClube _servClube;

        public ClubeController(ServClube servClube)
        {
            _servClube = servClube;
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(Guid id)
        {
            try
            {
                return Ok(_servClube.BuscarClubePorId(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult Listar([FromQuery] FiltroClubeDTO filtro)
        {
            try
            {
                return Ok(_servClube.ListarClubes(filtro));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Inserir(InserirClubeDTO inserirDto)
        {
            try
            {
                var novoClube = new Clube
                {
                    Nome = inserirDto.Nome,
                    DataFundacao = inserirDto.DataFundacao,
                    Nacionalidade = inserirDto.Nacionalidade,
                    UrlLogo = inserirDto.UrlLogo,
                    Status = inserirDto.Status
                };
                _servClube.InserirClube(novoClube);

                return Ok(novoClube);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("{id}")]
        public IActionResult Alterar(Guid id, AlterarClubeDTO alterarDto)
        {
            try
            {
                return Ok(_servClube.AlterarClube(id, alterarDto));
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
                _servClube.ExcluirClube(id);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
