using HexaContent.Core.Repositories;

namespace HexaContent.Core;

public interface IUnitOfWork : IDisposable
{
	IArticlesRepository Articles { get; }
	IArticleMetaRepository ArticleMeta { get; }
	IArticleSectionsRepository ArticleSections { get; }
	IAuthorsRepository Authors { get; }
	IAuthorMetaRepository AuthorMeta { get; }
	ISectionsRepository Sections { get; }
	ISectionMetaRepository SectionMeta { get; }
	IMediaRepository Media { get; }
	IMediaMetaRepository MediaMeta { get; }
	ValueTask<int> SaveChangesAsync();
	void BeginTransaction();
	void CommitTransaction();
	void RollbackTransaction();
}
