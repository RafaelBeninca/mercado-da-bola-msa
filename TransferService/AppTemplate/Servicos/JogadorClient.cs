using TransferService.DTO;

namespace TransferService.Servicos
{
    public class JogadorClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public JogadorClient(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public BuscarJogadorDTO BuscarJogador(Guid id)
        {
            var urlJogadorApp = _configuration["JogadorUrl"];

            if (string.IsNullOrEmpty(urlJogadorApp))
            {
                throw new Exception("A URL do micro de Jogador não foi configurada.");
            }

            var metodoBuscarConta = urlJogadorApp + "/api/Jogador/" + id;

            var response = _httpClient.GetAsync(metodoBuscarConta).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao buscar jogador.");
            }

            var jogadorDto = response.Content.ReadFromJsonAsync<BuscarJogadorDTO>().Result;

            return jogadorDto;
        }

        

    }
}
