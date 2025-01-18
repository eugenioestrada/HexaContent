namespace HexaContent.Core;

public abstract class MetaEntityBase<TKey, TOBject, TObjectKey> : EntityBase<TKey>
	where TKey : struct
	where TObjectKey : struct
	where TOBject : EntityBase<TObjectKey>
{
	public required int ObjectId { get; set; }
	public required string Key { get; set; }
	public required string Value { get; set; }

	public TOBject? Object { get; set; }
}
