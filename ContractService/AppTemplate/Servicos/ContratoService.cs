using ContractService.Infra;
using ContractService.DTO;
using ContractService.Servicos;
using Microsoft.EntityFrameworkCore;

namespace ContractService.Servicos
{
    public class ContratoService
    {
        private const int TamanhoMaximoPagina = 100;
        private readonly DataContext _dataContext;
        private JogadorClient _jogadorClient;
        private ClubeClient _clubeClient;


        public ContratoService(
            DataContext dataContext,
            JogadorClient jogadorClient,
            ClubeClient clubeClient
        )
        {
            _dataContext = dataContext;
            _jogadorClient = jogadorClient;
            _clubeClient = clubeClient;
        }

        public Contrato InserirContrato(InserirContratoDTO novoContrato)
        {
            var contrato = new Contrato();

            var filtro = new FiltroContratoDTO();

            filtro.JogadorId = novoContrato.JogadorId;
            filtro.Status = EnumStatusContrato.Vigente;

            var contratosVigentes = this.ListarContratos(filtro);

            if (contratosVigentes.Itens.Count > 0)
            {
                throw new Exception("Jogador não pode ter mais de um contrato 'Vigente' ao mesmo tempo.");
            }

            contrato.Id = Guid.NewGuid();
            contrato.JogadorId = novoContrato.JogadorId;
            contrato.ClubeId = novoContrato.ClubeId;
            contrato.DataInicio = novoContrato.DataInicio;
            contrato.DataFimEstiamda = novoContrato.DataFimEstiamda;
            contrato.SalarioMes = novoContrato.SalarioMes;
            contrato.MultaRescisoria = novoContrato.MultaRescisoria;
            contrato.Status = EnumStatusContrato.Vigente;
            contrato.Descricao = novoContrato.Descricao;

            ValidarContrato(contrato);

            var alterarJogador = new AlterarJogadorDTO();
            alterarJogador.Status = EnumStatusJogador.Ativo;
            _jogadorClient.AlterarJogador(contrato.JogadorId, alterarJogador);

            _dataContext.Add(contrato);
            _dataContext.SaveChanges();

            return contrato;
        }

        public void ValidarContrato(Contrato contrato)
        {
            if (contrato.JogadorId == Guid.Empty)
            {
                throw new Exception("ID de Jogador está vazio.");
            }

            var jogador = _jogadorClient.BuscarJogador(contrato.JogadorId);

            if (jogador == null)
            {
                throw new Exception("Jogador não encontrado.");
            }

            if (contrato.ClubeId == Guid.Empty)
            {
                throw new Exception("ID de Clube está vazio.");
            }

            var clube = _clubeClient.BuscarClube(contrato.ClubeId);

            if (clube == null)
            {
                throw new Exception("Clube não encontrado.");
            }

            if (contrato.DataInicio == DateTime.MinValue)
            {
                throw new Exception("Data de Ínicio está vazia.");
            }

            if (contrato.DataFimEstiamda == DateTime.MinValue)
            {
                throw new Exception("Data de Fim Estimada está vazia.");
            }

            // if (contrato.DataEncerramento == DateTime.MinValue)
            // {
            //    throw new Exception("Data de Encerramento está vazia.");
            //}

            if (contrato.SalarioMes < 1)
            {
                throw new Exception("Salário do Contrato não pode ser menor que 1.");
            }

            if (contrato.MultaRescisoria < 1)
            {
                throw new Exception("Multa Rescisória não pode ser menor que 1.");
            }
        }

