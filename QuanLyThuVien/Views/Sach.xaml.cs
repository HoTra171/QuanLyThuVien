using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using QuanLyThuVien.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Reflection.Metadata.BlobBuilder;

namespace QuanLyThuVien
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Sach : Window
    {

        private BookService _bookService;
        public BookViewModel ViewModel { get; set; }
        public Sach()
        {
            InitializeComponent();
            _bookService = new BookService();
            LoadData();
            ViewModel = new BookViewModel();
            DataContext = ViewModel;
        }

        private void LoadData()
        {
            var books = _bookService.GetAllBooks();
            dataGrid.ItemsSource = books; // Gán danh sách sách vào DataGrid
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy dữ liệu từ các TextBox và DatePicker
            string bookName = tbBookName.Text;
            string category = tbCategory.Text; // Nếu bạn có một thuộc tính Category trong Book
            string author = tbAuthor.Text;
            string publisher = tbPublisher.Text;
            DateTime? datePublish = tbDatePublish.SelectedDate; // DatePicker sẽ trả về null nếu không có ngày nào được chọn
            DateTime? dateImport = tbDateImport.SelectedDate; // Tương tự cho ngày nhập
            decimal? price;

            // Chuyển đổi giá trị từ TextBox sang decimal
            if (!string.IsNullOrWhiteSpace(tbPrice.Text) && decimal.TryParse(tbPrice.Text, out decimal parsedPrice))
            {
                price = parsedPrice;
            }
            else
            {
                price = null; // Nếu không hợp lệ, gán giá trị null
            }

            // Tạo đối tượng Book mới với thông tin đã nhập
            var newBook = new Book
            {
                BookName = bookName,
                Author = author,
                Category = category,
                Publisher = publisher,
                DatePublish = datePublish,
                DateImport = dateImport,
                Price = price
                // Nếu bạn có Category, hãy thêm vào đây
            };

            // Tạo một BookService để thêm cuốn sách vào cơ sở dữ liệu
            var bookService = new BookService();
            bookService.AddBook(newBook);
            LoadData(); // Tải lại dữ liệu để cập nhật DataGrid
            // Thông báo cho người dùng
            MessageBox.Show("Thêm sách thành công!");

            // Có thể xóa dữ liệu trong các TextBox sau khi thêm
            ClearInputFields();
        }

        // Phương thức để xóa dữ liệu trong các TextBox
        private void ClearInputFields()
        {
            tbBookName.Clear();
            tbCategory.Clear();
            tbAuthor.Clear();
            tbPublisher.Clear();
            tbDatePublish.SelectedDate = null;
            tbDateImport.SelectedDate = null;
            tbPrice.Clear();
        }


        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra cuốn sách đã được chọn
            if (dataGrid.SelectedItem is Book selectedBook)
            {
                // Cập nhật các thuộc tính của cuốn sách từ TextBox
                selectedBook.BookName = tbBookName.Text;
                selectedBook.Category = tbCategory.Text; // Nếu bạn có thuộc tính Category
                selectedBook.Author = tbAuthor.Text;
                selectedBook.Publisher = tbPublisher.Text;
                selectedBook.DatePublish = tbDatePublish.SelectedDate;
                selectedBook.DateImport = tbDateImport.SelectedDate;

                decimal? price;
                if (!string.IsNullOrWhiteSpace(tbPrice.Text) && decimal.TryParse(tbPrice.Text, out decimal parsedPrice))
                {
                    price = parsedPrice;
                }
                else
                {
                    price = null;
                }
                selectedBook.Price = price;

                var bookService = new BookService();
                bookService.UpdateBook(selectedBook); // Cập nhật sách trong cơ sở dữ liệu

                MessageBox.Show("Cập nhật sách thành công!");
                LoadData(); // Tải lại dữ liệu
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sách để cập nhật!");
            }
        }


        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem is Book selectedBook)
            {
                // Hiển thị dữ liệu của cuốn sách được chọn vào các TextBox
                tbBookName.Text = selectedBook.BookName;
                tbCategory.Text = selectedBook.Category; // Nếu bạn có thuộc tính Category
                tbAuthor.Text = selectedBook.Author;
                tbPublisher.Text = selectedBook.Publisher;
                tbDatePublish.SelectedDate = selectedBook.DatePublish;
                tbDateImport.SelectedDate = selectedBook.DateImport;
                tbPrice.Text = selectedBook.Price?.ToString("F2") ?? ""; // Chuyển đổi giá trị decimal sang string
            }
        }

        private void DataGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid != null)
            {
                // Lấy hàng được chọn
                var clickedItem = dataGrid.SelectedItem as Book;
                if (clickedItem != null)
                {
                    // Đảo ngược giá trị của IsSelected
                    clickedItem.IsSelected = !clickedItem.IsSelected;
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy danh sách các sách được chọn để xóa
            var selectedBooks = ViewModel.Books.Where(b => b.IsSelected).ToList();

            // Tạo một chuỗi để hiển thị thông tin sách
            StringBuilder message = new StringBuilder("Các sách được chọn để xóa:\n");
            foreach (var book in selectedBooks)
            {
                MessageBox.Show("Đã chọn");
            }

            // Kiểm tra nếu có sách nào được chọn
            if (selectedBooks.Count > 0)
            {
                foreach (var book in selectedBooks)
                {
                    // Xóa sách khỏi cơ sở dữ liệu
                    _bookService.DeleteBook(book.Id);

                    // Xóa sách khỏi danh sách hiển thị (ObservableCollection)
                    ViewModel.Books.Remove(book);
                }

                MessageBox.Show("Đã xóa sách thành công!"); // Thông báo sau khi xóa xong
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sách để xóa."); // Hiển thị thông báo nếu không có sách nào được chọn
            }
        }

    }
}