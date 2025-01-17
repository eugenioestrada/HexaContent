using System.ComponentModel.DataAnnotations;

namespace HexaContent.Core
{
	public class EntityBase
	{
		/// <summary>
		/// Gets or sets the unique identifier for the article.
		/// </summary>
		[Key]
		public int Id { get; set; }
	}
}