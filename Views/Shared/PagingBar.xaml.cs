using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Accounting.Views.Shared
{
    /// <summary>
    /// Interaction logic for PagingBar.xaml
    /// </summary>
    public partial class PagingBar : UserControl,INotifyPropertyChanged
    {
        public int PageNo
        {
            get
            {
                return PageNo;
            }
            set
            {
                PageNo = value;
                txtpageNo.Text = $"{PageNo} of {TotalPages}";
                OnPropertyChanged("PageNo");
            }
        }
        public int SeedNumber
        {
            get
            {
                return SeedNumber;
            }
            set
            {
                SeedNumber = value;
                TotalPages = TotalCount / SeedNumber;
                OnPropertyChanged("SeedNumber");
            }
        }
        public int BindedCount
        {
            get
            {
                return BindedCount;
            }
            set
            {
                if (value <= SeedNumber)
                    this.Visibility = Visibility.Hidden;
                else
                    this.Visibility = Visibility.Visible;
                BindedCount = value;
                OnPropertyChanged("BindedCount");
            }
        }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public PagingBar()
        {
            InitializeComponent();
        }
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
