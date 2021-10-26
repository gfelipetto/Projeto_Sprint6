using FluentResults;
using Newtonsoft.Json;
using SisProdutos.Data.Dtos;
using SisProdutos.Data.Dtos.Usuario;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SisProdutos.HttpClients
{
    public class SisClientesApiClient
    {
        private readonly HttpClient _httpClient;
        public SisClientesApiClient(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<Result> CadastrarClienteApi(CadastrarUsuarioDto usuario)
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
                var resposta = await _httpClient.PostAsync("v1/SisClientes/cliente/cadastrar/" + usuario.Id, content);

                resposta.EnsureSuccessStatusCode();
                if (resposta.IsSuccessStatusCode) return Result.Ok();
                return Result.Fail("Falha ao cadastrar cliente");
            }
            catch (Exception)
            {
                return Result.Fail("Falha ao cadastrar cliente");
            }

        }
    }
}
