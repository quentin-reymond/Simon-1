using System.Globalization;

namespace Simon.Converters
{
    public class GameStateToVisibilityConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
                return false;
                
            string state = value.ToString() ?? "";
            string targetState = parameter?.ToString() ?? "";
            
            return state == targetState;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}