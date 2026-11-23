using TeamService.Servicos;

namespace TeamService.DTO
{
    public class FiltroClubeDTO
    {
        public string? Nome { get; set; }

        public string? Nacionalidade { get; set; }

        public EnumStatusClube? Status { get; set; }

        public int Pagina { get; set; } = 1;

        public int TamanhoPagina { get; set; } = 20;
    }
}
