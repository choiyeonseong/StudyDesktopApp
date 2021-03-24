using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfTestApp
{
    public class NameConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string name;
            switch ((string)parameter)
            {
                case "FormatLastFirst":
                    name = $"{values[1]}, {values[0]}"; // 뒤집어서 포맷팅
                    break;
                default:
                    name = $"{values[0]} {values[1]}";  // 기본
                    break;
            }
            return name;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {   // 안씀 
            throw new NotImplementedException();
        }
    }
}
