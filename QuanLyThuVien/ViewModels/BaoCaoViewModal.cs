using LiveCharts;
using LiveCharts.Definitions.Charts;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private ObservableCollection<ListBorrowed> _allList;

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
        
        private string _SelectedYearChart;
        public string SelectedYearChart
        {
            get { return _SelectedYearChart; }
            set
            {
                _SelectedYearChart = value;
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

        // số lượt mượn sách
        public int TotalBorrow
        {
            get {
                return DataProvider.Ins.DB.ListBorrowed.Select(b => b.Id).Count();
            }
        }
        
        // số đầu độc giả mượn sách
        public int TotalReader
        {
            get {
                 // Tính số lượng Reader duy nhất
                return DataProvider.Ins.DB.ListBorrowed.Select(b => b.IdReader).Distinct().Count(); 
            }
        }

        // tính tổng số đầu sách đã mượn
        public int TotalBook
        {
            get {
                return DataProvider.Ins.DB.ListBorrowed.Select(b => b.IdBook).Distinct().Count();
            }
        }

        // làm biểu đồ thống kê
        private SeriesCollection _SeriesCollection;
        public SeriesCollection SeriesCollection
        {
            get => _SeriesCollection;
            set
            {
                _SeriesCollection = value;
                OnPropertyChanged(nameof(SeriesCollection));
            }
        }


        private ObservableCollection<string> _Labels;
        public ObservableCollection<string> Labels
        {
            get => _Labels;
            set
            {
                _Labels = value;
                OnPropertyChanged(nameof(Labels));
            }
        }


        public Func<double, string> Formatter { get; set; }


        // Các lệnh cho chức năng Thêm, Cập nhật, Xóa và Tải sách
        public ICommand StatisticsDayCommand { get; set; }
        public ICommand StatisticsMonthCommand { get; set; }
        public ICommand StatisticsYearCommand { get; set; }
        public ICommand StatisticsCommand { get; set; }

        public BaoCaoViewModal()
        {
            _allList = new ObservableCollection<ListBorrowed>(DataProvider.Ins.DB.ListBorrowed);
            ListMonth = new ObservableCollection<ListBorrowed>(_allList);
            ListDay = new ObservableCollection<ListBorrowed>(_allList);

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

            //Báo cáo theo năm trong phần biểu đồ
            StatisticsYearCommand = new RelayCommand<object>(p =>
            {
                return true;
            },
            p => LoadChartData());

            //Báo cáo theo tháng
            StatisticsCommand = new RelayCommand<object>(p =>
            {
                return true;
            },
            p => {
                ListDay = _allList;
                ListMonth = _allList;
            });
                
        }

        // hàm biểu đồ
        private void LoadChartData()
        {
            // Kiểm tra nếu SelectedYearChart không rỗng và có thể chuyển đổi thành số nguyên
            if (int.TryParse(SelectedYearChart, out int year))
            {
                // Lấy danh sách số sách được mượn theo từng tháng trong năm đã chọn
                var monthlyBorrowData = DataProvider.Ins.DB.ListBorrowed
                    .Where(borrow => borrow.DateBorrowed.Year == year)
                    .GroupBy(borrow => borrow.DateBorrowed.Month) // Nhóm theo tháng
                    .Select(group => new
                    {
                        Month = group.Key,
                        BorrowCount = group.Count(), // Đếm số lượt mượn trong từng tháng
                        UniqueReaderCount = group.Select(borrow => borrow.IdReader).Distinct().Count(), // Đếm số lượng độc giả duy nhất
                        UniqueBookCount = group.Select(borrow => borrow.IdBook).Distinct().Count() // Đếm số ID sách duy nhất
                    })
                    .OrderBy(result => result.Month) // Sắp xếp theo tháng
                    .ToList();

                // Khởi tạo danh sách nhãn (Labels) cho biểu đồ
                Labels = new ObservableCollection<string>();
                ChartValues<int> borrowCounts = new ChartValues<int>();
                ChartValues<int> readerCounts = new ChartValues<int>();
                ChartValues<int> bookCounts = new ChartValues<int>();

                // Duyệt qua kết quả để gán dữ liệu cho biểu đồ
                for (int month = 1; month <= 12; month++)
                {
                    // Tìm dữ liệu của tháng hiện tại
                    Labels.Add($"Tháng {month}");
                    var data = monthlyBorrowData.FirstOrDefault(x => x.Month == month);

                    // Nếu có dữ liệu cho tháng đó, thêm vào các danh sách, nếu không thì thêm 0
                    borrowCounts.Add(data != null ? data.BorrowCount : 0);
                    readerCounts.Add(data != null ? data.UniqueReaderCount : 0);
                    bookCounts.Add(data != null ? data.UniqueBookCount : 0);
                }

                // Gán dữ liệu vào SeriesCollection
                SeriesCollection = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Số lượt mượn",
                        Values = borrowCounts,
                    },
                    new ColumnSeries
                    {
                        Title = "Số lượng độc giả",
                        Values = readerCounts,
                    },
                    new ColumnSeries
                    {
                        Title = "Số đầu sách",
                        Values = bookCounts
                    },
                };


                // Định dạng trục y (nếu cần)
                Formatter = value => value.ToString("N0");
            }
            else
            {
                // Nếu SelectedYearChart không hợp lệ, hiển thị thông báo lỗi
                MessageBox.Show("Vui lòng chọn một năm hợp lệ.");
            }
        }




        private void StatisticsDay()
        {
            // Kiểm tra nếu SelectedDate không null
            if (SelectedDate.Value.Day > 0 && SelectedDate.Value.Day <= 31 && SelectedDate.Value.Month > 0 && SelectedDate.Value.Month <= 12)
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
