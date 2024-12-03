using CommunityToolkit.Mvvm.Messaging;
using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
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

        public ICommand SearchCommand { get; set; }
        public ICommand BooksBorrow { get; set; }



        public MuonSachViewModal()
        {
            _allListBorrowd = new ObservableCollection<ListBorrowed>(DataProvider.Ins.DB.ListBorrowed);
            ListBorrowed = new ObservableCollection<ListBorrowed>(_allListBorrowd); // Hiển thị ban đầu là toàn bộ sách


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


            BooksBorrow = new RelayCommand<object>((p) =>
            {

                if (SelectBookItem == null)
                    return false;

                return true;
            },
            (p) => AddListBorrow());


        }

        private void AddListBorrow()
        {

            var book = DataProvider.Ins.DB.Books.Where(p => p.Id == SelectBookItem.Id);
            var bookBorrow = new ListBorrowed();
            bookBorrow.IdReader = ReaderId;
            var a = book;


            //ListBorrowed.Add(book);

            //}
        }

        private void LoadReaderData(int readerId)
        {
            var listBorrowedReader = _allListBorrowd.Where(x => x.IdReader == ReaderId).ToList();
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
    }
}