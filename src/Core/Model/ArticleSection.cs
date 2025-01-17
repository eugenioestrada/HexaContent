﻿namespace HexaContent.Core.Model
{
	public class ArticleSection : EntityBase<long>
	{
		public int ArticleId { get; set; }
		public int SectionId { get; set; }

		public Article? Article { get; set; }
		public Section? Section { get; set; }
	}
}