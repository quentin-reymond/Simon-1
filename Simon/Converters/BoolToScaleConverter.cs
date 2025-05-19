using System.Globalization;

namespace Simon.Converters
{
    public class BoolToScaleConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
                return 1.0;
                
            bool isActive = (bool)value;
            
            // Retourne 1.1 (10% plus grand) si actif, sinon 1.0 (taille normale)
            return isActive ? 1.1 : 1.0;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}