using System.ComponentModel.DataAnnotations.Schema;

namespace HexaContent.Core.Model;

/// <summary>
/// Represents a media item in the system.
/// </summary>
[Table("media")]
public sealed class Media : EntityBase<long>
{
    /// <summary>
    /// Gets or sets the source URL of the media.
    /// </summary>
    public string SourceUrl { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date and time when the media was published.
    /// </summary>
    public DateTime? PublishedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the media was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the description of the media.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the alternative text for the media.
    /// </summary>
    public string AlternativeText { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the caption for the media.
    /// </summary>
    public string Caption { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of the media.
    /// </summary>
    public MediaType Type { get; set; }
}
