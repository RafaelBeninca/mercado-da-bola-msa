namespace ContractService.DTO
{
    public class InserirContratoDTO
    {
        public Guid JogadorId { get; set; }
        public Guid ClubeId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFimEstiamda { get; set; }
        public Decimal SalarioMes {  get; set; }
        public Decimal MultaRescisoria { get; set; }
        public string Descricao { get; set; }

    }
}
