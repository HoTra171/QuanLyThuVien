
﻿using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Identity.Client;
using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static QuanLyThuVien.ViewModels.MuonSachViewModal;

namespace QuanLyThuVien.ViewModels
{
    internal class DocGiaViewModal : BaseViewModel
    {
        public ObservableCollection<Reader> _List;
        public ObservableCollection<Reader> List
        {
            get => _List;
            set
            {
                _List = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<Reader> _allList;
        public ObservableCollection<Reader> FilteredReaders { get; set; } = new ObservableCollection<Reader>();

        public ObservableCollection<Reader> _Reader;
        public ObservableCollection<Reader> Reader
        {
            get => _Reader;
            set
            {
                _Reader = value;
                OnPropertyChanged();
            }
        }

        private Reader _SelectedItem;
        public Reader SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));

                // Cập nhật các thuộc tính khi chọn một dòng
                if (SelectedItem != null)
                {
                    Id = SelectedItem.Id;
                    UserNameText = SelectedItem.UserNameText;
                    Address = SelectedItem.Address;
                    Email = SelectedItem.Email;
                    ReaderType = SelectedItem.ReaderType;
                    Dob = SelectedItem.Dob;
                    DateCreated = SelectedItem.DateCreated;
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

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set
            {
                _Email = value;
                OnPropertyChanged();
            }
        }

        private string _ReaderType;
        public string ReaderType
        {
            get { return _ReaderType; }
            set
            {
                _ReaderType = value;
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

        private DateTime? _DateCreated;
        public DateTime? DateCreated
        {
            get { return _DateCreated; }
            set
            {
                _DateCreated = value;
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
                FilterReaders();
            }
        }

        //chưa làm 
        private decimal? _debt;
        public decimal? debt
        {
            get => _debt ;
            set
            {
                _debt = value;
                OnPropertyChanged();
            }
        }

        public class ReaderIdMessage
        {
            public int ReaderId { get; set; }

            public ReaderIdMessage(int readerId)
            {
                ReaderId = readerId;
            }
        }


        // Các lệnh cho chức năng Thêm, Cập nhật, Xóa và Tải sách
        public ICommand AddReaderCommand { get; set; }
        public ICommand UpdateReaderCommand { get; set; }
        public ICommand DeleteReaderCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand ShowReaderCommand { get; set; }
        public ICommand distroyCommand { get; set; }



        public DocGiaViewModal()
        {
            // Tính tổng số tiền nợ 
            sumDebtReader();

            // Lưu dữ liệu ban đầu vào danh sách tạm
            _allList = new ObservableCollection<Reader>(DataProvider.Ins.DB.Readers);
            List = new ObservableCollection<Reader>(_allList); // Hiển thị ban đầu là toàn bộ sách


            SearchCommand = new RelayCommand<string>(p => true, p => FilterReaders());

            // Định nghĩa các lệnh
            AddReaderCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(UserNameText))
                    return false;

                if (SelectedItem != null)
                    return false;

                return true;
            },
            (p) => AddReader());


            UpdateReaderCommand = new RelayCommand<object>(p =>
            {
                if (string.IsNullOrEmpty(UserNameText) || SelectedItem == null)
                    return false;

                if (SelectedItem != null)
                    return true;

                return true;
            },
            p => UpdateReader());

            // Khởi tạo DeleteCommand
            DeleteReaderCommand = new RelayCommand<Reader>(p =>
            {
                if (SelectedItem == null)
                    return false;

                return true;
            },
            p => DeleteReader());

            //xem chi tiết độc giả
            ShowReaderCommand = new RelayCommand<Reader>(p =>
            {
                if (SelectedItem != null)
                    return true;
                if (SelectedItem == null)
                    return false;
                return true;
            },
            p => ShowReader());


            distroyCommand = new RelayCommand<Reader>(p =>
            {
                return true;
            },
            p => ClearFields());
        }

        // tính tổng số nợ của các độc giả
        private void sumDebtReader()
        {
            // Lấy tổng nợ của các độc giả và lưu kết quả vào bộ nhớ
            var totalDebts = (from borrow in DataProvider.Ins.DB.ListBorrowed
                              group borrow by borrow.IdReader into g
                              select new
                              {
                                  idReader = g.Key,
                                  TotalDebt = g.Sum(b => b.debtBook)
                              }).ToList();  // Đảm bảo truy vấn được thực thi và kết quả được lưu vào bộ nhớ

            // Cập nhật các độc giả với tổng nợ
            foreach (var debt in totalDebts)
            {
                // Tìm độc giả tương ứng
                var reader = DataProvider.Ins.DB.Readers.FirstOrDefault(r => r.Id == debt.idReader);
                if (reader != null)
                {
                    // Cập nhật số nợ của độc giả
                    reader.debt = debt.TotalDebt;
                }
            }

            try
            {
                // Lưu tất cả thay đổi vào cơ sở dữ liệu sau khi cập nhật tất cả
                DataProvider.Ins.DB.SaveChanges();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine($"Lỗi khi lưu dữ liệu: {ex.Message}");
            }
        }


