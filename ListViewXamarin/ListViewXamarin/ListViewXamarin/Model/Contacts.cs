using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListViewXamarin
{
    public class Contacts : INotifyPropertyChanged
    {
        #region Fields
        private string contactName;
        private string contactType;
        private int salary;
        private ImageSource image;
        #endregion

        #region Constructor
        public Contacts(string name, int number)
        {
            contactName = name;
            salary = number;
        }
        #endregion

        #region Properties
        public string ContactName
        {
            get { return contactName; }
            set
            {
                if (contactName != value)
                {
                    contactName = value;
                    this.RaisedOnPropertyChanged("ContactName");
                }
            }
        }

        public string ContactType
        {
            get { return contactType; }
            set
            {
                if (contactType != value)
                {
                    contactType = value;
                    this.RaisedOnPropertyChanged("ContactType");
                }
            }
        }

        public int Salary
        {
            get { return salary; }
            set
            {
                if (salary != value)
                {
                    salary = value;
                    this.RaisedOnPropertyChanged("Salary");
                }
            }
        }

        public ImageSource ContactImage
        {
            get { return this.image; }
            set
            {
                this.image = value;
                this.RaisedOnPropertyChanged("ContactImage");
            }
        }
        #endregion

        #region Interface implementation
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisedOnPropertyChanged(string _PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(_PropertyName));
            }
        }
        #endregion
    }
}
