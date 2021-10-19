using SisClientes.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace SisClientes.HttpClients
{
    public class CepApiClient
    {
        private readonly HttpClient _httpClient;
        public CepApiClient(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<Cep> GetCepAsync(string cep)
        {
            var resposta = await _httpClient.GetAsync($"{cep}/json/");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<Cep>();
        }
    }
}
