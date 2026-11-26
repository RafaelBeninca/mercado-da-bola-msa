using Microsoft.AspNetCore.WebUtilities;
using System.Buffers.Text;
using System.Text;
using System.Text.Json;
using TransferService.DTO;

namespace TransferService.Servicos
{
    public class ContratoClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ContratoClient(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public ResultadoPaginadoDTO<BuscarContratoDTO> BuscarContratos(FiltroContratoDTO filtro)
        {
            var urlContratoApp = _configuration["ContratoUrl"];

            if (string.IsNullOrEmpty(urlContratoApp))
            {
                throw new Exception("A URL do micro de Contrato não foi configurada.");
            }

            var metodoBuscarContratos = urlContratoApp + "/api/Contrato";
            var queryParams = new Dictionary<string, string?>();

            if (filtro != null)
            {
                if (filtro.Status.HasValue)
                    queryParams["Status"] = ((int)filtro.Status.Value).ToString();

                if (filtro.JogadorId.HasValue)
                    queryParams["JogadorId"] = filtro.JogadorId.Value.ToString();

                if (filtro.ClubeId.HasValue)
                    queryParams["ClubeId"] = filtro.ClubeId.Value.ToString();

                queryParams["Pagina"] = filtro.Pagina.ToString();
                queryParams["TamanhoPagina"] = filtro.TamanhoPagina.ToString();
            }

            var urlComQuery = QueryHelpers.AddQueryString(metodoBuscarContratos, queryParams);
            var response = _httpClient.GetAsync(urlComQuery).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao buscar contratos.");
            }

            var contratosDto = response.Content.ReadFromJsonAsync<ResultadoPaginadoDTO<BuscarContratoDTO>>().Result;

            return contratosDto;
        }

        public BuscarContratoDTO RescindirContrato(Guid id)
        {
            var urlContratoApp = _configuration["ContratoUrl"];

            if (string.IsNullOrEmpty(urlContratoApp))
            {
                throw new Exception("A URL do micro de Contrato não foi configurada.");
            }

            var metodoRescindirContrato = urlContratoApp + "/api/Contrato/rescindir/" + id;

            var response = _httpClient.PatchAsync(metodoRescindirContrato, null).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao rescindir contrato.");
            }

            var contratoRescindidoDto = response.Content.ReadFromJsonAsync<BuscarContratoDTO>().Result;

            return contratoRescindidoDto;
        }

        public BuscarContratoDTO InserirContrato(InserirContratoDTO contrato)
        {
            var urlContratoApp = _configuration["ContratoUrl"];

            if (string.IsNullOrEmpty(urlContratoApp))
            {
                throw new Exception("A URL do micro de Contrato não foi configurada.");
            }

            var metodoInserirContrato = urlContratoApp + "/api/Contrato";

            string jsonString = JsonSerializer.Serialize(contrato);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = _httpClient.PostAsync(metodoInserirContrato, content).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao inserir contrato.");
            }

            var contratoInseridoDto = response.Content.ReadFromJsonAsync<BuscarContratoDTO>().Result;

            return contratoInseridoDto;
        }

    }
}
