﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace AngkorMoon.Desktop.Utils.Converters
{
    public class NegatableBooleanToVisibilityConverter : IValueConverter
    {
        public NegatableBooleanToVisibilityConverter()
        {
            FalseVisibility = Visibility.Collapsed;
        }

        public object Convert(object value, Type typeTarget, object parameter, CultureInfo culture)
        {
            bool bVal;
            /*
            bool result = bool.TryParse(value.ToString(), out bVal);
            if (!result) return value;
            */
            return !bool.TryParse(value.ToString(), out bVal) ? value 
                : (bVal && Negate) || (!bVal && !Negate) ? FalseVisibility 
                : Visibility.Visible;
            /*
            if (bVal && !Negate) return Visibility.Visible;
            if (bVal && Negate) return FalseVisibility;
            if (!bVal && Negate) return Visibility.Visible;
            if (!bVal && !Negate) return FalseVisibility;
            
            else return Visibility.Visible;   
            */ 
        }

        public object ConvertBack(object value, Type typeTarget, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public bool Negate { get; set; }
        public Visibility FalseVisibility { get; set; }
    }
}
