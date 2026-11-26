namespace TransferService.DTO
{
    public class BuscarContratoDTO
    {
        public Guid Id { get; set; }
        public Guid JogadorId { get; set; }
        public Guid ClubeId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFimEstiamda { get; set; }
        public DateTime DataEncerramento { get; set; }
        public Decimal SalarioMes { get; set; }
        public Decimal MultaRescisoria { get; set; }
        public string Status { get; set; }
        public string Descricao { get; set; }

    }
}
