using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TorchLight.Converter
{
    /// <summary>
    /// Value converter that translates true to <see cref="Visibility.Visible"/> and false to
    /// <see cref="Visibility.Collapsed"/>.
    /// </summary>
    public sealed class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            var converterParameter = parameter as string;
            if (converterParameter != null)
            {
                return ConvertWithParameter(value, converterParameter);
            }

            return (value is bool && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }

        private object ConvertWithParameter(object value, string isNotInverted)
        {
            if (bool.Parse(isNotInverted))
            {
                return (value is bool && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
            }

            return (value is bool && (bool)value) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            string converterParameter = parameter as string;
            if (converterParameter != null)
            {
                return ConvertBackWithParameter(value, converterParameter);
            }
            return value is Visibility && (Visibility)value == Visibility.Visible;
        }

        private object ConvertBackWithParameter(object value, string isNotInverted)
        {
            if (bool.Parse(isNotInverted))
            {
                return (value is Visibility) && (Visibility)value == Visibility.Visible;
            }

            return (value is Visibility) && (Visibility)value == Visibility.Collapsed;
        }
    }
}
