using HexaContent.Core.Model;

namespace HexaContent.Core.Services;

/// <summary>
/// Provides operations for managing sections in the system.
/// </summary>
public interface ISectionService
{
    /// <summary>
    /// Creates a new section.
    /// </summary>
    /// <param name="section">The section model containing all necessary information.</param>
    /// <returns>A result containing the created section if successful.</returns>
    Task<Result<Section>> CreateSection(Section section);

    /// <summary>
    /// Updates an existing section with the provided model data.
    /// </summary>
    /// <param name="section">The section model containing updated information.</param>
    /// <returns>A result indicating whether the update was successful.</returns>
    Task<Result<bool>> UpdateSection(Section section);

    /// <summary>
    /// Deletes a section by its ID.
    /// </summary>
    /// <param name="sectionId">The ID of the section to delete.</param>
    /// <returns>A result indicating whether the deletion was successful.</returns>
    Task<Result<bool>> DeleteSection(int sectionId);

    /// <summary>
    /// Retrieves a paginated list of sections.
    /// </summary>
    /// <param name="max">The maximum number of sections to retrieve. Default is 50.</param>
    /// <param name="from">The starting index for pagination. Default is 0.</param>
    /// <returns>A result containing the collection of sections if successful.</returns>
    Task<Result<IEnumerable<Section>>> GetAllSections(int max = 50, int from = 0);

    /// <summary>
    /// Retrieves a specific section by its ID.
    /// </summary>
    /// <param name="sectionId">The ID of the section to retrieve.</param>
    /// <returns>A result containing the requested section if successful.</returns>
    Task<Result<Section>> GetSection(int sectionId);
}
