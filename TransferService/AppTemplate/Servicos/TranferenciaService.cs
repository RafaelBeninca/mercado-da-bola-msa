using TransferService.Infra;
using TransferService.DTO;
using TransferService.Servicos;
using Microsoft.EntityFrameworkCore;
using TransferService.Servicos;
using TransferService.DTO;
using ContractService;

namespace TransferService.Servicos
{
    public class TranferenciaService
    {
        private const int TamanhoMaximoPagina = 100;
        private readonly DataContext _dataContext;
        private JogadorClient _jogadorClient;
        private ClubeClient _clubeClient;
        private ContratoClient _contratoClient;


        public TranferenciaService(
            DataContext dataContext,
            JogadorClient jogadorClient,
            ClubeClient clubeClient,
            ContratoClient contratoClient
        )
        {
            _dataContext = dataContext;
            _jogadorClient = jogadorClient;
            _clubeClient = clubeClient;
            _contratoClient = contratoClient;
        }

        public void InserirTranferencia(InserirTransferenciaDTO novaTransferencia)
        {
            var filtro = new FiltroContratoDTO();
            filtro.Status = EnumStatusContrato.Vigente;
            filtro.JogadorId = novaTransferencia.JogadorId;

            var contratoVigente = _contratoClient.BuscarContratos(filtro);

            if (contratoVigente.Itens.Count < 1)
            {
                throw new Exception("O Jogador não possui Contrato 'Vigente'. Não pode ser transferido");
            }

            if (contratoVigente.Itens[0].ClubeId == novaTransferencia.ClubeDestinoId)
            {
                throw new Exception("O Jogador não pode ser transferido para o mesmo clube que está.");
            }

            var novoContrato = new InserirContratoDTO();
            novoContrato.JogadorId = novaTransferencia.JogadorId;
            novoContrato.ClubeId = novaTransferencia.ClubeDestinoId;
            novoContrato.DataInicio = novaTransferencia.Contrato.DataInicio;
            novoContrato.DataFimEstiamda = novaTransferencia.Contrato.DataFimEstiamda;
            novoContrato.SalarioMes = novaTransferencia.Contrato.SalarioMes;
            novoContrato.MultaRescisoria = novaTransferencia.Contrato.MultaRescisoria;
            novoContrato.Descricao = novaTransferencia.Contrato.Descricao;

            _contratoClient.RescindirContrato(contratoVigente.Itens[0].Id);
            _contratoClient.InserirContrato(novoContrato);

            var transferencia = new Transferencia();
            transferencia.Id = Guid.NewGuid();
            transferencia.JogadorId = novaTransferencia.JogadorId;
            transferencia.ClubeDestinoId = novaTransferencia.ClubeDestinoId;
            transferencia.DataEfetivacao = novaTransferencia.DataEfetivacao;
            transferencia.ValorTransferencia = novaTransferencia.ValorTransferencia;

            ValidarTranferencia(transferencia);

            _dataContext.Add(transferencia);
            _dataContext.SaveChanges();
        }

        public void ValidarTranferencia(Transferencia transferencia)
        {
            if (transferencia.JogadorId == Guid.Empty)
            {
                throw new Exception("ID de Jogador está vazio.");
            }

            var jogador = _jogadorClient.BuscarJogador(transferencia.JogadorId);

            if (jogador == null)
            {
                throw new Exception("Jogador não encontrado.");
            }

            if (transferencia.ClubeDestinoId == Guid.Empty)
            {
                throw new Exception("ID de Clube de Destino está vazio.");
            }

            var clube = _clubeClient.BuscarClube(transferencia.ClubeDestinoId);

            if (clube == null)
            {
                throw new Exception("Clube de Destino não encontrado.");
            }

            if (transferencia.DataEfetivacao == DateTime.MinValue)
            {
                throw new Exception("Data Efetivação está vazia.");
            }

            if (transferencia.ValorTransferencia < 1)
            {
                throw new Exception("Valor de Transferência não pode ser menor que 1.");
            }
        }

        public ResultadoPaginadoDTO<Transferencia> ListarTransferencias(FiltroTransferenciaDTO filtro)
        {
            var pagina = Math.Max(filtro.Pagina, 1);
            var tamanhoPagina = Math.Clamp(filtro.TamanhoPagina, 1, TamanhoMaximoPagina);

            var consulta = _dataContext.Transferencias.AsQueryable();

            if (filtro.JogadorId.HasValue)
            {
                consulta = consulta.Where(p => p.JogadorId == filtro.JogadorId.Value);
            }

            if (filtro.ClubeDestinoId.HasValue)
            {
                consulta = consulta.Where(p => p.ClubeDestinoId == filtro.ClubeDestinoId.Value);
            }

            var totalItens = consulta.Count();

            var itens = consulta
                .OrderBy(p => p.Id)
                .ThenBy(p => p.JogadorId)
                .ThenBy(p => p.ClubeDestinoId)
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToList();

            return new ResultadoPaginadoDTO<Transferencia>
            {
                Itens = itens,
                Pagina = pagina,
                TamanhoPagina = tamanhoPagina,
                TotalItens = totalItens,
                TotalPaginas = (int)Math.Ceiling(totalItens / (double)tamanhoPagina)
            };
        }

        public Transferencia BuscarTransferenciaPorId(Guid id)
        {
            var contrato = _dataContext.Transferencias.FirstOrDefault(p => p.Id == id);

            if (contrato == null)
            {
                throw new Exception("Transferência não encontrada.");
            }

            return contrato;
        }
    }
}
