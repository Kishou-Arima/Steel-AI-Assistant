using System.Globalization;

namespace Steel.Converters
{
    public class BubbleColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            bool isUser = value is bool b && b;
            var currentApplication = Application.Current;

            if (currentApplication == null)
            {
                throw new InvalidOperationException("Application.Current is null.");
            }

            return isUser
                ? currentApplication.RequestedTheme == AppTheme.Dark ? Color.FromArgb("#3A72F8") : Color.FromArgb("#D0E2FF")
                : currentApplication.RequestedTheme == AppTheme.Dark ? Color.FromArgb("#2D2D30") : Color.FromArgb("#E5E5E5");
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
