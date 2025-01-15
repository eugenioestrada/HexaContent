﻿namespace HexaContent.Core.Model;

/// <summary>
/// Represents an author in the system.
/// </summary>
public class Author
{
	/// <summary>
	/// Gets or sets the unique identifier for the author.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the name of the author.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the email of the author.
	/// </summary>
	public string Email { get; set; }

	/// <summary>
	/// Gets or sets the bio of the author.
	/// </summary>
	public string Bio { get; set; }

	/// <summary>
	/// Gets or sets the date and time when the author was created.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the date and time when the author was last updated.
	/// </summary>
	public DateTime UpdatedAt { get; set; }

	/// <summary>
	/// Gets the list of articles written by the author.
	/// </summary>
	public List<Article>? Articles { get; }
}