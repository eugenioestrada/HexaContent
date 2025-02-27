namespace HexaContent.Core.Utils;

public static class Argument
{
	public static void NotNull(object? value, string name)
	{
		if (value == null)
		{
			throw new ArgumentNullException(name);
		}
	}
	public static void Ensures(bool condition, string message)
	{
		if (!condition)
		{
			throw new ArgumentNullException(message);
		}
	}

	public static void EnsuresNot(bool condition, string message)
	{
		Ensures(!condition, message);
	}
}
