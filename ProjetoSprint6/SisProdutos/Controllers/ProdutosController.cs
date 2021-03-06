using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SisProdutos.Data.Dtos.Produto;
using SisProdutos.Models;
using SisProdutos.Services;
using System;
using System.Threading.Tasks;

namespace SisProdutos.Controllers
{
    [ApiController]
    //[Authorize]
    [Route(template: "v1/SisProdutos/produtos")]
    public class ProdutosController : ControllerBase
    {
        private ProdutosService _produtosService;
        public ProdutosController(ProdutosService produtosService)
        {
            _produtosService = produtosService;
        }

        [HttpGet(template: "mostrar")]
        public async Task<IActionResult> GetTodosProdutosAsync([FromQuery] ProdutosFiltro filtro, [FromQuery] ProdutosOrdem ordem)
        {
            var produtos = await _produtosService.GetTodosProdutos(filtro, ordem);
            return Ok(produtos);
        }

        [HttpGet(template: "mostrar/{id}")]
        public async Task<IActionResult> GetProdutoPorIdAsync(Guid id)
        {
            var produto = await _produtosService.GetProdutoPorIdAsync(id);
            if (produto.IsFailed) return NotFound(produto.Errors);
            return Ok(produto.Successes);
        }

        [HttpPost(template: "cadastrar")]
        public async Task<IActionResult> CadastrarNovoProdutoAsync([FromBody] CadastrarProdutosDto produtoNovo)
        {
            if (!ModelState.IsValid) return BadRequest(produtoNovo);

            var resultado = await _produtosService.CadastrarNovoProdutoAsync(produtoNovo);
            if (resultado == null) return BadRequest();
            
            return Ok(resultado);
        }

        [HttpDelete(template: "deletar/{id}")]
        public async Task<IActionResult> DeletarProdutoAsync(Guid id)
        {
            var resultado = await _produtosService.DeletarProdutoAsync(id);
            if (resultado.IsFailed) return BadRequest(resultado.Errors);
            return NoContent();
        }
    }
}
