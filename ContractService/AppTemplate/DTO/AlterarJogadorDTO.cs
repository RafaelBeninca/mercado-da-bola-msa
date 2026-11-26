namespace ContractService.DTO
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

    public enum EnumPeDominante
    {
        Direito = 1,
        Esquerdo = 2,
        Ambidestro = 3
    }

    public enum EnumPosicao
    {
        Goleiro = 1,
        Zagueiro = 2,
        Lateral = 3,
        Volante = 4,
        Meia = 5,
        Ponta = 6,
        Atacante = 7
    }

    public enum EnumStatusJogador
    {
        Ativo = 1,
        Lesionado = 2,
        Suspenso = 3,
        SemClube = 4,
        Aposentado = 5
    }
}
