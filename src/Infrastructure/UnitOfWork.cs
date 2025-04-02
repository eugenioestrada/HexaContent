using HexaContent.Core;
using HexaContent.Core.Repositories;
using HexaContent.Infrastructure.Database;
using Microsoft.EntityFrameworkCore.Storage;

namespace HexaContent.Infrastructure;

/// <summary>
/// Represents the unit of work for managing database transactions and repositories.
/// </summary>
/// <param name="context">The database context to be used by the unit of work.</param>
/// <param name="articles">The repository for managing articles.</param>
/// <param name="articleMeta">The repository for managing article metadata.</param>
/// <param name="articleSections">The repository for managing article sections.</param>
/// <param name="authors">The repository for managing authors.</param>
/// <param name="authorMeta">The repository for managing author metadata.</param>
/// <param name="sections">The repository for managing sections.</param>
/// <param name="sectionMeta">The repository for managing section metadata.</param>
/// <param name="media">The repository for managing media.</param>
/// <param name="mediaMeta">The repository for managing media metadata.</param>
public class UnitOfWork(DatabaseContext context,
					  IArticlesRepository articles,
					  IArticleMetaRepository articleMeta,
					  IArticleSectionsRepository articleSections,
					  IAuthorsRepository authors,
					  IAuthorMetaRepository authorMeta,
					  ISectionsRepository sections,
					  ISectionMetaRepository sectionMeta,
					  IMediaRepository media,
					  IMediaMetaRepository mediaMeta) : IUnitOfWork
{
	private readonly DatabaseContext _context = context;

	private IDbContextTransaction? _transaction;

	/// <summary>
	/// Gets the articles repository.
	/// </summary>
	public IArticlesRepository Articles { get; } = articles;

	/// <summary>
	/// Gets the article meta repository.
	/// </summary>
	public IArticleMetaRepository ArticleMeta { get; } = articleMeta;

	/// <summary>
	/// Gets the article sections repository.
	/// </summary>
	public IArticleSectionsRepository ArticleSections { get; } = articleSections;

	/// <summary>
	/// Gets the authors repository.
	/// </summary>
	public IAuthorsRepository Authors { get; } = authors;

	/// <summary>
	/// Gets the author meta repository.
	/// </summary>
	public IAuthorMetaRepository AuthorMeta { get; } = authorMeta;

	/// <summary>
	/// Gets the sections repository.
	/// </summary>
	public ISectionsRepository Sections { get; } = sections;

	/// <summary>
	/// Gets the section meta repository.
	/// </summary>
	public ISectionMetaRepository SectionMeta { get; } = sectionMeta;

	/// <summary>
	/// Gets the media repository.
	/// </summary>
	public IMediaRepository Media { get; } = media;

	/// <summary>
	/// Gets the media meta repository.
	/// </summary>
	public IMediaMetaRepository MediaMeta { get; } = mediaMeta;

	/// <summary>
	/// Saves the changes made in the context to the database asynchronously.
	/// </summary>
	/// <returns>The number of state entries written to the database.</returns>
	public async ValueTask<int> SaveChangesAsync()
	{
		return await _context.SaveChangesAsync();
	}

	/// <summary>
	/// Begins a new database transaction.
	/// </summary>
	/// <exception cref="InvalidOperationException">Thrown if a transaction is already in progress.</exception>
	public void BeginTransaction()
	{
		if (_transaction != null)
		{
			throw new InvalidOperationException("A transaction is already in progress.");
		}

		_transaction = _context.Database.BeginTransaction();
	}

	/// <summary>
	/// Commits the current database transaction.
	/// </summary>
	/// <exception cref="InvalidOperationException">Thrown if no transaction is in progress.</exception>
	public void CommitTransaction()
	{
		if (_transaction == null)
		{
			throw new InvalidOperationException("No transaction is in progress.");
		}

		_transaction.Commit();
		_transaction.Dispose();
		_transaction = null;
	}

	/// <summary>
	/// Rolls back the current database transaction.
	/// </summary>
	/// <exception cref="InvalidOperationException">Thrown if no transaction is in progress.</exception>
	public void RollbackTransaction()
	{
		if (_transaction == null)
		{
			throw new InvalidOperationException("No transaction is in progress.");
		}

		_transaction.Rollback();
		_transaction.Dispose();
		_transaction = null;
	}

	/// <summary>
	/// Disposes the unit of work, including the transaction and context.
	/// </summary>
	public void Dispose()
	{
		_transaction?.Dispose();
		_context.Dispose();
	}
}
