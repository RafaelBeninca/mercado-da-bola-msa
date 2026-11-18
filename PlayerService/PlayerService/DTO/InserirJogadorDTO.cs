using PlayerService.Servicos;

namespace PlayerService.DTO
{
    public class InserirJogadorDTO
    {
        public string Nome { get; set; } = string.Empty;

        public string Sobrenome { get; set; } = string.Empty;

        public DateTime DataNascimento { get; set; }

        public EnumPeDominante PeDominante { get; set; }

        public EnumPosicao Posicao { get; set; }

        public short Altura { get; set; }

        public string Nacionalidade { get; set; } = string.Empty;

        public EnumStatusJogador Status { get; set; }
    }
}
