using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace QuanLyThuVien.ViewModels
{
    class BaoCaoViewModal : BaseViewModel
    {
        public ObservableCollection<ListBorrowed> _ListMonth;
        public ObservableCollection<ListBorrowed> ListMonth
        {
            get => _ListMonth;
            set
            {
                _ListMonth = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ListBorrowed> FilteredListMonth { get; set; } = new ObservableCollection<ListBorrowed>();

        public ObservableCollection<ListBorrowed> _ListDay;
        public ObservableCollection<ListBorrowed> ListDay
        {
            get => _ListDay;
            set
            {
                _ListDay = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _Month;
        public ObservableCollection<string> Month
        {
            get { return _Month; }
            set
            {
                _Month = value;
                OnPropertyChanged(); // Giúp cập nhật UI khi thay đổi
            }
        }
        private string _SelectedYear;
        public string SelectedYear
        {
            get { return _SelectedYear; }
            set
            {
                _SelectedYear = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _selectedDate;
        public DateTime? SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                // Kiểm tra nếu giá trị không null và gán chỉ phần ngày, không có giờ
                if (value.HasValue)
                {
                    _selectedDate = value.Value.Date; // Chỉ lấy ngày, không có giờ
                }
                else
                {
                    _selectedDate = null;
                }
                OnPropertyChanged();
            }
        }


        private string _SelectedMonth;
        public string SelectedMonth
        {
            get { return _SelectedMonth; }
            set
            {
                _SelectedMonth = value;
                OnPropertyChanged();
            }
        }

        public int TotalBorrow
        {
            get {
                return DataProvider.Ins.DB.ListBorrowed.Select(b => b.Id).Count();
            }
        }
        
        public int TotalReader
        {
            get {
                 // Tính số lượng Reader duy nhất
                return DataProvider.Ins.DB.ListBorrowed.Select(b => b.IdReader).Distinct().Count(); 
            }
        }

        public int TotalBook
        {
            get {
                return DataProvider.Ins.DB.ListBorrowed.Select(b => b.IdBook).Distinct().Count();
            }
        }

        // Các lệnh cho chức năng Thêm, Cập nhật, Xóa và Tải sách
        public ICommand StatisticsDayCommand { get; set; }
        public ICommand StatisticsMonthCommand { get; set; }

        public BaoCaoViewModal()
        {
            ListDay = new ObservableCollection<ListBorrowed>(DataProvider.Ins.DB.ListBorrowed); 
            ListMonth = new ObservableCollection<ListBorrowed>(DataProvider.Ins.DB.ListBorrowed);

            Month = new ObservableCollection<string> { "1", "2", "3", "4", "5", "6", "7", "8","9", "10","11","12"};
            // Định nghĩa các lệnh
            //Báo cáo theo ngày
            StatisticsMonthCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedYear == null || SelectedMonth == null)
                    return false;
                return true;
            },
            (p) => StatisticsMonth());

            //Báo cáo theo tháng
            StatisticsDayCommand = new RelayCommand<object>(p =>
            {
                if (SelectedDate == null)
                    return false;
                return true;
            },
            p => StatisticsDay());
                
        }

        private void StatisticsDay()
        {
            // Kiểm tra nếu SelectedDate không null
            if (SelectedDate.HasValue)
            {
                // Lấy ngày, tháng, năm từ SelectedDate
                var selectedDay = SelectedDate.Value.Day;
                var selectedMonth = SelectedDate.Value.Month;
                var selectedYear = SelectedDate.Value.Year;

                // Lọc danh sách mượn sách theo ngày, tháng, và năm
                var filteredList = DataProvider.Ins.DB.ListBorrowed.Where(borrow =>
                    borrow.DateBorrowed.Day == selectedDay &&
                    borrow.DateBorrowed.Month == selectedMonth &&
                    borrow.DateBorrowed.Year == selectedYear).ToList();

                // Cập nhật danh sách Books
                ListDay = new ObservableCollection<ListBorrowed>(filteredList);
            }
            else
            {
                // Xử lý nếu SelectedDate không hợp lệ hoặc chưa chọn ngày
                MessageBox.Show("Vui lòng chọn ngày hợp lệ.");
            }
        }


        private void StatisticsMonth()
        {
            // Kiểm tra nếu SelectedYear và SelectedMonth không rỗng và có thể chuyển đổi thành int
            if (int.TryParse(SelectedMonth, out int month) && int.TryParse(SelectedYear, out int year))
            {
                // Lọc danh sách mượn sách theo tháng và năm
                var filteredList = DataProvider.Ins.DB.ListBorrowed.Where(borrow =>
                    borrow.DateBorrowed.Month == month && borrow.DateBorrowed.Year == year).ToList();

                // Cập nhật danh sách Books
                ListMonth = new ObservableCollection<ListBorrowed>(filteredList);
            }
            else
            {
                // Xử lý nếu tháng hoặc năm không hợp lệ
                // Có thể thông báo lỗi cho người dùng hoặc thực hiện một hành động khác
                MessageBox.Show("Vui lòng chọn tháng và năm hợp lệ.");
            }
        }

    }
}
