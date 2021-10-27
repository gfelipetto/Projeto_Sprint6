using FluentResults;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SisProdutos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SisProdutos.HttpClients
{
    public class AuditoriaApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public AuditoriaApiClient(HttpClient client, IConfiguration configuration)
        {
            _httpClient = client;
            _configuration = configuration;
        }

        public async Task<Result> IncluirAtividadeAsync(AtividadeApiClient atividadeApiClient)
        {
            try
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(atividadeApiClient), Encoding.UTF8, "application/json");
                var resposta = await _httpClient.PostAsync(_configuration.GetSection("EndpointAuditoria").Value, content);

                resposta.EnsureSuccessStatusCode();
                if (resposta.IsSuccessStatusCode) return Result.Ok();
                return Result.Fail("Falha ao cadastrar atividade");
            }
            catch (Exception)
            {
                return Result.Fail("Falha ao cadastrar atividade");
            }
        }

        public async Task<Result> IncluirExcecaoAsync(ExcecaoApiClient excecaoApiClient)
        {
            try
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(excecaoApiClient), Encoding.UTF8, "application/json");
                var resposta = await _httpClient.PostAsync(_configuration.GetSection("EndpointAuditoriaExcecao").Value, content);

                resposta.EnsureSuccessStatusCode();
                if (resposta.IsSuccessStatusCode) return Result.Ok();
                return Result.Fail("Falha ao cadastrar excecao");
            }
            catch (Exception)
            {
                return Result.Fail("Falha ao cadastrar excecao");
            }
        }
    }
}
