namespace ContractService.Servicos
{
    public class Contrato
    {
        public Guid Id { get; set; }
        public Guid JogadorId { get; set; }
        public Guid ClubeId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFimEstiamda { get; set; }
        public DateTime DataEncerramento { get; set; }
        public Decimal SalarioMes {  get; set; }
        public Decimal MultaRescisoria { get; set; }
        public EnumStatusContrato Status { get; set; }
        public string Descricao { get; set; }

    }

    public enum EnumStatusContrato
    {
        Vigente = 1,
        Rescindido = 2,
        Expirado = 3,
        Invalido = 4
    }
}
