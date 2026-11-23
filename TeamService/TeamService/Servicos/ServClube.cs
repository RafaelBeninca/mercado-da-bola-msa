using Microsoft.EntityFrameworkCore;
using TeamService.DTO;

namespace TeamService.Servicos
{
    public class ServClube
    {
        private const int TamanhoMaximoPagina = 100;

        private readonly DataContext _dataContext;

        public ServClube(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void InserirClube(Clube novoClube)
        {
            ValidarClube(novoClube);

            novoClube.Id = Guid.NewGuid();

            _dataContext.Add(novoClube);
            _dataContext.SaveChanges();
        }

        public void ValidarClube(Clube clube)
        {
            if (string.IsNullOrWhiteSpace(clube.Nome))
            {
                throw new Exception("Falta informar o nome do clube.");
            }

            if (string.IsNullOrWhiteSpace(clube.Nacionalidade))
            {
                throw new Exception("Falta informar a nacionalidade do clube.");
            }

            if (clube.DataFundacao == default || clube.DataFundacao.Date > DateTime.Today)
            {
                throw new Exception("Data de fundação inválida.");
            }

            if (!Enum.IsDefined(clube.Status))
            {
                throw new Exception("Status inválido.");
            }
        }

        public ResultadoPaginadoDTO<Clube> ListarClubes(FiltroClubeDTO filtro)
        {
            var pagina = Math.Max(filtro.Pagina, 1);
            var tamanhoPagina = Math.Clamp(filtro.TamanhoPagina, 1, TamanhoMaximoPagina);

            var consulta = _dataContext.Clubes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro.Nome))
            {
                consulta = consulta.Where(c => EF.Functions.Like(c.Nome, $"%{filtro.Nome}%"));
            }

            if (!string.IsNullOrWhiteSpace(filtro.Nacionalidade))
            {
                consulta = consulta.Where(c => EF.Functions.Like(c.Nacionalidade, filtro.Nacionalidade));
            }

            if (filtro.Status.HasValue)
            {
                consulta = consulta.Where(c => c.Status == filtro.Status.Value);
            }

            var totalItens = consulta.Count();

            var itens = consulta
                .OrderBy(c => c.Nome)
                .ThenBy(c => c.Id)
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToList();

            return new ResultadoPaginadoDTO<Clube>
            {
                Itens = itens,
                Pagina = pagina,
                TamanhoPagina = tamanhoPagina,
                TotalItens = totalItens,
                TotalPaginas = (int)Math.Ceiling(totalItens / (double)tamanhoPagina)
            };
        }

        public Clube BuscarClubePorId(Guid id)
        {
            var clube = _dataContext.Clubes.FirstOrDefault(c => c.Id == id);

            if (clube == null)
            {
                throw new Exception("Clube não encontrado.");
            }

            return clube;
        }

        public Clube AlterarClube(Guid id, AlterarClubeDTO dto)
        {
            var clube = BuscarClubePorId(id);

            if (dto.Nome != null) clube.Nome = dto.Nome;
            if (dto.DataFundacao.HasValue) clube.DataFundacao = dto.DataFundacao.Value;
            if (dto.Nacionalidade != null) clube.Nacionalidade = dto.Nacionalidade;
            if (dto.UrlLogo != null) clube.UrlLogo = dto.UrlLogo;
            if (dto.Status.HasValue) clube.Status = dto.Status.Value;

            ValidarClube(clube);

            _dataContext.SaveChanges();

            return clube;
        }

        public void ExcluirClube(Guid id)
        {
            var clube = BuscarClubePorId(id);

            _dataContext.Clubes.Remove(clube);
            _dataContext.SaveChanges();
        }
    }
}
