using System;
using System.Collections.Generic;
using System.Linq;
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

namespace QuanLyThuVien
{
    /// <summary>
    /// Interaction logic for DocGia.xaml
    /// </summary>
    public partial class DocGia : Window
    {
        public DocGia()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            List<DocGia1> docGias = new List<DocGia1>()
            {
                new DocGia1() { Id = 1, HoTen = "Độc giả mới 1", NgaySinh = new DateTime(2000, 1, 1), Email = "abc123@gmail.com", DiaChi = "Hà Tĩnh", NgayTaoThe = new DateTime(2020, 7, 7) },
                // Thêm các độc giả khác ở đây
            };

        }
        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }
    }

    public class DocGia1
    {
        public int Id { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgayTaoThe { get; set; }
    }
}

