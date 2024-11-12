using System.Linq;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services
{
    internal class AccountUserService
    {
        public bool Login(string username, string password)
        {
            using (var context = new LibraryContext())
            {
                // Kiểm tra xem có người dùng nào có username và password khớp không
                var user = context.AccountUsers
                    .FirstOrDefault(u => u.UserNameText == username && u.PasswordText == password);

                return user != null; // Trả về true nếu tìm thấy người dùng
            }
        }
    }
}
