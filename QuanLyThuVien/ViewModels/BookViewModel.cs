using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using QuanLyThuVien.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
namespace QuanLyThuVien.ViewModels
{
    public class BookViewModel : BaseViewModel
    {
        private readonly BookService _bookService;

        public ObservableCollection<Book> Books { get; set; }

        private Book _selectedBook;
        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
                _selectedBook = value;
                OnPropertyChanged(nameof(SelectedBook));
            }
        }

        public ICommand AddBookCommand { get; set; }
        public ICommand UpdateBookCommand { get; set; }
        public ICommand DeleteBookCommand { get; set; }
        public ICommand LoadBooksCommand { get; set; }


        public BookViewModel()
        {
            _bookService = new BookService();
            Books = new ObservableCollection<Book>(_bookService.GetAllBooks());

            AddBookCommand = new RelayCommand(AddBook);
            UpdateBookCommand = new RelayCommand(UpdateBook);
            DeleteBookCommand = new RelayCommand(DeleteBook);
            LoadBooksCommand = new RelayCommand(LoadBooks);
        }

        private void AddBook()
        {
            if (SelectedBook != null)
            {
                _bookService.AddBook(new Book
                {
                    BookName = SelectedBook.BookName,
                    Author = SelectedBook.Author,
                    Category = SelectedBook.Category,
                    Publisher = SelectedBook.Publisher,
                    DatePublish = SelectedBook.DatePublish,
                    DateImport = SelectedBook.DateImport,
                    Price = SelectedBook.Price
                });

                Books.Add(SelectedBook);
                SelectedBook = null; // Reset selection
            }
        }

        private void UpdateBook()
        {
            if (SelectedBook != null)
            {
                _bookService.UpdateBook(SelectedBook);
            }
        }

        private void DeleteBook()
        {
            if (SelectedBook != null)
            {
                _bookService.DeleteBook(SelectedBook.Id); // Gọi với ID của sách
                Books.Remove(SelectedBook); // Xóa sách khỏi danh sách hiển thị
                SelectedBook = null; // Bỏ chọn sách sau khi xóa
            }
        }

        private void LoadBooks()
        {
            using (var context = new LibraryContext())
            {
                Books = new ObservableCollection<Book>(context.Books.ToList());
            }
        }
    }
}
