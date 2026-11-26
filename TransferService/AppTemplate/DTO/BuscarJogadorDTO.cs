namespace TransferService.DTO
{
    public class BuscarJogadorDTO
    {
        public Guid Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Sobrenome { get; set; } = string.Empty;

        public DateTime DataNascimento { get; set; }

        public string PeDominante { get; set; }

        public string Posicao { get; set; }

        public short Altura { get; set; }

        public string Nacionalidade { get; set; } = string.Empty;

        public string Status { get; set; }
    }
}
