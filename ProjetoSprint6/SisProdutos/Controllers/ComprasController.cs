using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SisProdutos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SisProdutos.Controllers
{
    [ApiController]
    [Authorize]
    [Route(template: "v1/SisProdutos/compra")]
    public class ComprasController : ControllerBase
    {
        private readonly ComprasService _comprasService;

        public ComprasController(ComprasService comprasService)
        {
            _comprasService = comprasService;
        }
        [HttpGet(template: "mostrar")]
        public async Task<IActionResult> GetListaDeComprasAsync()
        {
            var lista = await _comprasService.GetListaDeComprasAsync();
            return Ok(lista);
        }

        [HttpPost(template: "adicionar/{id}")]
        public async Task<IActionResult> AdicionarProdutoAoCarrinhoAsync(Guid id)
        {
            var resultado = await _comprasService.AdicionarProdutoAoCarrinhoAsync(id);
            if (resultado == null) return BadRequest();
            return Ok();
        }

        [HttpDelete(template: "remover/{id}")]
        public async Task<IActionResult> RemoverProdutoDoCarrinhoAsync(Guid id)
        {
            var resultado = await _comprasService.RemoverProdutoDoCarrinhoAsync(id);
            if (resultado.IsFailed) return BadRequest(resultado.Errors);
            return NoContent();
        }

        [HttpGet(template: "finalizar")]
        public async Task<IActionResult> FinalizarCompras()
        {
            var resultado = await _comprasService.FinalizarCompras();
            if (resultado.IsFailed) return Ok(resultado.Errors);
            return Ok(resultado.Successes);
        }
    }
}
