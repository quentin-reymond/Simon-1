using System.Globalization;

namespace Simon.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
                return Colors.Gray;
                
            bool isActive = (bool)value;
            string? colorName = parameter as string;
            
            if (isActive)
            {
                // Couleurs vives quand actives
                switch (colorName)
                {
                    case "Red":
                        return Color.FromRgb(255, 69, 58); // Rouge vif
                    case "Green":
                        return Color.FromRgb(48, 209, 88); // Vert vif
                    case "Blue":
                        return Color.FromRgb(10, 132, 255); // Bleu vif
                    case "Yellow":
                        return Color.FromRgb(255, 214, 10); // Jaune vif
                    default:
                        return Colors.Gray;
                }
            }
            else
            {
                // Couleurs atténuées quand inactives
                switch (colorName)
                {
                    case "Red":
                        return Color.FromRgb(128, 34, 29); // Rouge foncé
                    case "Green":
                        return Color.FromRgb(24, 104, 44); // Vert foncé
                    case "Blue":
                        return Color.FromRgb(5, 66, 127); // Bleu foncé
                    case "Yellow":
                        return Color.FromRgb(127, 107, 5); // Jaune foncé
                    default:
                        return Colors.DarkGray;
                }
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}