namespace TransferService.DTO
{
    public class BuscarClubeDTO
    {
        public Guid Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public DateTime DataFundacao { get; set; }

        public string Nacionalidade { get; set; } = string.Empty;

        public string? UrlLogo { get; set; }

        public string Status { get; set; }
    }
}
