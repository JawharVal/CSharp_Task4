using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfApp12
{
    public class TextToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string status)
            {
                return status == "Full" ? Brushes.Red : Brushes.Green;
            }
            return Brushes.Black; // Default color if unexpected status
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException(); // Not used in this context
        }
    }
}