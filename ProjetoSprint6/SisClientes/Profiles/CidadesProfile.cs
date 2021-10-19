using AutoMapper;
using SisClientes.Data.Dtos;
using SisClientes.Models;

namespace SisClientes.Profiles
{
    public class CidadesProfile : Profile
    {
        public CidadesProfile()
        {
            CreateMap<Cidades, LerTodasCidadesDto>();
            CreateMap<AtualizarCidadeDto, Cidades>();
        }
    }
}
