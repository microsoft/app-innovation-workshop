using System;
using System.Globalization;
using Xamarin.Forms;
using ContosoFieldService.Models;

namespace ContosoFieldService.Converters
{
    /// <summary>
    /// Converts a job status to an according color.
    /// </summary>
    public class JobStatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is JobStatus status)
            {
                switch (status)
                {
                    case JobStatus.Waiting:
                        return (Color)Application.Current.Resources["AccentColor"];
                    case JobStatus.InProgress:
                        return (Color)Application.Current.Resources["AccentColorBlue"];
                    case JobStatus.Complete:
                        return (Color)Application.Current.Resources["AccentColorRed"];
                }
            }

            return Color.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
