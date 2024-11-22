using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLyThuVien.Models;
using QuanLyThuVien.ViewModels;

namespace QuanLyThuVien.ViewModels
{
    public class DangNhapViewModel : BaseViewModel
    {
        public ICommand LoadDangNhapCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        private string _UserName;
        private string _Password;

        public string Password
        {
            get { return _Password; }
            set { _Password = value; OnPropertyChanged(); } 
        }

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; OnPropertyChanged(); } 
        }

        public DangNhapViewModel()
        {
            LoadDangNhapCommand = new RelayCommand<Button>(p => { return p != null; }, p =>
            {
                // Khởi tạo cửa sổ DieuKhien
                DieuKhien mainWindow = new DieuKhien();

                // Lấy cửa sổ hiện tại
                FrameworkElement window = Window.GetWindow(p);
                var loginWindow = window as Window;

                // Kiểm tra tên đăng nhập và mật khẩu
                if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
                {
                    MessageBox.Show("Bạn chưa nhập UserName hoặc Password", "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    string passEncode = Password;

                    // Kiểm tra tài khoản trong cơ sở dữ liệu
                    int count = DataProvider.Ins.DB.AccountUsers.Where(x => x.UserNameText == UserName && x.PasswordText == passEncode).Count();
                    if (count > 0)
                    {
                        loginWindow.Hide(); // Ẩn cửa sổ đăng nhập
                        mainWindow.ShowDialog(); // Hiển thị cửa sổ chính
                        loginWindow.Close(); // Đóng cửa sổ đăng nhập
                    }
                    else
                    {
                        // Hiển thị thông báo nếu không tìm thấy tài khoản
                        MessageBox.Show("UserName hoặc Password không đúng", "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            });

            // Cập nhật mật khẩu từ PasswordBox
            PasswordChangedCommand = new RelayCommand<PasswordBox>(p => p != null, p => { Password = p.Password; });
        }
    }
}
