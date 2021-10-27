using FluentResults;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SisProdutos.Data.Dtos;
using SisProdutos.Data.Dtos.Usuario;
using SisProdutos.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SisProdutos.HttpClients
{
    public class SisClientesApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public SisClientesApiClient(HttpClient client, IConfiguration configuration)
        {
            _httpClient = client;
            _configuration = configuration;
        }

        public async Task<Result> CadastrarClienteApiAsync(CadastrarUsuarioDto usuario)
        {
            try
            {
                var novoCliente = new CadastrarUsuarioViaClientDto
                {
                    Nome = usuario.ClientName,
                    DataNascimento = usuario.BirthDate,
                    Cep = usuario.Cep
                };
                HttpContent content = new StringContent(JsonConvert.SerializeObject(novoCliente), Encoding.UTF8, "application/json");
                var resposta = await _httpClient.PostAsync(_configuration.GetSection("EndpointSisClientesClienteCadastrar").Value + usuario.Id, content);

                resposta.EnsureSuccessStatusCode();
                if (resposta.IsSuccessStatusCode) return Result.Ok();
                return Result.Fail("Falha ao cadastrar cliente");
            }
            catch (Exception)
            {
                return Result.Fail("Falha ao cadastrar cliente");
            }

        }
        public async Task<UsuarioClienteApiClient> GetClienteApiAsync(Guid id)
        {
            var resposta = await _httpClient.GetAsync(_configuration.GetSection("EndpointSisClientesPegarCliente").Value + id);
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<UsuarioClienteApiClient>();
        }

        public async Task<UsuarioCidadeApiClient> GetCidadeApiAsync(Guid id)
        {
            var resposta = await _httpClient.GetAsync(_configuration.GetSection("EndpointSisClientesPegarCidade").Value + id);
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<UsuarioCidadeApiClient>();
        }
    }
}