        public ResultadoPaginadoDTO<Contrato> ListarContratos(FiltroContratoDTO filtro)
        {
            var pagina = Math.Max(filtro.Pagina, 1);
            var tamanhoPagina = Math.Clamp(filtro.TamanhoPagina, 1, TamanhoMaximoPagina);

            var consulta = _dataContext.Contratos.AsQueryable();

            if (filtro.JogadorId.HasValue)
            {
                consulta = consulta.Where(p => p.JogadorId == filtro.JogadorId.Value);
            }

            if (filtro.ClubeId.HasValue)
            {
                consulta = consulta.Where(p => p.ClubeId == filtro.ClubeId.Value);
            }

            if (filtro.Status.HasValue)
            {
                consulta = consulta.Where(p => p.Status == filtro.Status.Value);
            }

            var totalItens = consulta.Count();

            var itens = consulta
                .OrderBy(p => p.Id)
                .ThenBy(p => p.JogadorId)
                .ThenBy(p => p.ClubeId)
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToList();

            return new ResultadoPaginadoDTO<Contrato>
            {
                Itens = itens,
                Pagina = pagina,
                TamanhoPagina = tamanhoPagina,
                TotalItens = totalItens,
                TotalPaginas = (int)Math.Ceiling(totalItens / (double)tamanhoPagina)
            };
        }

        public Contrato BuscarContratoPorId(Guid id)
        {
            var contrato = _dataContext.Contratos.FirstOrDefault(p => p.Id == id);

            if (contrato == null)
            {
                throw new Exception("Contrato não encontrado.");
            }

            return contrato;
        }

        public ContratoAtualizadoDTO AtualizarContrato(Guid id, AtualizarContratoDTO contrato)
        {
            var contratoAntigo = this.BuscarContratoPorId(id);

            if (contratoAntigo.Status != EnumStatusContrato.Vigente)
            {
                throw new Exception("Contrato precisar ser 'Vigente' para ser editado.");
            }

            var contratoNovo = new Contrato();
            contratoNovo.Id = Guid.NewGuid();
            contratoNovo.JogadorId = contratoAntigo.JogadorId;
            contratoNovo.ClubeId = contratoAntigo.ClubeId;
            contratoNovo.DataInicio = contratoAntigo.DataInicio;
            contratoNovo.DataFimEstiamda = contratoAntigo.DataFimEstiamda;
            contratoNovo.DataEncerramento = contratoAntigo.DataEncerramento;
            contratoNovo.SalarioMes = contratoAntigo.SalarioMes;
            contratoNovo.MultaRescisoria = contratoAntigo.MultaRescisoria;
            contratoNovo.Descricao = contratoAntigo.Descricao;


            if (contrato.DataFimEstiamda != DateTime.MinValue) contratoNovo.DataFimEstiamda = (DateTime)contrato.DataFimEstiamda;
            if (contrato.SalarioMes > 0) contratoNovo.SalarioMes = (Decimal)contrato.SalarioMes;
            if (contrato.MultaRescisoria > 0) contratoNovo.MultaRescisoria = (Decimal)contrato.MultaRescisoria;
            if (contrato.Descricao != null) contratoNovo.Descricao = contrato.Descricao;

            ValidarContrato(contratoNovo);

            contratoAntigo.Status = EnumStatusContrato.Invalido;
            contratoNovo.Status = EnumStatusContrato.Vigente;

            _dataContext.Add(contratoNovo);
            _dataContext.SaveChanges();
            
            var contratoAlteracao = new ContratoAtualizadoDTO();
            contratoAlteracao.ContratoInativadoId = contratoAntigo.Id;
            contratoAlteracao.NovoContratoId = contratoNovo.Id;

            return contratoAlteracao;
        }

        public Contrato RescindirContrato(Guid id)
        {
            var contrato = this.BuscarContratoPorId(id);

            if (contrato.Status != EnumStatusContrato.Vigente)
            {
                throw new Exception("Contrato precisar ser 'Vigente' para ser rescindido.");
            }

            var alterarJogador = new AlterarJogadorDTO();
            alterarJogador.Status = EnumStatusJogador.SemClube;
            _jogadorClient.AlterarJogador(contrato.JogadorId, alterarJogador);

            contrato.Status = EnumStatusContrato.Rescindido;
            contrato.DataEncerramento = DateTime.Now;
            _dataContext.SaveChanges();

            return contrato;
        }
    }
}
