using System.Text.RegularExpressions;

namespace Keyring.Helpers;

public static class EmailValidator
{
	private static readonly Regex regex = new("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+.com$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

	public static bool IsValid (string s) => regex.IsMatch(s);
}
