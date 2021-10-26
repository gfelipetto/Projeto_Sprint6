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
    public class ComprasService
    {
        private readonly IList<Produtos> _listaProdutos;
        private readonly SisProdutosDbContext _sisProdutosDbContext;
        private readonly IMapper _mapper;
        public ComprasService(SisProdutosDbContext sisProdutosDbContext, IMapper mapper)
        {
            _sisProdutosDbContext = sisProdutosDbContext;
            _mapper = mapper;
            _listaProdutos = new List<Produtos>();
        }
        public async Task<Produtos> AdicionarProdutoAoCarrinhoAsync(Guid id)
        {
            var produto = await _sisProdutosDbContext.Produtos.FirstOrDefaultAsync(p => p.Id == id);
            produto.Cidades = null;
            produto.PalavrasChave = null;
            produto.Categorias = null;
            if (produto == null) return null;

            _listaProdutos.Add(produto);
            return produto;
        }

        public List<LerProdutoDto> GetListaDeComprasAsync()
        {
            var listaProdutosDto = _mapper.Map<List<Produtos>, List<LerProdutoDto>>(_listaProdutos.ToList());
            return listaProdutosDto;
        }

        public async Task<Result> RemoverProdutoDoCarrinhoAsync(Guid id)
        {
            var produto = await _sisProdutosDbContext.Produtos.FirstOrDefaultAsync(p => p.Id == id);
            if (produto == null) return Result.Fail("Produto não encontrado");

            _listaProdutos.Remove(produto);
            return Result.Ok();
        }

        public Result FinalizarCompras()
        {
            decimal valorTotal = decimal.Zero;
            foreach (var produto in _listaProdutos)
            {
                valorTotal += produto.Preco;
                //foreach (var cidade in produto.Cidades)
                //{
                //    if (cidade.Nome !=  )
                //    {

                //    }
                //}
            }
            if (valorTotal > decimal.Zero)
            {

                return Result.Ok().WithSuccess("Valor total a pagar: " + valorTotal.ToString());
            }
            return Result.Fail("Não existe itens no carrinho");
        }
    }
}
