using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MovieHacker.Model.Extensions
{
    public static class Extensions
    {
        public static bool IsContains(this string str, string containableString)
        {
            if (str == null || containableString == null) return true;
            if (str.Length == 0 || containableString.Length == 0) return true;
            var str1 = str.ToLower();
            var str2 = containableString.ToLower();
            return str1.Contains(str2);
        }

        public static string FromBoolToString(this bool x)
        {
            return x ? "Да" : "Нет";
        }
        
        public static Visibility BoolToVisibility(this bool x)
        {
            return x ? Visibility.Collapsed : Visibility.Visible;
        }

        public static bool IsAlreadyExistInEnumerable(this IEnumerable<string> values, string element)
        {
            foreach(var e in values)
            {
                if (e.IsContains(element)) return true;
            }
            return false;
        }
    }
}
