using System;
using System.Globalization;
using System.Windows.Data;

namespace GuildWars2.Model.Converter
{
    public class ToUpperCasingColors : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value is string) {
                string text = value as string;
                if(text.Contains("light"))
                    text = text.Insert(5, " ");

                if(text.Contains("deep"))
                    text = text.Insert(4, " ");
                return text.ToString().ToUpper();
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
    }
}