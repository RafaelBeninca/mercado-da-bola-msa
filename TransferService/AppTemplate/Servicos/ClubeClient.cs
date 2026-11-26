using TransferService.DTO;

namespace TransferService.Servicos
{
    public class ClubeClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ClubeClient(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public BuscarClubeDTO BuscarClube(Guid id)
        {
            var urlClubeApp = _configuration["ClubeUrl"];

            if (string.IsNullOrEmpty(urlClubeApp))
            {
                throw new Exception("A URL do micro de Clube não foi configurada.");
            }

            var metodoBuscarConta = urlClubeApp + "/api/Clube/" + id;

            var response = _httpClient.GetAsync(metodoBuscarConta).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao buscar clube.");
            }

            var clubeDto = response.Content.ReadFromJsonAsync<BuscarClubeDTO>().Result;

            return clubeDto;
        }

    }
}
