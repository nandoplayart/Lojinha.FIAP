using AutoMapper;
using Lojinha.FIAP.Core.Models;
using Lojinha.FIAP.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lojinha.FIAP.Infrastructure.Mappings
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<Produto, ProdutoViewModel>()
            .ForMember(x => x.Id, mv => mv.MapFrom(x => x.Id))
            .ForMember(x => x.Nome, mv => mv.MapFrom(x => x.Nome))
            .ForMember(x => x.Preco, mv => mv.MapFrom(x => x.Preco))
            .ForMember(x => x.ImagemUrl, mv => mv.MapFrom(x => x.ImagemPrincipalUrl));

        }
    }
}
