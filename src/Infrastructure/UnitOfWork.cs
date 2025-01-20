using HexaContent.Core;
using HexaContent.Core.Repositories;
using HexaContent.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace HexaContent.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _context;
    private IDbContextTransaction _transaction;

    public UnitOfWork(DatabaseContext context, 
                      IArticlesRepository articles, 
                      IArticleMetaRepository articleMeta, 
                      IArticleSectionsRepository articleSections, 
                      IAuthorsRepository authors, 
                      IAuthorMetaRepository authorMeta, 
                      ISectionsRepository sections, 
                      ISectionMetaRepository sectionMeta, 
                      IMediaRepository media, 
                      IMediaMetaRepository mediaMeta)
    {
        _context = context;
        Articles = articles;
        ArticleMeta = articleMeta;
        ArticleSections = articleSections;
        Authors = authors;
        AuthorMeta = authorMeta;
        Sections = sections;
        SectionMeta = sectionMeta;
        Media = media;
        MediaMeta = mediaMeta;
    }

    public IArticlesRepository Articles { get; }
    public IArticleMetaRepository ArticleMeta { get; }
    public IArticleSectionsRepository ArticleSections { get; }
    public IAuthorsRepository Authors { get; }
    public IAuthorMetaRepository AuthorMeta { get; }
    public ISectionsRepository Sections { get; }
    public ISectionMetaRepository SectionMeta { get; }
    public IMediaRepository Media { get; }
    public IMediaMetaRepository MediaMeta { get; }

    public async ValueTask<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void BeginTransaction()
    {
        if (_transaction != null)
        {
            throw new InvalidOperationException("A transaction is already in progress.");
        }
        _transaction = _context.Database.BeginTransaction();
    }

    public void CommitTransaction()
    {
        _transaction.Commit();
        _transaction.Dispose();
        _transaction = null;
    }

    public void RollbackTransaction()
    {
        _transaction.Rollback();
        _transaction.Dispose();
        _transaction = null;
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
