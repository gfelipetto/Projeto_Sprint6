using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using SisProdutos.Data;
using SisProdutos.Data.Dtos.Produto;
using SisProdutos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SisProdutos.Services
{
    public class ProdutosService
    {
        private readonly SisProdutosDbContext _sisProdutosDbContext;
        private readonly IMapper _mapper;

        public ProdutosService(SisProdutosDbContext sisProdutosDbContext, IMapper mapper)
        {
            _sisProdutosDbContext = sisProdutosDbContext;
            _mapper = mapper;
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
                _sisProdutosDbContext.Produtos.Add(novoProduto);

                if (produtoNovo.Cidade.Count > 0)
                {
                    var listaCidades = new List<ProdutoCidades>();
                    foreach (var cidade in produtoNovo.Cidade)
                    {
                        var novaCidade = new ProdutoCidades
                        {
                            Nome = cidade.Nome,
                            Estado = cidade.Estado,
                            ProdutoId = novoProduto.Id
                        };
                        listaCidades.Add(novaCidade);
                    }
                    await _sisProdutosDbContext.ProdutoCidades.AddRangeAsync(listaCidades);
                }

                if (produtoNovo.Categorias.Count > 0)
                {
                    var listaCategorias = new List<Categorias>();
                    foreach (var categoria in produtoNovo.Categorias)
                    {
                        var novaCategoria = new Categorias
                        {
                            Nome = categoria.Nome,
                            ProdutoId = novoProduto.Id
                        };
                        listaCategorias.Add(novaCategoria);
                    }
                    await _sisProdutosDbContext.Categorias.AddRangeAsync(listaCategorias);
                }

                if (produtoNovo.PalavrasChave.Count > 0)
                {
                    var listaPalavrasChave = new List<PalavrasChave>();
                    foreach (var palavraChave in produtoNovo.PalavrasChave)
                    {
                        var novaPalavraChave = new PalavrasChave
                        {
                            Nome = palavraChave.Nome,
                            ProdutoId = novoProduto.Id
                        };
                        listaPalavrasChave.Add(novaPalavraChave);
                    }
                    await _sisProdutosDbContext.PalavrasChave.AddRangeAsync(listaPalavrasChave);
                }

                await _sisProdutosDbContext.SaveChangesAsync();
                return novoProduto;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<Result> DeletarProdutoAsync(Guid id)
        {
            var produto = _sisProdutosDbContext.Produtos.FirstOrDefault(p => p.Id == id);
            if (produto == null) return Result.Fail("Produto não encontrado");

            _sisProdutosDbContext.Produtos.Remove(produto);
            await _sisProdutosDbContext.SaveChangesAsync();
            return Result.Ok();
        }
        public async Task<List<LerProdutoDto>> GetTodosProdutos(ProdutosFiltro filtro, ProdutosOrdem ordem)
        {
            var listaProdutos = await _sisProdutosDbContext.Produtos
                .Include("Cidades")
                .Include("PalavrasChave")
                .Include("Categorias")
                .AplicarFiltro(_sisProdutosDbContext,filtro)
                .AplicarOrdem(ordem)
                .ToListAsync();

            var listaProdutosDto = _mapper.Map<List<Produtos>, List<LerProdutoDto>>(listaProdutos);
            return listaProdutosDto;
        }
        public async Task<LerProdutoDto> GetProdutoPorIdAsync(Guid id)
        {
            var produto = await _sisProdutosDbContext.Produtos.Include("Cidades").Include(p => p.PalavrasChave).Include("Categorias").AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if (produto == null) return null;

            var produtoDto = _mapper.Map<LerProdutoDto>(produto);
            return produtoDto;
        }
    }
}
