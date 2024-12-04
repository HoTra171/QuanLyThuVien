using CommunityToolkit.Mvvm.Messaging;
using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using static QuanLyThuVien.ViewModels.DocGiaViewModal;
using static System.Reflection.Metadata.BlobBuilder;

namespace QuanLyThuVien.ViewModels
{
    internal class MuonSachViewModal : BaseViewModel
    {
        public ObservableCollection<Book> _ListBook;
        public ObservableCollection<Book> ListBook
        {
            get => _ListBook;
            set
            {
                _ListBook = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Book> _allBook;


        private ObservableCollection<ListBorrowed> _allListBorrowd;


        public ObservableCollection<ListBorrowed> _ListBorrowed;
        public ObservableCollection<ListBorrowed> ListBorrowed
        {
            get => _ListBorrowed;
            set
            {
                _ListBorrowed = value;
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
                FilterBooks();
            }
        }

        private int _readerId;
        public int ReaderId
        {
            get => _readerId;
            set
            {
                _readerId = value;
                OnPropertyChanged();
                LoadReaderData(_readerId); // Gọi phương thức load thông tin độc giả
            }
        }


        private int _BookId;
        public int BookId
        {
            get => _BookId;
            set
            {
                _BookId = value;
                OnPropertyChanged();
            }
        }

        private DateTime _DateBorrowed;
        public DateTime DateBorrowed
        {
            get => _DateBorrowed;
            set
            {
                if (_DateBorrowed != value)
                {
                    _DateBorrowed = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _DateExpired;
        public DateTime DateExpired
        {
            get => _DateExpired;
            set
            {
                if (_DateExpired != value)
                {
                    _DateExpired = value;
                    OnPropertyChanged();
                }
            }
        }


        private Book _selectBookItem;
        public Book SelectBookItem
        {
            get => _selectBookItem;
            set
            {
                _selectBookItem = value;
                OnPropertyChanged();

                // Cập nhật các thuộc tính khi chọn một dòng
                if (SelectBookItem != null)
                {
                    BookId = SelectBookItem.Id;
                }

            }

        }
        
        private ListBorrowed _SelectBookBorrowItem;
        public ListBorrowed SelectBookBorrowItem
        {
            get => _SelectBookBorrowItem;
            set
            {
                _SelectBookBorrowItem = value;
                OnPropertyChanged();

                // Cập nhật các thuộc tính khi chọn một dòng
                if (SelectBookBorrowItem != null)
                {
                    BookId = SelectBookBorrowItem.Id;
                }

            }

        }

        public ICommand SearchCommand { get; set; }
        public ICommand BooksBorrow { get; set; }
        public ICommand ReturnBook { get; set; }


        public MuonSachViewModal()
        {
            ListBorrowed = new ObservableCollection<ListBorrowed>(DataProvider.Ins.DB.ListBorrowed); // Hiển thị ban đầu là toàn bộ sách


            // Lưu dữ liệu ban đầu vào danh sách tạm
            _allBook = new ObservableCollection<Book>(DataProvider.Ins.DB.Books);
            ListBook = new ObservableCollection<Book>(_allBook); // Hiển thị ban đầu là toàn bộ sách

            SearchCommand = new RelayCommand<string>(p => true, p => FilterBooks());


            // Đăng ký nhận tin nhắn
            WeakReferenceMessenger.Default.Register<ReaderIdMessage>(this, (r, message) =>
            {
                ReaderId = message.ReaderId;

                // Tải dữ liệu dựa trên ReaderId nếu cần
                LoadReaderData(ReaderId);
            });

            //Thay đổi status
            UpdateStatus();

            BooksBorrow = new RelayCommand<object>((p) =>
            {

                if (SelectBookItem == null)
                    return false;

                return true;
            },
            (p) => AddListBorrow());

            ReturnBook = new RelayCommand<object>((p) =>
            {

                if (SelectBookBorrowItem == null)
                    return false;

                return true;
            },
            (p) => ReturnListBook());

        }

        private void ReturnListBook()
        {
            var BookBorrow = DataProvider.Ins.DB.ListBorrowed.Where(x => x.Id == SelectBookBorrowItem.Id).SingleOrDefault();

            if (BookBorrow == null) return;

            // Xóa sách khỏi cơ sở dữ liệu
            DataProvider.Ins.DB.ListBorrowed.Remove(BookBorrow);
            DataProvider.Ins.DB.SaveChanges();
            // Xóa sách khỏi danh sách hiện tại
            ListBorrowed.Remove(BookBorrow);
            MessageBox.Show("Trả sách thành công");

        }

        private void AddListBorrow()
        {

            // Lấy ra đối tượng sách từ cơ sở dữ liệu
            var book = DataProvider.Ins.DB.Books.FirstOrDefault(p => p.Id == SelectBookItem.Id);

            // Thực hiện thao tác thêm sách vào cơ sở dữ liệu hoặc danh sách
            ListBorrowed newBookBorrow = new ListBorrowed()
            {
                IdReader = ReaderId,
                IdBook = book.Id,
                DateBorrowed = DateTime.Now,
                DateExpired = DateTime.Now.AddMonths(1),
            };

            DataProvider.Ins.DB.ListBorrowed.Add(newBookBorrow);
            DataProvider.Ins.DB.SaveChanges();
            ListBorrowed.Add(newBookBorrow);
            MessageBox.Show("Đã Mượn Sách");

        }

        private void LoadReaderData(int readerId)
        {
            var listBorrowedReader = ListBorrowed.Where(x => x.IdReader == ReaderId).ToList();
            ListBorrowed = new ObservableCollection<ListBorrowed>(listBorrowedReader);
        }

        // Chức năng tìm kiếm
        private void FilterBooks()
        {
            // Lọc sách theo SearchText hoặc BookId
            var filteredBooks = _allBook.Where(b => string.IsNullOrEmpty(SearchText) ||
                                                      b.BookName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                                      b.Id.ToString().Contains(SearchText)).ToList();


            // Cập nhật danh sách Books
            ListBook = new ObservableCollection<Book>(filteredBooks);
        }


        //cải tiến mỗi khi chạy ứng dụng thì kiểm tra xem ngày trả với ngày hiện tại nếu ngày trả bé hơn ngày hiện tại thì thay đổi status thành cũ
        private void UpdateStatus()
        {
            var BookBorrow = DataProvider.Ins.DB.ListBorrowed;
            foreach (var item in BookBorrow)
            {
                if (item.DateExpired < DateTime.Now)
                {
                    if (item.Status != "Cũ") 
                    {
                        item.Status = "Cũ";
                    }
                }
                else
                {
                    if (item.Status != "Mới") 
                    {
                        item.Status = "Mới";
                    }
                }
            }

            DataProvider.Ins.DB.SaveChanges();
        }

    }
}
