using System.Windows;
using System.Windows.Controls;
using QuanLyThuVien.Services;
using QuanLyThuVien.ViewModels;

namespace QuanLyThuVien
{
    public partial class DangNhap : Window
    {
        private readonly AccountUserViewModel _viewModel; // Định nghĩa biến ViewModel

        public DangNhap()
        {
            InitializeComponent();
            _viewModel = new AccountUserViewModel(); // Khởi tạo ViewModel
            DataContext = _viewModel; // Đặt DataContext cho cửa sổ
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.LoginCommand.Execute(null); // Thực thi lệnh đăng nhập
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            _viewModel.Password = passwordBox.Password; // Gán giá trị mật khẩu vào ViewModel
        }
    }
}
