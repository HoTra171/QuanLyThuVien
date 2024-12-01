using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
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
        public ICommand SearchCommand { get; set; }

        public MuonSachViewModal()
        {
            // Lưu dữ liệu ban đầu vào danh sách tạm
            _allBook = new ObservableCollection<Book>(DataProvider.Ins.DB.Books);
            ListBook = new ObservableCollection<Book>(_allBook); // Hiển thị ban đầu là toàn bộ sách

            SearchCommand = new RelayCommand<string>(p => true, p => FilterBooks());

        }

    }
}
