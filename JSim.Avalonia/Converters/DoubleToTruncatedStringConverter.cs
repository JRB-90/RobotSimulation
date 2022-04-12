using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;
using System.Globalization;

namespace JSim.Avalonia.Converters
{
    public class DoubleToTruncatedStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is double d)
            {
                return $"{d:F3}";
            }
            else
            {
                return 
                    new BindingNotification(
                        new ArgumentException("Argument is not a double"),
                        BindingErrorType.Error
                    );
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string s)
            {
                try
                {
                    double d = System.Convert.ToDouble(s);

                    return d;
                }
                catch
                {
                    return
                        new BindingNotification(
                            new ArgumentException("Failed to parse string to double"),
                            BindingErrorType.Error
                        );
                }
            }
            else
            {
                return
                    new BindingNotification(
                        new ArgumentException("Argument is not a string"),
                        BindingErrorType.Error
                    );
            }
        }
    }
}
