using System.ComponentModel.DataAnnotations;

namespace HexaContent.Core
{
	public class EntityBase<TKey> where TKey : struct
	{
		/// <summary>
		/// Gets or sets the unique identifier for the article.
		/// </summary>
		[Key]
		public TKey Id { get; set; }
	}
}