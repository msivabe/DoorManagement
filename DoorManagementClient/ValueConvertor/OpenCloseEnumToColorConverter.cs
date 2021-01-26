using GateManagement.Domain;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace DoorManagementClient.ValueConvertor
{
    [ValueConversion(typeof(OpenCloseState), typeof(SolidColorBrush))]
    public class OpenCloseStateEnumToColorConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is OpenCloseState))
                throw new ArgumentException("invalid value");
            OpenCloseState stateValue = (OpenCloseState)value;

            if (stateValue == OpenCloseState.CLOSE)
                return new SolidColorBrush(Colors.Red);
            else
                return new SolidColorBrush(Colors.Green);
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush val = (value as SolidColorBrush);
            if (val.Color == Colors.Red)
                return OpenCloseState.CLOSE;
            else
                return OpenCloseState.OPEN;
         
        }
    }
}
