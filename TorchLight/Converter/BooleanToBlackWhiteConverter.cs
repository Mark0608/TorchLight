
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Windows.UI;
using System.Windows.Media;

namespace TorchLight.Converter
{
    public sealed class BooleanToBlackWhiteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            return (value is bool && (bool)value) ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }
    }
}