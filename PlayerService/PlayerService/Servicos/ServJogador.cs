using Microsoft.EntityFrameworkCore;
using PlayerService.DTO;

namespace PlayerService.Servicos
{
    public class ServJogador
    {
        private const int TamanhoMaximoPagina = 100;

        private readonly DataContext _dataContext;

        public ServJogador(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Jogador InserirJogador(InserirJogadorDTO jogador)
        {
            var novoJogador = new Jogador
            {
                Nome = jogador.Nome,
                Sobrenome = jogador.Sobrenome,
                DataNascimento = jogador.DataNascimento,
                PeDominante = jogador.PeDominante,
                Posicao = jogador.Posicao,
                Altura = jogador.Altura,
                Nacionalidade = jogador.Nacionalidade,
                Id = Guid.NewGuid(),
                Status = EnumStatusJogador.SemClube
            };

            ValidarJogador(novoJogador);

            _dataContext.Add(novoJogador);
            _dataContext.SaveChanges();

            return novoJogador;
        }

        public void ValidarJogador(Jogador jogador)
        {
            if (string.IsNullOrWhiteSpace(jogador.Nome))
            {
                throw new Exception("Falta informar o nome do jogador.");
            }

            if (string.IsNullOrWhiteSpace(jogador.Sobrenome))
            {
                throw new Exception("Falta informar o sobrenome do jogador.");
            }

            if (string.IsNullOrWhiteSpace(jogador.Nacionalidade))
            {
                throw new Exception("Falta informar a nacionalidade do jogador.");
            }

            if (jogador.DataNascimento == default || jogador.DataNascimento.Date > DateTime.Today)
            {
                throw new Exception("Data de nascimento inválida.");
            }

            if (jogador.Altura <= 0 || jogador.Altura > 300)
            {
                throw new Exception("A altura deve ser informada em centímetros (ex.: 178).");
            }

            if (!Enum.IsDefined(jogador.PeDominante))
            {
                throw new Exception("Pé dominante inválido.");
            }

            if (!Enum.IsDefined(jogador.Posicao))
            {
                throw new Exception("Posição inválida.");
            }

            if (!Enum.IsDefined(jogador.Status))
            {
                throw new Exception("Status inválido.");
            }
        }

        public ResultadoPaginadoDTO<Jogador> ListarJogadores(FiltroJogadorDTO filtro)
        {
            var pagina = Math.Max(filtro.Pagina, 1);
            var tamanhoPagina = Math.Clamp(filtro.TamanhoPagina, 1, TamanhoMaximoPagina);

            var consulta = _dataContext.Jogadores.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro.Nome))
            {
                consulta = consulta.Where(p => EF.Functions.Like(p.Nome, $"%{filtro.Nome}%"));
            }

            if (!string.IsNullOrWhiteSpace(filtro.Nacionalidade))
            {
                consulta = consulta.Where(p => EF.Functions.Like(p.Nacionalidade, filtro.Nacionalidade));
            }

            if (filtro.PeDominante.HasValue)
            {
                consulta = consulta.Where(p => p.PeDominante == filtro.PeDominante.Value);
            }

            if (filtro.Posicao.HasValue)
            {
                consulta = consulta.Where(p => p.Posicao == filtro.Posicao.Value);
            }

            if (filtro.Status.HasValue)
            {
                consulta = consulta.Where(p => p.Status == filtro.Status.Value);
            }

            var totalItens = consulta.Count();

            var itens = consulta
                .OrderBy(p => p.Nome)
                .ThenBy(p => p.Sobrenome)
                .ThenBy(p => p.Id)
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToList();

            return new ResultadoPaginadoDTO<Jogador>
            {
                Itens = itens,
                Pagina = pagina,
                TamanhoPagina = tamanhoPagina,
                TotalItens = totalItens,
                TotalPaginas = (int)Math.Ceiling(totalItens / (double)tamanhoPagina)
            };
        }

        public Jogador BuscarJogadorPorId(Guid id)
        {
            var jogador = _dataContext.Jogadores.FirstOrDefault(p => p.Id == id);

            if (jogador == null)
            {
                throw new Exception("Jogador não encontrado.");
            }

            return jogador;
        }

        public Jogador AlterarJogador(Guid id, AlterarJogadorDTO dto)
        {
            var jogador = BuscarJogadorPorId(id);

            if (dto.Nome != null) jogador.Nome = dto.Nome;
            if (dto.Sobrenome != null) jogador.Sobrenome = dto.Sobrenome;
            if (dto.DataNascimento.HasValue) jogador.DataNascimento = dto.DataNascimento.Value;
            if (dto.PeDominante.HasValue) jogador.PeDominante = dto.PeDominante.Value;
            if (dto.Posicao.HasValue) jogador.Posicao = dto.Posicao.Value;
            if (dto.Altura.HasValue) jogador.Altura = dto.Altura.Value;
            if (dto.Nacionalidade != null) jogador.Nacionalidade = dto.Nacionalidade;
            if (dto.Status.HasValue) jogador.Status = dto.Status.Value;

            ValidarJogador(jogador);

            _dataContext.SaveChanges();

            return jogador;
        }

        public void ExcluirJogador(Guid id)
        {
            var jogador = BuscarJogadorPorId(id);

            _dataContext.Jogadores.Remove(jogador);
            _dataContext.SaveChanges();
        }
    }
}
