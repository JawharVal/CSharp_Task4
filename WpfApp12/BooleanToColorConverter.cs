using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfApp12
{
    public class BooleanToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool && (bool)value)
            {
                return Brushes.Red; // Overcrowded
            }
            return Brushes.Green; // Not overcrowded
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("BooleanToColorConverter is a one-way converter.");
        }
    }
}