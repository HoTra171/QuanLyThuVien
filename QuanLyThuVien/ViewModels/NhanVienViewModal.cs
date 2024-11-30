using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyThuVien.ViewModels
{
    internal class NhanVienViewModal : BaseViewModel
    {
        public ObservableCollection<AccountUser> _List;
        public ObservableCollection<AccountUser> List
        {
            get => _List;
            set
            {
                _List = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<AccountUser> _allList;
        public ObservableCollection<AccountUser> FilteredAccountUsers { get; set; } = new ObservableCollection<AccountUser>();

        private AccountUser _SelectedItem;
        public AccountUser SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));

                // Cập nhật các thuộc tính khi chọn một dòng
                if (SelectedItem != null)
                {
                    UserAccount = SelectedItem.UserAccount;
                    Address = SelectedItem.Address;
                    PhoneNumber = SelectedItem.PhoneNumber;
                    Dob = SelectedItem.Dob;
                    UserNameText = SelectedItem.UserNameText;
                    PasswordText = SelectedItem.PasswordText;
                }
            }
        }

        private int _Id;
        public int Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                OnPropertyChanged();
            }
        }

        private string _UserNameText;
        public string UserNameText
        {
            get { return _UserNameText; }
            set
            {
                _UserNameText = value;
                OnPropertyChanged();
            }
        }

        private string? _address;
        public string? Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _dob;
        public DateTime? Dob
        {
            get { return _dob; }
            set
            {
                _dob = value;
                OnPropertyChanged();
            }
        }

        private string _userAccount;
        public string UserAccount
        {
            get { return _userAccount; }
            set
            {
                _userAccount = value;
                OnPropertyChanged();
            }
        }

        private string _PasswordText;
        public string PasswordText
        {
            get { return _PasswordText; }
            set
            {
                _PasswordText = value;
                OnPropertyChanged();
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                FilterAccountUsers();
            }
        }

        // Các lệnh cho chức năng Thêm, Cập nhật, Xóa và Tải sách
        public ICommand AddEmployeeCommand { get; set; }
        public ICommand UpdateEmployeeCommand { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        public NhanVienViewModal()
        {
            // Lưu dữ liệu ban đầu vào danh sách tạm
            _allList = new ObservableCollection<AccountUser>(DataProvider.Ins.DB.AccountUsers.Where(user => user.Role != true)); 
            List = new ObservableCollection<AccountUser>(_allList); // Hiển thị ban đầu là toàn bộ sách

            SearchCommand = new RelayCommand<string>(p => true, p => FilterAccountUsers());

            // Định nghĩa các lệnh
            AddEmployeeCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(UserAccount))
                    return false;

                if (SelectedItem != null)
                    return false;

                // Kiểm tra trùng tên sách
                var isDuplicate = DataProvider.Ins.DB.AccountUsers.Any(x => x.UserAccount == UserAccount);
                if (isDuplicate)
                    return false;

                return true;
            },
            (p) => AddAccountUser());


            UpdateEmployeeCommand = new RelayCommand<object>(p =>
            {
                if (string.IsNullOrEmpty(UserAccount) || SelectedItem == null)
                    return false;

                return true;
            },
            p => UpdateAccountUser());

            // Khởi tạo DeleteCommand
            DeleteEmployeeCommand = new RelayCommand<AccountUser>(p =>
            {
                if (SelectedItem == null)
                    return false;

                return true;
            },
            p => DeleteAccountUser());
        }

        //Phương thức thêm mới sách
        private void AddAccountUser()
        {
            // Kiểm tra nếu tất cả thông tin đã hợp lệ
            if (string.IsNullOrWhiteSpace(UserNameText) || string.IsNullOrWhiteSpace(Address) || string.IsNullOrWhiteSpace(PasswordText) ||
                string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrWhiteSpace(UserAccount) ||
                !Dob.HasValue)
            {
                // Hiển thị thông báo lỗi (có thể dùng MessageBox hoặc Notification)
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin cho Nhân Viên.");
                return;
            }

            // Thực hiện thao tác thêm sách vào cơ sở dữ liệu hoặc danh sách
            AccountUser newEmployy = new AccountUser()
            {
                PasswordText = PasswordText,
                UserAccount = UserAccount,
                PhoneNumber = PhoneNumber,
                UserNameText = UserNameText,
                Address = Address,
                Dob = Dob.Value,
            };

            DataProvider.Ins.DB.AccountUsers.Add(newEmployy);
            DataProvider.Ins.DB.SaveChanges();
            List.Add(newEmployy);
            _allList.Add(newEmployy);
            MessageBox.Show("Thêm Nhân Viên thành công!");

            // Làm sạch các trường nhập liệu sau khi thêm
            ClearFields();
        }

        // Phương thức cập nhật cuốn sách đã chọn
        private void UpdateAccountUser()
        {
            var employy = DataProvider.Ins.DB.AccountUsers.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
            // Kiểm tra nếu tất cả thông tin đã hợp lệ
            if (string.IsNullOrWhiteSpace(UserNameText) || string.IsNullOrWhiteSpace(Address) || string.IsNullOrWhiteSpace(PasswordText) ||
                string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrWhiteSpace(UserAccount) ||
                !Dob.HasValue)
            {
                // Hiển thị thông báo lỗi (có thể dùng MessageBox hoặc Notification)
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin cho Nhân Viên.");
                return;
            }
            employy.PasswordText = PasswordText;
            employy.UserAccount = UserAccount;
            employy.PhoneNumber = PhoneNumber;
            employy.UserNameText = UserNameText;
            employy.Address = Address;
            employy.Dob = Dob.Value;
            DataProvider.Ins.DB.SaveChanges();
            List.Remove(SelectedItem);
            List.Add(employy);
            _allList.Add(employy);
            SelectedItem = employy;
            OnPropertyChanged("List");

            MessageBox.Show("Cập nhật Nhân Viên thành công!");
            ClearFields();
        }

        //Xóa các trường nhập dữ liệu
        private void ClearFields()
        {
            PasswordText = string.Empty;
            UserAccount = string.Empty;
            PhoneNumber = string.Empty;
            UserNameText = string.Empty;
            Address = string.Empty;
            Dob = null;
        }

        //Xóa nhân viên 
        private void DeleteAccountUser()
        {
            var AccountUser = DataProvider.Ins.DB.AccountUsers.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();

            if (AccountUser == null) return;

            // Xóa sách khỏi cơ sở dữ liệu
            DataProvider.Ins.DB.AccountUsers.Remove(AccountUser);
            DataProvider.Ins.DB.SaveChanges();

            // Xóa sách khỏi danh sách hiện tại
            List.Remove(AccountUser);
            _allList.Remove(AccountUser);

            MessageBox.Show("Xóa Nhân Viên thành công!");
            ClearFields();
        }

        // Chức năng tìm kiếm
        private void FilterAccountUsers()
        {
            // Lọc sách theo SearchText hoặc BookId
            var FilteredAccountUsers = _allList.Where(b => string.IsNullOrEmpty(SearchText) ||
                                                      b.UserAccount.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                                      b.Id.ToString().Contains(SearchText)).ToList();


            // Cập nhật danh sách Books
            List = new ObservableCollection<AccountUser>(FilteredAccountUsers);
        }
    }
}
