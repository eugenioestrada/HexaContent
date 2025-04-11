using HexaContent.Core;
using HexaContent.Core.Model;
using HexaContent.Core.Repositories;
using HexaContent.Core.Services;

namespace HexaContent.Services;

/// <summary>
/// Service for managing sections in the system.
/// </summary>
public class SectionService(ISectionsRepository _sectionsRepository) : ISectionService
{
    public async Task<Result<Section>> CreateSection(Section section)
    {
        try
        {
            _sectionsRepository.Add(section);
            await _sectionsRepository.SaveChangesAsync();
            return Result<Section>.Success(section);
        }
        catch (Exception ex)
        {
            return Result<Section>.Failure(ex.Message);
        }
    }

    public async Task<Result<bool>> UpdateSection(Section section)
    {
        try
        {
            _sectionsRepository.Update(section);
            await _sectionsRepository.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(ex.Message);
        }
    }

    public async Task<Result<bool>> DeleteSection(int sectionId)
    {
        try
        {
            var section = await _sectionsRepository.FindAsync(sectionId);
            if (section != null)
            {
                _sectionsRepository.Remove(section);
                await _sectionsRepository.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            return Result<bool>.Failure("Section not found");
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(ex.Message);
        }
    }

    public async Task<Result<IEnumerable<Section>>> GetAllSections(int max = 50, int from = 0)
    {
        try
        {
            var sections = await _sectionsRepository.Get(max: max, from: from, orderByDesc: s => s.UpdatedAt);
            return Result<IEnumerable<Section>>.Success(sections);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Section>>.Failure(ex.Message);
        }
    }

    public async Task<Result<Section>> GetSection(int sectionId)
    {
        try
        {
            var section = await _sectionsRepository.FindAsync(sectionId);
            return Result<Section>.Success(section);
        }
        catch (Exception ex)
        {
            return Result<Section>.Failure(ex.Message);
        }
    }
}
