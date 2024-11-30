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
        public ObservableCollection<Employee> _List;
        public ObservableCollection<Employee> List
        {
            get => _List;
            set
            {
                _List = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Employee> _allList;
        public ObservableCollection<Employee> FilteredEmployees { get; set; } = new ObservableCollection<Employee>();

        private Employee _SelectedItem;
        public Employee SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));

                // Cập nhật các thuộc tính khi chọn một dòng
                if (SelectedItem != null)
                {
                    FullName = SelectedItem.FullName;
                    Address = SelectedItem.Address;
                    PhoneNumber = SelectedItem.PhoneNumber;
                    Dob = SelectedItem.Dob;
                    UserAccount = SelectedItem.UserAccount;
                }
            }
        }

        private int _employeeId;
        public int EmployeeId
        {
            get { return _employeeId; }
            set
            {
                _employeeId = value;
                OnPropertyChanged();
            }
        }

        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
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

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                FilterEmployees();
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
            _allList = new ObservableCollection<Employee>(DataProvider.Ins.DB.Employees); ;
            List = new ObservableCollection<Employee>(_allList); // Hiển thị ban đầu là toàn bộ sách

            SearchCommand = new RelayCommand<string>(p => true, p => FilterEmployees());

            // Định nghĩa các lệnh
            AddEmployeeCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(FullName))
                    return false;

                if (SelectedItem != null)
                    return false;

                // Kiểm tra trùng tên sách
                var isDuplicate = DataProvider.Ins.DB.Employees.Any(x => x.FullName == FullName);
                if (isDuplicate)
                    return false;

                return true;
            },
            (p) => AddEmployee());


            UpdateEmployeeCommand = new RelayCommand<object>(p =>
            {
                if (string.IsNullOrEmpty(FullName) || SelectedItem == null)
                    return false;

                // Kiểm tra trùng tên sách
                var isDuplicate = DataProvider.Ins.DB.Employees.Any(x => x.FullName == FullName);
                if (isDuplicate)
                    return false;

                return true;
            },
            p => UpdateEmployee());

            // Khởi tạo DeleteCommand
            DeleteEmployeeCommand = new RelayCommand<Book>(p =>
            {
                if (SelectedItem == null)
                    return false;

                return true;
            },
            p => DeleteEmployee());
        }

        //Phương thức thêm mới sách
        private void AddEmployee()
        {
            // Kiểm tra nếu tất cả thông tin đã hợp lệ
            if (string.IsNullOrWhiteSpace(FullName) || string.IsNullOrWhiteSpace(Address) ||
                string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrWhiteSpace(UserAccount) ||
                !Dob.HasValue)
            {
                // Hiển thị thông báo lỗi (có thể dùng MessageBox hoặc Notification)
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin cho Nhân Viên.");
                return;
            }

            // Thực hiện thao tác thêm sách vào cơ sở dữ liệu hoặc danh sách
            Employee newEmployy = new Employee()
            {
                FullName = FullName,
                PhoneNumber = PhoneNumber,
                UserAccount = UserAccount,
                Address = Address,
                Dob = Dob.Value,
            };

            DataProvider.Ins.DB.Employees.Add(newEmployy);
            DataProvider.Ins.DB.SaveChanges();
            List.Add(newEmployy);
            _allList.Add(newEmployy);
            MessageBox.Show("Thêm Nhân Viên thành công!");

            // Làm sạch các trường nhập liệu sau khi thêm
            ClearFields();
        }

        // Phương thức cập nhật cuốn sách đã chọn
        private void UpdateEmployee()
        {
            var employy = DataProvider.Ins.DB.Employees.Where(x => x.EmployeeId == SelectedItem.EmployeeId).SingleOrDefault();
            // Kiểm tra nếu tất cả thông tin đã hợp lệ
            if (string.IsNullOrWhiteSpace(FullName) || string.IsNullOrWhiteSpace(Address) ||
                string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrWhiteSpace(UserAccount) ||
                !Dob.HasValue)
            {
                // Hiển thị thông báo lỗi (có thể dùng MessageBox hoặc Notification)
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin cho Nhân Viên.");
                return;
            }
            employy.FullName = FullName;
            employy.PhoneNumber = PhoneNumber;
            employy.UserAccount = UserAccount;
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
            FullName = string.Empty;
            PhoneNumber = string.Empty;
            UserAccount = string.Empty;
            Address = string.Empty;
            Dob = null;
        }

        //Xóa nhân viên 
        private void DeleteEmployee()
        {
            var employee = DataProvider.Ins.DB.Employees.Where(x => x.EmployeeId == SelectedItem.EmployeeId).SingleOrDefault();

            if (employee == null) return;

            // Xóa sách khỏi cơ sở dữ liệu
            DataProvider.Ins.DB.Employees.Remove(employee);
            DataProvider.Ins.DB.SaveChanges();

            // Xóa sách khỏi danh sách hiện tại
            List.Remove(employee);
            _allList.Remove(employee);
        }

        // Chức năng tìm kiếm
        private void FilterEmployees()
        {
            // Lọc sách theo SearchText hoặc BookId
            var FilteredEmployees = _allList.Where(b => string.IsNullOrEmpty(SearchText) ||
                                                      b.FullName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                                      b.EmployeeId.ToString().Contains(SearchText)).ToList();


            // Cập nhật danh sách Books
            List = new ObservableCollection<Employee>(FilteredEmployees);
        }
    }
}
