using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.ViewModels
{
    public class DoiMatKhauViewModel : BaseViewModel
    {
        private string _oldPassword;
        private string _newPassword;
        private string _confirmPassword;


        public string OldPassword
        {
            get { return _oldPassword; }
            set { _oldPassword = value; OnPropertyChanged(); }
        }

        public string NewPassword
        {
            get { return _newPassword; }
            set { _newPassword = value; OnPropertyChanged(); }
        }

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { _confirmPassword = value; OnPropertyChanged(); }
        }

        public ICommand ChangePasswordCommand { get; set; }
        public ICommand OldPasswordChangedCommand { get; set; }
        public ICommand NewPasswordChangedCommand { get; set; }
        public ICommand ConfirmPasswordChangedCommand { get; set; }

        public DoiMatKhauViewModel()
        {

            // Khởi tạo Command
            ChangePasswordCommand = new RelayCommand<object>((p) => true, (p) => ChangePassword());
            OldPasswordChangedCommand = new RelayCommand<PasswordBox>((p) => p != null, (p) => OldPassword = p.Password);
            NewPasswordChangedCommand = new RelayCommand<PasswordBox>((p) => p != null, (p) => NewPassword = p.Password);
            ConfirmPasswordChangedCommand = new RelayCommand<PasswordBox>((p) => p != null, (p) => ConfirmPassword = p.Password);
        }

        private void ChangePassword()
        {
            // Kiểm tra mật khẩu mới và xác nhận mật khẩu
            if (string.IsNullOrEmpty(OldPassword))
            {
                MessageBox.Show("Mật khẩu cũ không được để trống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(NewPassword))
            {
                MessageBox.Show("Mật khẩu mới không được để trống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(ConfirmPassword))
            {
                MessageBox.Show("vui lòng nhập lại mật khẩu mới !", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (NewPassword != ConfirmPassword)
            {
                MessageBox.Show("Mật khẩu mới và xác nhận mật khẩu không khớp!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Lấy thông tin người dùng từ cơ sở dữ liệu
            var currentUser = DataProvider.Ins.DB.AccountUsers.FirstOrDefault(u => u.Id == DangNhapViewModel.Id);

            if (currentUser == null)
            {
                MessageBox.Show("Người dùng không tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (currentUser.PasswordText != OldPassword)
            {
                MessageBox.Show("Mật khẩu cũ không chính xác!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Cập nhật mật khẩu
            currentUser.PasswordText = NewPassword;
            DataProvider.Ins.DB.SaveChanges();

            MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            // Reset trường mật khẩu
            OldPassword = string.Empty;
            NewPassword = string.Empty;
            ConfirmPassword = string.Empty;


            DieuKhien mainWindow = new DieuKhien();

            // Đóng màn hình đổi mật khẩu
            Application.Current.Windows
               .OfType<Window>()
               .FirstOrDefault(w => w.IsActive)?.Close();
            mainWindow.Show();


        }
    }
}