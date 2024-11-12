using System;
using System.Collections.Generic;
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

namespace QuanLyThuVien
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : UserControl
    {
        // Định nghĩa Dependency Property
        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register("PlaceholderText", typeof(string), typeof(Search), new PropertyMetadata(string.Empty));

        // Property cho PlaceholderText
        public string PlaceholderText
        {
            get { return (string)GetValue(PlaceholderTextProperty); }
            set { SetValue(PlaceholderTextProperty, value); }
        }

        public Search()
        {
            InitializeComponent();
        }

        private void tbNhapTen_GotFocus(object sender, RoutedEventArgs e)
        {
            // Ẩn TextBlock khi TextBox có focus
            tblNhapTen.Visibility = Visibility.Collapsed;
        }

        private void tbNhapTen_LostFocus(object sender, RoutedEventArgs e)
        {
            // Hiện TextBlock nếu TextBox trống sau khi mất focus
            if (string.IsNullOrWhiteSpace(tbNhapTen.Text))
            {
                tblNhapTen.Visibility = Visibility.Visible;
            }
        }

        private void tblNhapTen_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tbNhapTen.Focus();
        }

    }
}
