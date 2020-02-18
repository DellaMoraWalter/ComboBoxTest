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
            typeof(MainPage),
            Values.value1,
            propertyChanged: selectedEnumValueChanged);

        public Values selectedEnumValue
        {
            get => (Values)GetValue(selectedEnumValueProperty);
            set => SetValue(selectedEnumValueProperty, value);
        }
        static void selectedEnumValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var self = bindable as MainPage;
            }
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



    public class EnumItem<T>
    {
        public T value { get; set; }
        public string dsc { get; set; }

        public EnumItem(T value, string dsc)
        {
            this.value = value;
            this.dsc = dsc;
        }
    }



    public class EnumItemsCollection<T> : Xamarin.Forms.IValueConverter
    {
        private IList<EnumItem<T>> _values = null;
        public IList<EnumItem<T>> values
        {
            get
            {
                if (_values == null)
                    _values = GetValues();

                return _values;
            }
        }

        protected virtual IList<EnumItem<T>> GetValues()
        {
            throw new NotImplementedException();
        }

        public EnumItem<T> GetEnumItemFromValue(T value)
        {
            return values.FirstOrDefault(p => EqualityComparer<T>.Default.Equals(p.value, value));
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                if (parameter == null)
                    return GetEnumItemFromValue((T)value);
                else if ((string)parameter == "description")
                    return GetEnumItemFromValue((T)value).dsc;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return ((EnumItem<T>)value).value;
            }

            return null;
        }
    }



    public class ValuesItems : EnumItemsCollection<Values>
    {
        protected override IList<EnumItem<Values>> GetValues()
        {
            return new List<EnumItem<Values>>()
            {
                new EnumItem<Values>(Values.value1, AppResources.Value1Dsc),
                new EnumItem<Values>(Values.value2, AppResources.Value2Dsc),
                new EnumItem<Values>(Values.value3, AppResources.Value3Dsc)
            };
        }
    }
}
