using TransferService.Servicos;

namespace TransferService.DTO
{
    public class FiltroTransferenciaDTO
    {

        public Guid? JogadorId { get; set; }

        public Guid? ClubeDestinoId { get; set; }
        public int Pagina { get; set; } = 1;

        public int TamanhoPagina { get; set; } = 20;
    }
}
