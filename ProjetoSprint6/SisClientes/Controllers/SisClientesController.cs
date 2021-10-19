using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SisClientes.Data;
using SisClientes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SisClientes.Controllers
{
    [ApiController]
    [Route(template:"v1")]
    public class SisClientesController : ControllerBase
    {
        private readonly SisClientesDbContext _scContext;
        private readonly IMapper _mapper;
        public SisClientesController(SisClientesDbContext sisClienteContext, IMapper mapper)
        {
            _scContext = sisClienteContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route(template:"TodosClientes")]
        public async Task<IActionResult> GetTodosClientes()
        {
            
        } 
    }
}
