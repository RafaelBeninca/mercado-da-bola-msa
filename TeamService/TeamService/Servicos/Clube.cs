namespace TeamService.Servicos
{
    public class Clube
    {
        public Guid Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public DateTime DataFundacao { get; set; }

        public string Nacionalidade { get; set; } = string.Empty;

        public string? UrlLogo { get; set; }

        public EnumStatusClube Status { get; set; }
    }

    public enum EnumStatusClube
    {
        Ativo = 1,
        Inativo = 2
    }
}
