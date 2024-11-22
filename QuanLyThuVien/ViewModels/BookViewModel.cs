using QuanLyThuVien.Models;
using QuanLyThuVien.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows;
using System.Windows.Documents;

namespace QuanLyThuVien.ViewModels
{
    public class BookViewModel : BaseViewModel
    {
        public ObservableCollection<Book> _Books;
        public ObservableCollection<Book> Books
        {
            get => _Books;
            set
            {
                _Books = value;
                OnPropertyChanged();
            }
        }

        private Book _SelectedItem;
        public Book SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));

                // Cập nhật các thuộc tính khi chọn một dòng
                if (SelectedItem != null)
                {
                    BookName = SelectedItem.BookName;
                    Category = SelectedItem.Category;
                    Author = SelectedItem.Author;
                    Publisher = SelectedItem.Publisher;
                    DatePublish = SelectedItem.DatePublish;
                    DateImport = SelectedItem.DateImport;
                    Price = SelectedItem.Price ?? 0m; // Nếu Price là null, gán giá trị 0

                }
            }
        }


        private string _bookName;
        public string BookName
        {
            get { return _bookName; }
            set
            {
                _bookName = value;
                OnPropertyChanged();
            }
        }

        private string _category;
        public string Category
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged();
            }
        }

        private string _author;
        public string Author
        {
            get { return _author; }
            set
            {
                _author = value;
                OnPropertyChanged();
            }
        }

        private string _publisher;
        public string Publisher
        {
            get { return _publisher; }
            set
            {
                _publisher = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _datePublish;
        public DateTime? DatePublish
        {
            get { return _datePublish; }
            set
            {
                _datePublish = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _dateImport;
        public DateTime? DateImport
        {
            get { return _dateImport; }
            set
            {
                _dateImport = value;
                OnPropertyChanged();
            }
        }

        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        // Các lệnh cho chức năng Thêm, Cập nhật, Xóa và Tải sách
        public ICommand AddBookCommand { get; set; }
        public ICommand UpdateBookCommand { get; set; }
        public ICommand DeleteBookCommand { get; set; }
        public ICommand LoadBooksCommand { get; set; }

        public BookViewModel()
        {
            // Khởi tạo danh sách sách từ cơ sở dữ liệu
            Books = new ObservableCollection<Book>(DataProvider.Ins.DB.Books);

            // Định nghĩa các lệnh
            AddBookCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(BookName))
                    return false;

                if (SelectedItem != null)
                    return false;

                // Kiểm tra trùng tên sách
                var isDuplicate = DataProvider.Ins.DB.Books.Any(x => x.BookName == BookName);
                if (isDuplicate)
                    return false;

                return true;
            },
            (p) => AddBook());


            UpdateBookCommand = new RelayCommand<object>(p =>
            {
            if (string.IsNullOrEmpty(BookName) || SelectedItem == null)
                    return false;

                // Kiểm tra trùng tên sách
                var isDuplicate = DataProvider.Ins.DB.Books.Any(x => x.BookName == BookName);
                if (isDuplicate)
                    return false;

                return true;
            },
            p => UpdateBook());
        }

        //Phương thức thêm mới sách
        private void AddBook()
        {
            // Kiểm tra nếu tất cả thông tin đã hợp lệ
            if (string.IsNullOrWhiteSpace(BookName) || string.IsNullOrWhiteSpace(Category) ||
                string.IsNullOrWhiteSpace(Author) || string.IsNullOrWhiteSpace(Publisher) ||
                !DatePublish.HasValue || !DateImport.HasValue || Price <= 0)
            {
                // Hiển thị thông báo lỗi (có thể dùng MessageBox hoặc Notification)
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin cho sách.");
                return;
            }

            // Thực hiện thao tác thêm sách vào cơ sở dữ liệu hoặc danh sách
            Book newBook = new Book()
            {
                BookName = BookName,
                Category = Category,
                Author = Author,
                Publisher = Publisher,
                DatePublish = DatePublish.Value,
                DateImport = DateImport.Value,
                Price = Price
            };

            DataProvider.Ins.DB.Books.Add(newBook);
            DataProvider.Ins.DB.SaveChanges();
            Books.Add(newBook);
            MessageBox.Show("Thêm sách thành công!");

            // Làm sạch các trường nhập liệu sau khi thêm
            ClearFields();
        }

        // Phương thức cập nhật cuốn sách đã chọn
        private void UpdateBook()
        {
            var book = DataProvider.Ins.DB.Books.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
            // Kiểm tra nếu tất cả thông tin đã hợp lệ
            if (string.IsNullOrWhiteSpace(BookName) || string.IsNullOrWhiteSpace(Category) ||
                string.IsNullOrWhiteSpace(Author) || string.IsNullOrWhiteSpace(Publisher) ||
                !DatePublish.HasValue || !DateImport.HasValue || Price <= 0)
            {
                // Hiển thị thông báo lỗi (có thể dùng MessageBox hoặc Notification)
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin cho sách.");
                return;
            }
            book.BookName = BookName;
            book.Category = Category;
            book.Author = Author;
            book.Publisher = Publisher;
            book.DatePublish = DatePublish.Value;
            book.DateImport = DateImport.Value;
            book.Price = Price;
            DataProvider.Ins.DB.SaveChanges();
            Books.Remove(SelectedItem);
            Books.Add(book);
            SelectedItem = book;
            OnPropertyChanged("Books");

            MessageBox.Show("Cập nhật sách thành công!");
            ClearFields();
        }

        //Xóa các trường nhập dữ liệu
        private void ClearFields()
        {
            BookName = string.Empty;
            Category = string.Empty;
            Author = string.Empty;
            Publisher = string.Empty;
            DatePublish = null;
            DateImport = null;
            Price = 0;
        }
    }
}
