using AutoMapper;
using HexaContent.Infrastructure.Database;

namespace HexaContent.Infrastructure.Repositories;

public abstract class RepositoryBase
{
	protected readonly DatabaseContext _databaseContext;
	protected readonly IMapper _mapper;

	/// <summary>
	/// Initializes a new instance of the <see cref="ArticlesRepository"/> class.
	/// </summary>
	/// <param name="databaseContext">The database context.</param>
	/// <param name="mapper">The AutoMapper instance.</param>
	public RepositoryBase(DatabaseContext databaseContext, IMapper mapper)
	{
		this._databaseContext = databaseContext;
		this._mapper = mapper;
	}
}