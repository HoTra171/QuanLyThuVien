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
    /// Interaction logic for TrangChu.xaml
    /// </summary>
    public partial class DieuKhien : Window
    {
        public DieuKhien()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
    }
}
