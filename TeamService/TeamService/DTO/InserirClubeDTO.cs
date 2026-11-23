using TeamService.Servicos;

namespace TeamService.DTO
{
    public class InserirClubeDTO
    {
        public string Nome { get; set; } = string.Empty;

        public DateTime DataFundacao { get; set; }

        public string Nacionalidade { get; set; } = string.Empty;

        public string? UrlLogo { get; set; }

        public EnumStatusClube Status { get; set; } = EnumStatusClube.Ativo;
    }
}
