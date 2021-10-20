using AutoMapper;
using SisClientes.Data.Dtos;
using SisClientes.Models;

namespace SisClientes.Profiles
{
    public class ClientesProfile : Profile
    {
        public ClientesProfile()
        {
            CreateMap<Clientes, LerTodosClientesDto>();
            CreateMap<Clientes, LerClienteSemCidadeDto>();
            CreateMap<AtualizarClienteDto, Clientes>();
        }
    }
}
