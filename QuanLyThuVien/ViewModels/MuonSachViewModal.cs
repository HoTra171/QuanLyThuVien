using CommunityToolkit.Mvvm.Messaging;
using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public ICommand SearchCommand { get; set; }

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