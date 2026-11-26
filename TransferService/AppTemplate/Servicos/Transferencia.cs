using TransferService.Servicos;

namespace TransferService.Servicos
{
    public class Transferencia
    {
        public Guid Id { get; set; }
        public Guid JogadorId { get; set; }
        public Guid ClubeDestinoId { get; set; }
        public DateTime DataEfetivacao { get; set; }
        public Decimal ValorTransferencia { get; set; }
    }
}
