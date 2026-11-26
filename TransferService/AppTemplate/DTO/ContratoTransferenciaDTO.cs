namespace TransferService.DTO
{
    public class ContratoTransferenciaDTO
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFimEstiamda { get; set; }
        public Decimal SalarioMes { get; set; }
        public Decimal MultaRescisoria { get; set; }
        public string Descricao { get; set; }
    }
}
