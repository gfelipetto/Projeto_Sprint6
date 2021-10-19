using AutoMapper;
using SisClientes.Data.Dtos;
using SisClientes.Models;

namespace SisClientes.Profiles
{
    public class EnderecosProfile : Profile
    {
        public EnderecosProfile()
        {
            CreateMap<Enderecos, LerTodosEnderecosDto>();
            CreateMap<AtualizarEnderecoDto, Enderecos>();
        }
    }
}
