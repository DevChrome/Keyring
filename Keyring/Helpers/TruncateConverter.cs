using System.Globalization;

namespace Keyring.Helpers;

public class TruncateConverter : IValueConverter
{
	public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
	{
		string text = value.ToString().Trim();
		int length = System.Convert.ToInt32(parameter);
		if (text.Length < length)
			return text;
		return text[..(length - 3)] + "...";
	}

	public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
