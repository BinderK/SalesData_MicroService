using AutoMapper;
using SalesData.API.DTOs;
using SalesData.BL.DomainModels;
using System.Runtime.Intrinsics.X86;

namespace SalesData.API.MappingProfiles
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<ArticleNumber, ArticleNumberDto>();
            CreateMap<Sale, SaleDto>();

            CreateMap<SaleDto, Sale>()
                .ForMember(s => s.Id, opts => opts.Ignore())
                .ForMember(s => s.SaleDate, opts => opts.Ignore())
                .ForMember(s => s.ArticleNumber, opts => opts.MapFrom(dto => ArticleNumber.CreateArticleNumber(dto.ArticleNumber.ToString())));
        }
    }
}
