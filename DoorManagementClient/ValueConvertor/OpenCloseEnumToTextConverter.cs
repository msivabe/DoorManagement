using GateManagement.Domain;
using System;
using System.Globalization;
using System.Windows.Data;

namespace DoorManagementClient.ValueConvertor
{
    [ValueConversion(typeof(OpenCloseState), typeof(string))]
    public class OpenCloseEnumToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is OpenCloseState))
                throw new ArgumentException("invalid value");
            OpenCloseState stateValue = (OpenCloseState)value;

            return Enum.GetName(typeof(OpenCloseState), stateValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string val = (value as string);
            return Enum.Parse(typeof(OpenCloseState), val);
        }
    }
}