        private void ShowReader()
        {
            // Giả sử `reader` là một đối tượng kiểu `Reader`
            var reader = DataProvider.Ins.DB.Readers.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();

            if (Reader == null)
            {
                Reader = new ObservableCollection<Reader>();
            }

            if (reader != null)
            {
                debt = reader.debt;

                Reader.Clear(); // Xóa dữ liệu cũ trong danh sách
                Reader.Add(reader); // Thêm đối tượng `reader` vào danh sách

                // Gửi tin nhắn chứa Id
                WeakReferenceMessenger.Default.Send(new ReaderIdMessage(reader.Id));

            }

        }

        //Phương thức thêm mới sách
        private void AddReader()
        {
            // Kiểm tra nếu tất cả thông tin đã hợp lệ
            if (string.IsNullOrWhiteSpace(UserNameText) || string.IsNullOrWhiteSpace(Address) ||

                string.IsNullOrWhiteSpace(ReaderType) || string.IsNullOrWhiteSpace(Email) || !Dob.HasValue || !DateCreated.HasValue)
            {
                // Hiển thị thông báo lỗi (có thể dùng MessageBox hoặc Notification)
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin cho Độc Giả.");
                return;
            }

            // Thực hiện thao tác thêm sách vào cơ sở dữ liệu hoặc danh sách
            Reader newReader = new Reader()
            {
                UserNameText = UserNameText,
                Address = Address,
                Email = Email,
                ReaderType = ReaderType,
                Dob = Dob,
                DateCreated = DateCreated,
            };

            DataProvider.Ins.DB.Readers.Add(newReader);
            DataProvider.Ins.DB.SaveChanges();
            List.Add(newReader);
            _allList.Add(newReader);
            MessageBox.Show("Thêm Độc Giả thành công!");

            // Làm sạch các trường nhập liệu sau khi thêm
            ClearFields();
        }

        // Phương thức cập nhật cuốn sách đã chọn
        private void UpdateReader()
        {
            var reader = DataProvider.Ins.DB.Readers.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
            // Kiểm tra nếu tất cả thông tin đã hợp lệ
            if (string.IsNullOrWhiteSpace(UserNameText) || string.IsNullOrWhiteSpace(Address) ||

                string.IsNullOrWhiteSpace(ReaderType) || string.IsNullOrWhiteSpace(Email) || !Dob.HasValue || !DateCreated.HasValue)
            {
                // Hiển thị thông báo lỗi (có thể dùng MessageBox hoặc Notification)
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin cho Độc Giả.");
                return;
            }
            reader.UserNameText = UserNameText;
            reader.Address = Address;
            reader.Email = Email;
            reader.ReaderType = ReaderType;
            reader.Dob = Dob;
            reader.DateCreated = DateCreated;
            DataProvider.Ins.DB.SaveChanges();
            List.Remove(SelectedItem);
            List.Add(reader);
            _allList.Add(reader);
            SelectedItem = reader;
            OnPropertyChanged("List");

            MessageBox.Show("Cập nhật Độc Giả thành công!");
            ClearFields();
        }

        //Xóa các trường nhập dữ liệu
        private void ClearFields()
        {
            SelectedItem = null;
            UserNameText = string.Empty;
            Address = string.Empty;
            Email = string.Empty;
            ReaderType = string.Empty;
            Dob = null;
            DateCreated = null;
        }

        //Xóa nhân viên 
        private void DeleteReader()
        {
            var Reader = DataProvider.Ins.DB.Readers.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();

            if (Reader == null) return;

            // Xóa sách khỏi cơ sở dữ liệu
            DataProvider.Ins.DB.Readers.Remove(Reader);
            DataProvider.Ins.DB.SaveChanges();

            // Xóa sách khỏi danh sách hiện tại
            List.Remove(Reader);
            _allList.Remove(Reader);

            MessageBox.Show("Xóa Độc Giả thành công!");
            ClearFields();
        }

        // Chức năng tìm kiếm
        private void FilterReaders()
        {
            // Lọc sách theo SearchText hoặc BookId
            var FilteredReaders = _allList.Where(b => string.IsNullOrEmpty(SearchText) ||
                                                      b.UserNameText.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                                      b.Id.ToString().Contains(SearchText)).ToList();


            // Cập nhật danh sách Books
            List = new ObservableCollection<Reader>(FilteredReaders);
        }
    }
}