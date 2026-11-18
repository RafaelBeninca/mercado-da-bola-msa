using PlayerService.Servicos;

namespace PlayerService.DTO
{
    public class FiltroJogadorDTO
    {
        public string? Nome { get; set; }

        public string? Nacionalidade { get; set; }

        public EnumPeDominante? PeDominante { get; set; }

        public EnumPosicao? Posicao { get; set; }

        public EnumStatusJogador? Status { get; set; }

        public int Pagina { get; set; } = 1;

        public int TamanhoPagina { get; set; } = 20;
    }
}
