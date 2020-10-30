using Syncfusion.DataSource.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListViewXamarin
{
    #region SummaryConverter
    public class SummaryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int result = 0;
            var items = value as IEnumerable;
            if(items != null)
            {
                var groupitems = items.ToList<object>().ToList<object>();

                if (groupitems != null)
                {
                    for (int i = 0; i < groupitems.Count; i++)
                    {
                        var contact = groupitems[i] as Contacts;
                        result += contact.Salary;
                    }
                }
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}
