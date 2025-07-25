using System;
using System.Globalization;
using System.Windows.Data;

namespace DocHandler.Helpers
{
    /// <summary>
    /// Converts a string to a boolean value indicating whether it's not empty
    /// </summary>
    public class StringNotEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !string.IsNullOrWhiteSpace(value as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}