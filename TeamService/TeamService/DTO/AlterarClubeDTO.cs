using TeamService.Servicos;

namespace TeamService.DTO
{
    public class AlterarClubeDTO
    {
        public string? Nome { get; set; }

        public DateTime? DataFundacao { get; set; }

        public string? Nacionalidade { get; set; }

        public string? UrlLogo { get; set; }

        public EnumStatusClube? Status { get; set; }
    }
}
