using System;
using System.Globalization;
using Xamarin.Forms;

namespace ContosoFieldService.Converters
{
    /// <summary>
    /// Converts a boolean to its negated value.
    /// </summary>
    public class NegateBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool status ? !status : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}