using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.ViewModels
{
    public class AccountUserViewModel : INotifyPropertyChanged
    {
        private readonly AccountUserService _accountUserService = new AccountUserService();

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand { get; }

        public AccountUserViewModel()
        {
            LoginCommand = new RelayCommand(() => ExecuteLogin());
        }

        private void ExecuteLogin()
        {
            if (!AllowLogin()) return; // Dừng thực hiện nếu không đủ thông tin
            // Gọi phương thức Login từ AccountUserService
            bool result = _accountUserService.Login(_username, _password);
            if (result)
            {
                MessageBox.Show("Đăng nhập thành công", "Đăng Nhập", MessageBoxButton.OK, MessageBoxImage.Information);
                // Khởi tạo và hiển thị cửa sổ DieuKhien
                var dieuKhien = new DieuKhien();
                dieuKhien.Show(); // Mở cửa sổ DieuKhien

                // Nếu bạn muốn đóng cửa sổ đăng nhập sau khi mở DieuKhien
                Application.Current.MainWindow.Close(); 
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool AllowLogin()
        {
            if (string.IsNullOrWhiteSpace(_username) && string.IsNullOrWhiteSpace(_password))
            {
                MessageBox.Show("Bạn chưa nhập Tài Khoản và Mật Khẩu", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(_username))
            {
                MessageBox.Show("Bạn chưa nhập Tài Khoản", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(_password))
            {
                MessageBox.Show("Bạn chưa nhập Mật Khẩu", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
