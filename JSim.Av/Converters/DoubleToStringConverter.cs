using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;
using System.Globalization;

namespace JSim.Av.Converters
{
    public class DoubleToStringConverter : IValueConverter
    {
        public object? Convert(
            object? value, 
            Type targetType, 
            object? parameter,
            CultureInfo culture)
        {
            if (value is double val)
            {
                try
                {
                    var res = $"{val:F3}";

                    return res;
                }
                catch
                {
                    return
                        new BindingNotification(
                            new InvalidCastException(),
                            BindingErrorType.DataValidationError
                        );
                }
            }
            else
            {
                return
                    new BindingNotification(
                        new InvalidCastException(),
                        BindingErrorType.DataValidationError
                    );
            }
        }

        public object? ConvertBack(
            object? value, 
            Type targetType, 
            object? parameter, 
            CultureInfo culture)
        {
            if (value is string val)
            {
                try
                {
                    var res = System.Convert.ToDouble(val);

                    return res;
                }
                catch
                {
                    return
                        new BindingNotification(
                            new InvalidCastException(),
                            BindingErrorType.DataValidationError
                        );
                }
            }
            else
            {
                return
                    new BindingNotification(
                        new InvalidCastException(),
                        BindingErrorType.DataValidationError
                    );
            }
        }
    }
}
