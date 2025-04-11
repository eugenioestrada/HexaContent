using HexaContent.Core.Model;

namespace HexaContent.Core.Services;

/// <summary>
/// Provides operations for managing authors in the system.
/// </summary>
public interface IAuthorService
{
    /// <summary>
    /// Creates a new author.
    /// </summary>
    /// <param name="author">The author model containing all necessary information.</param>
    /// <returns>A result containing the created author if successful.</returns>
    Task<Result<Author>> CreateAuthor(Author author);

    /// <summary>
    /// Updates an existing author with the provided model data.
    /// </summary>
    /// <param name="author">The author model containing updated information.</param>
    /// <returns>A result indicating whether the update was successful.</returns>
    Task<Result<bool>> UpdateAuthor(Author author);

    /// <summary>
    /// Deletes an author by its ID.
    /// </summary>
    /// <param name="authorId">The ID of the author to delete.</param>
    /// <returns>A result indicating whether the deletion was successful.</returns>
    Task<Result<bool>> DeleteAuthor(int authorId);

    /// <summary>
    /// Retrieves a paginated list of authors.
    /// </summary>
    /// <param name="max">The maximum number of authors to retrieve. Default is 50.</param>
    /// <param name="from">The starting index for pagination. Default is 0.</param>
    /// <returns>A result containing the collection of authors if successful.</returns>
    Task<Result<IEnumerable<Author>>> GetAllAuthors(int max = 50, int from = 0);

    /// <summary>
    /// Retrieves a specific author by its ID.
    /// </summary>
    /// <param name="authorId">The ID of the author to retrieve.</param>
    /// <returns>A result containing the requested author if successful.</returns>
    Task<Result<Author>> GetAuthor(int authorId);
}
