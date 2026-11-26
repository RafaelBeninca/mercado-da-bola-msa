using ContractService.Servicos;

namespace ContractService.DTO
{
    public class FiltroContratoDTO
    {
        public EnumStatusContrato? Status { get; set; }

        public Guid? JogadorId { get; set; }

        public Guid? ClubeId { get; set; }
        public int Pagina { get; set; } = 1;

        public int TamanhoPagina { get; set; } = 20;
    }
}
