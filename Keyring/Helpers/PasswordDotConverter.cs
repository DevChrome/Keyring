using System.Globalization;

namespace Keyring.Helpers;

public class PasswordDotConverter : IValueConverter
{
	public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
	{
		const char Dot = '\u25CF';
		int len = value.ToString().Length;
		return new string(Dot, len);
	}

	public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
