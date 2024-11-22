using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLyThuVien.ViewModels;

namespace QuanLyThuVien
{
    public partial class DangNhap : Window
    {
        private readonly DangNhapViewModel _viewModel; // Định nghĩa biến ViewModel

        public DangNhap()
        {
            InitializeComponent();
        }
    }
}


