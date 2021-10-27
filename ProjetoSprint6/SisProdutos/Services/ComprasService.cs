using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Http;
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

namespace SisProdutos.Services
{
    public class ComprasService
    {
        private readonly SisProdutosDbContext _sisProdutosDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SisClientesApiClient _sisClientesApiClient;
        private readonly IMapper _mapper;

        public Guid UsuarioId { get; }
        public ComprasService(SisProdutosDbContext sisProdutosDbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor, SisClientesApiClient sisClientesApiClient)
        {
            _sisProdutosDbContext = sisProdutosDbContext;
            _httpContextAccessor = httpContextAccessor;
            _sisClientesApiClient = sisClientesApiClient;
            UsuarioId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            _mapper = mapper;
        }
        public async Task<Compras> AdicionarProdutoAoCarrinhoAsync(Guid id)
        {
            var produto = await _sisProdutosDbContext.Produtos.FirstOrDefaultAsync(p => p.Id == id);
            if (produto == null) return null;

            var produtoDto = _mapper.Map<Compras>(produto);
            produtoDto.IdUsuario = UsuarioId;

            await _sisProdutosDbContext.ProdutoCarrinho.AddAsync(produtoDto);
            await _sisProdutosDbContext.SaveChangesAsync();
            return produtoDto;
        }
        public async Task<List<Compras>> GetListaDeComprasAsync()
        {
            var listaProdutosDto = await _sisProdutosDbContext.ProdutoCarrinho.ToListAsync();
            return listaProdutosDto;
        }
        public async Task<Result> RemoverProdutoDoCarrinhoAsync(Guid id)
        {
            var produto = await _sisProdutosDbContext.ProdutoCarrinho.FirstOrDefaultAsync(p => p.Id == id);
            if (produto == null) return Result.Fail("Produto não encontrado");

            _sisProdutosDbContext.ProdutoCarrinho.Remove(produto);
            await _sisProdutosDbContext.SaveChangesAsync();
            return Result.Ok();
        }
        public async Task<Result> FinalizarCompras()
        {
            var clienteApi = await _sisClientesApiClient.GetClienteApiAsync(UsuarioId);
            var cidadeClienteApi = await _sisClientesApiClient.GetCidadeApiAsync(clienteApi.CidadeId);

            var listaProdutos = await _sisProdutosDbContext.ProdutoCarrinho.ToListAsync();

            decimal valorTotal = decimal.Zero;
            decimal frete = 29.90m;
            int cidadesDiferentes = 0;

            foreach (var produto in listaProdutos)
            {
                var cidade = _sisProdutosDbContext.ProdutoCidades.FirstOrDefault(c => c.ProdutoId == produto.Id);
                valorTotal += produto.Preco;

                if (cidade.Nome != cidadeClienteApi.Nome && cidade.Estado != cidadeClienteApi.Estado)
                {
                    cidadesDiferentes++;
                }
            }

            valorTotal += (frete * cidadesDiferentes);
            if (valorTotal > decimal.Zero)
            {
                _sisProdutosDbContext.ProdutoCarrinho.RemoveRange(listaProdutos);
                await _sisProdutosDbContext.SaveChangesAsync();
                return Result.Ok().WithSuccess(valorTotal.ToString());
            }
            return Result.Fail(valorTotal.ToString());
        }
    }
}
