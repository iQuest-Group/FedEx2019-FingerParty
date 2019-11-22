#region Using Directives

using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

#endregion

namespace FingerPartyApp
{
	public sealed class KeyToStringConverter : IValueConverter
	{
		#region Public Methods

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return TypeDescriptor.GetConverter(value).ConvertToString(value);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		#endregion
	}
}