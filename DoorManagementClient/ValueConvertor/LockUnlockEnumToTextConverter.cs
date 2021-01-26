using GateManagement.Domain;
using System;
using System.Globalization;
using System.Windows.Data;

namespace DoorManagementClient.ValueConvertor
{
    [ValueConversion(typeof(LockUnlockState), typeof(string))]
    public class LockUnlockEnumToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is LockUnlockState))
                throw new ArgumentException("invalid value");
            LockUnlockState stateValue = (LockUnlockState)value;

            return Enum.GetName(typeof(LockUnlockState), stateValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string val = (value as string);
            return Enum.Parse(typeof(LockUnlockState), val);
        }
    }
}
