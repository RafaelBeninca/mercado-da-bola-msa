using ContractService.Servicos;

namespace ContractService.DTO
{
    public class AtualizarContratoDTO
    {
        public DateTime? DataFimEstiamda { get; set; }
        public Decimal? SalarioMes { get; set; }
        public Decimal? MultaRescisoria { get; set; }
        public string? Descricao { get; set; }
    }
}
