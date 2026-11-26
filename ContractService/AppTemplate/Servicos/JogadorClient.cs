using ContractService.DTO;
using System.Diagnostics.Contracts;
using System.Text;
using System.Text.Json;

namespace ContractService.Servicos
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

            var metodoBuscarJogador = urlJogadorApp + "/api/Jogador/" + id;

            var response = _httpClient.GetAsync(metodoBuscarJogador).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao buscar jogador.");
            }

            var jogadorDto = response.Content.ReadFromJsonAsync<BuscarJogadorDTO>().Result;

            return jogadorDto;
        }

        public BuscarJogadorDTO AlterarJogador(Guid id, AlterarJogadorDTO jogador)
        {
            var urlJogadorApp = _configuration["JogadorUrl"];

            if (string.IsNullOrEmpty(urlJogadorApp))
            {
                throw new Exception("A URL do micro de Jogador não foi configurada.");
            }

            var metodoAlterarJogador = urlJogadorApp + "/api/Jogador/" + id;

            string jsonString = JsonSerializer.Serialize(jogador);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = _httpClient.PatchAsync(metodoAlterarJogador, content).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao atualizar jogador.");
            }

            var jogadorDto = response.Content.ReadFromJsonAsync<BuscarJogadorDTO>().Result;

            return jogadorDto;
        }



    }
}
