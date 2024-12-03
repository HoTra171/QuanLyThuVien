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
using System.Windows.Shapes;

namespace QuanLyThuVien
{
    /// <summary>
    /// Interaction logic for DocGia.xaml
    /// </summary>
    public partial class DocGia : Window
    {
        public DocGia()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BaoCao muon = new BaoCao();
            muon.Show();

            Window hi = MuonSach.GetWindow(this);
            if (hi != null)
            {
                // Đóng cửa sổ cha
                hi.Close();
            }
        }

        private void ButtonDetail_Click(object sender, RoutedEventArgs e)
        {
            MuonSach muon = new MuonSach();
            muon.Show();

            Window hi = MuonSach.GetWindow(this);
            if (hi != null)
            {
                // Đóng cửa sổ cha
                hi.Close();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
