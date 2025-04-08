using System.Text.RegularExpressions;

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

	public static void ThrowInvalidOperationIfStringEmpty(this string? value, string message)
	{
		if (string.IsNullOrEmpty(value))
		{
			throw new InvalidOperationException(message);
		}
	}

	public static void ThrowInvalidOperationIfNotSuccess(this Match match, string message)
	{
		if (!match.Success)
		{
			throw new InvalidOperationException(message);
		}
	}
}
