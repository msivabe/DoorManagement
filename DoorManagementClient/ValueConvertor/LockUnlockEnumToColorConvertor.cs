using GateManagement.Domain;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace DoorManagementClient.ValueConvertor
{
    [ValueConversion(typeof(LockUnlockState), typeof(SolidColorBrush))]
    public class LockUnlockEnumToColorConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is LockUnlockState))
                throw new ArgumentException("invalid value");
            LockUnlockState stateValue = (LockUnlockState)value;

            if (stateValue == LockUnlockState.LOCK)
                return new SolidColorBrush(Colors.Red);
            else
                return new SolidColorBrush(Colors.Green);
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush val = (value as SolidColorBrush);
            if (val.Color == Colors.Red)
                return LockUnlockState.LOCK;
            else
                return LockUnlockState.UNLOCK;
         
        }
    }
}
