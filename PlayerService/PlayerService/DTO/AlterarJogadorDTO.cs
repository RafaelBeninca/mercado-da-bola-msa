using PlayerService.Servicos;

namespace PlayerService.DTO
{
    public class AlterarJogadorDTO
    {
        public string? Nome { get; set; }

        public string? Sobrenome { get; set; }

        public DateTime? DataNascimento { get; set; }

        public EnumPeDominante? PeDominante { get; set; }

        public EnumPosicao? Posicao { get; set; }

        public short? Altura { get; set; }

        public string? Nacionalidade { get; set; }

        public EnumStatusJogador? Status { get; set; }
    }
}
