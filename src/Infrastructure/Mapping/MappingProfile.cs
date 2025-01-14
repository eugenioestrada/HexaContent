using AutoMapper;
using HexaContent.Core.Model;
using HexaContent.Infrastructure.Database;

namespace HexaContent.Infrastructure.Mapping;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Article, ArticleEntity>().ReverseMap();
		CreateMap<Author, AuthorEntity>().ReverseMap();
	}
}
