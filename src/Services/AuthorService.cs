using HexaContent.Core;
using HexaContent.Core.Model;
using HexaContent.Core.Repositories;
using HexaContent.Core.Services;

namespace HexaContent.Services;

/// <summary>
/// Service for managing authors in the system.
/// </summary>
public class AuthorService(IAuthorsRepository _authorsRepository) : IAuthorService
{
    public async Task<Result<Author>> CreateAuthor(Author author)
    {
        try
        {
            _authorsRepository.Add(author);
            await _authorsRepository.SaveChangesAsync();
            return Result<Author>.Success(author);
        }
        catch (Exception ex)
        {
            return Result<Author>.Failure(ex.Message);
        }
    }

    public async Task<Result<bool>> UpdateAuthor(Author author)
    {
        try
        {
            author.UpdatedAt = DateTime.UtcNow;
            _authorsRepository.Update(author);
            await _authorsRepository.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(ex.Message);
        }
    }

    public async Task<Result<bool>> DeleteAuthor(int authorId)
    {
        try
        {
            var author = await _authorsRepository.FindAsync(authorId);
            if (author != null)
            {
                _authorsRepository.Delete(author);
                await _authorsRepository.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            return Result<bool>.Failure("Author not found");
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(ex.Message);
        }
    }

    public async Task<Result<IEnumerable<Author>>> GetAllAuthors(int max = 50, int from = 0)
    {
        try
        {
            var authors = await _authorsRepository.Get(max: max, from: from, orderByDesc: a => a.UpdatedAt);
            return Result<IEnumerable<Author>>.Success(authors);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Author>>.Failure(ex.Message);
        }
    }

    public async Task<Result<Author>> GetAuthor(int authorId)
    {
        try
        {
            var author = await _authorsRepository.FindAsync(authorId);
            return Result<Author>.Success(author);
        }
        catch (Exception ex)
        {
            return Result<Author>.Failure(ex.Message);
        }
    }
}
