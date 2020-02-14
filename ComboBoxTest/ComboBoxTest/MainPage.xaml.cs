using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ComboBoxTest
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        #region selectedEnumValue
        public static readonly BindableProperty selectedEnumValueProperty = BindableProperty.Create(
            nameof(selectedEnumValue),
            typeof(Values),
            typeof(MainPage));

        public Values selectedEnumValue
        {
            get => (Values)GetValue(selectedEnumValueProperty);
            set => SetValue(selectedEnumValueProperty, value);
        }
        #endregion



        public MainPage()
        {
            InitializeComponent();
        }
    }



    public enum Values
    {
        value1 = 1,
        value2 = 2,
        value3 = 3
    }



    public class ValuesToDsc_Conv : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                switch ((Values)value)
                {
                    case Values.value1: return AppResources.Value1Dsc;
                    case Values.value2: return AppResources.Value2Dsc;
                    case Values.value3: return AppResources.Value3Dsc;
                    default: return null; //throw new ArgumentOutOfRangeException();
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }



    public class EnumToList_Conv : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter == null)
                return null;
            else
            {
                var array = Enum.GetValues(parameter as Type);

                List<object> list = new List<object>();
                foreach (object obj in array)
                    list.Add(obj);

                return list;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
