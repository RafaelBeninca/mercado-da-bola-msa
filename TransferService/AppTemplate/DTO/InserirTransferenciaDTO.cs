namespace TransferService.DTO
{
    public class InserirTransferenciaDTO
    {
        public Guid JogadorId { get; set; }
        public Guid ClubeDestinoId { get; set; }
        public DateTime DataEfetivacao { get; set; }
        public Decimal ValorTransferencia { get; set; }
        public ContratoTransferenciaDTO Contrato { get; set; }
    }
}
