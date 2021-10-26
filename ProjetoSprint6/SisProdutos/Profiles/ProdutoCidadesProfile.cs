using AutoMapper;
using SisProdutos.Data.Dtos.Cidade;
using SisProdutos.Models;

namespace SisProdutos.Profiles
{
    public class ProdutoCidadesProfile : Profile
    {
        public ProdutoCidadesProfile()
        {
            CreateMap<ProdutoCidades, LerCidadeDto>();
        }
    }
}
