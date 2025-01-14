using AutoMapper;
using HexaContent.Core.Model;
using HexaContent.Infrastructure.Database;

namespace HexaContent.Infrastructure.Mapping;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Article, ArticleEntity>().PreserveReferences().ReverseMap();
		CreateMap<Author, AuthorEntity>().PreserveReferences().ReverseMap();
	}
}
