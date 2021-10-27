using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using SisProdutos.Data;
using SisProdutos.Data.Dtos.Produto;
using SisProdutos.HttpClients;
using SisProdutos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace SisProdutos.Services
{
    public class ProdutosService
    {
        private readonly SisProdutosDbContext _sisProdutosDbContext;
        private readonly AuditoriaApiClient _auditoriaApiClient;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Guid UsuarioId { get; }
        public ProdutosService(SisProdutosDbContext sisProdutosDbContext, IMapper mapper, AuditoriaApiClient auditoriaApiClient, IHttpContextAccessor httpContextAccessor)
        {
            _sisProdutosDbContext = sisProdutosDbContext;
            _auditoriaApiClient = auditoriaApiClient;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            UsuarioId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
        public async Task<Produtos> CadastrarNovoProdutoAsync(CadastrarProdutosDto produtoNovo)
        {
            try
            {
                var novoProduto = new Produtos
                {
                    Nome = produtoNovo.Nome,
                    Descricao = produtoNovo.Descricao,
                    Preco = produtoNovo.Preco
                };
                if (produtoNovo.Cidade.Count > 0)
                {
                    var listaCidades = new List<ProdutoCidades>();
                    foreach (var cidade in produtoNovo.Cidade)
                    {
                        var novaCidade = new ProdutoCidades
                        {
                            Nome = cidade.Nome,
                            Estado = cidade.Estado,
                        };
                        listaCidades.Add(novaCidade);
                    }
                    novoProduto.Cidades = listaCidades;
                }

                if (produtoNovo.Categorias.Count > 0)
                {
                    var listaCategorias = new List<Categorias>();
                    foreach (var categoria in produtoNovo.Categorias)
                    {
                        var novaCategoria = new Categorias
                        {
                            Nome = categoria.Nome,
                        };
                        listaCategorias.Add(novaCategoria);
                    }
                    novoProduto.Categorias = listaCategorias;
                }

                if (produtoNovo.PalavrasChave.Count > 0)
                {
                    var listaPalavrasChave = new List<PalavrasChave>();
                    foreach (var palavraChave in produtoNovo.PalavrasChave)
                    {
                        var novaPalavraChave = new PalavrasChave
                        {
                            Nome = palavraChave.Nome,
                        };
                        listaPalavrasChave.Add(novaPalavraChave);
                    }
                    novoProduto.PalavrasChave = listaPalavrasChave;
                }
                _sisProdutosDbContext.Produtos.Add(novoProduto);

                await _sisProdutosDbContext.SaveChangesAsync();
                return novoProduto;
            }
            catch (Exception ex)
            {
                var novaExcecao = new ExcecaoApiClient
                {
                    UsuarioId = UsuarioId,
                    Excecao = ex.Message
                };
                await _auditoriaApiClient.IncluirExcecaoAsync(novaExcecao);
                return null;
            }
        }
        public async Task<Result> DeletarProdutoAsync(Guid id)
        {
            try
            {
                var produto = _sisProdutosDbContext.Produtos.FirstOrDefault(p => p.Id == id);
                if (produto == null) return Result.Fail("Produto não encontrado");

                _sisProdutosDbContext.Produtos.Remove(produto);
                await _sisProdutosDbContext.SaveChangesAsync();
                return Result.Ok();
            }
            catch (Exception ex)
            {
                var novaExcecao = new ExcecaoApiClient
                {
                    UsuarioId = UsuarioId,
                    Excecao = ex.Message
                };
                await _auditoriaApiClient.IncluirExcecaoAsync(novaExcecao);
                return Result.Fail(ex.Message);
            }
        }
        public async Task<List<LerProdutoDto>> GetTodosProdutos(ProdutosFiltro filtro, ProdutosOrdem ordem)
        {
            try
            {
                var listaProdutos = await _sisProdutosDbContext.Produtos
                .Include("Cidades")
                .Include("PalavrasChave")
                .Include("Categorias")
                .AplicarFiltro(_sisProdutosDbContext, filtro)
                .AplicarOrdem(ordem)
                .AsNoTracking()
                .ToListAsync();

                var listaProdutosDto = _mapper.Map<List<Produtos>, List<LerProdutoDto>>(listaProdutos);
                return listaProdutosDto;
            }
            catch (Exception ex)
            {
                var novaExcecao = new ExcecaoApiClient
                {
                    UsuarioId = UsuarioId,
                    Excecao = ex.Message
                };
                await _auditoriaApiClient.IncluirExcecaoAsync(novaExcecao);
                return null;
            }
            
        }
        public async Task<LerProdutoDto> GetProdutoPorIdAsync(Guid id)
        {
            try
            {
                var produto = await _sisProdutosDbContext.Produtos.Include("Cidades").Include("PalavrasChave").Include("Categorias").AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                if (produto == null) return null;
                var novaAtividade = new AtividadeApiClient
                {
                    UsuarioId = UsuarioId,
                    Nome = produto.Nome,
                    Descricao = produto.Descricao,
                    Preco = produto.Preco
                };

                var resultado = await _auditoriaApiClient.IncluirAtividadeAsync(novaAtividade);
                if (resultado.IsFailed) return null;

                var produtoDto = _mapper.Map<LerProdutoDto>(produto);
                return produtoDto;

            }
            catch (Exception ex)
            {
                var novaExcecao = new ExcecaoApiClient
                {
                    UsuarioId = UsuarioId,
                    Excecao = ex.Message
                };
                await _auditoriaApiClient.IncluirExcecaoAsync(novaExcecao);
                return null;
            }
        }
    }
}
