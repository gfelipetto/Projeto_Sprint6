using AutoMapper;
using SisProdutos.Data.Dtos.Produto;
using SisProdutos.Models;

namespace SisProdutos.Profiles
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<Produtos, LerProdutoDto>();
        }
    }
}
