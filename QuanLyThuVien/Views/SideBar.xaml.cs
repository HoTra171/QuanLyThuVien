using QuanLyThuVien.Models;
using QuanLyThuVien.ViewModels;
using QuanLyThuVien.Views;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyThuVien
{
    /// <summary>
    /// Interaction logic for SideBarTop.xaml
    /// </summary>
    public partial class SideBar : UserControl
    {
        public SideBar()
        {
            InitializeComponent();
        }

        private bool isSidebarVisible = false;

        private void BtnToggleSidebar_Click(object sender, RoutedEventArgs e)
        {
            if (!isSidebarVisible)
            {
                // Mở sidebar
                BtnToggleSidebar.Visibility = Visibility.Collapsed;
                BtnExit.Visibility = Visibility.Visible;
                LblHelloAdmin.Visibility = Visibility.Visible;
                LblAdmin.Visibility = Visibility.Visible;
                BtnDangXuat.Visibility = Visibility.Visible;
                BtnDoiMatKhau.Visibility = Visibility.Visible;
                TblBaoCao.Visibility = Visibility.Visible;
                TblDieuKhien.Visibility = Visibility.Visible;
                TblDocGia.Visibility = Visibility.Visible;
                TblNhanVien.Visibility = Visibility.Visible;
                TblMuonSach.Visibility = Visibility.Visible;
                TblSach.Visibility = Visibility.Visible;
                // Nếu cần, có thể thêm Visibility cho các nút khác
            }
            isSidebarVisible = true; // Đánh dấu là sidebar đang mở
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            // Đóng sidebar
            BtnToggleSidebar.Visibility = Visibility.Visible;
            BtnExit.Visibility = Visibility.Collapsed;
            LblHelloAdmin.Visibility = Visibility.Collapsed;
            LblAdmin.Visibility = Visibility.Collapsed;
            BtnDangXuat.Visibility = Visibility.Collapsed;
            BtnDoiMatKhau.Visibility = Visibility.Collapsed;
            TblBaoCao.Visibility = Visibility.Collapsed;
            TblDieuKhien.Visibility = Visibility.Collapsed;
            TblDocGia.Visibility = Visibility.Collapsed;
            TblNhanVien.Visibility = Visibility.Collapsed;
            TblMuonSach.Visibility = Visibility.Collapsed;
            TblSach.Visibility = Visibility.Collapsed;
            // Nếu cần, có thể thêm Visibility cho các nút khác
            isSidebarVisible = false; // Đánh dấu là sidebar đã đóng
        }

        private void BtnDieuKhien_Click(object sender, RoutedEventArgs e)
        {
            DieuKhien dieukhien = new DieuKhien();
            dieukhien.Show();

            Window hi = DieuKhien.GetWindow(this);
            if (hi != null)
            {
                // Đóng cửa sổ cha
                hi.Close();
            }
        }

        private void BtnDocGia_Click(object sender, RoutedEventArgs e)
        {
            DocGia docgia = new DocGia();
            docgia.Show();

            Window hi = DocGia.GetWindow(this);
            if (hi != null)
            {
                // Đóng cửa sổ cha
                hi.Close();
            }
        }

        private void BtnSach_Click(object sender, RoutedEventArgs e)
        {
            Sach sach = new Sach();
            sach.Show();

            Window hi = Sach.GetWindow(this);
            if (hi != null)
            {
                // Đóng cửa sổ cha
                hi.Close();
            }
        }

        private void BtnBaoCao_Click(object sender, RoutedEventArgs e)
        {
            if (DangNhapViewModel.Role == true)
            {
                BaoCao baocao = new BaoCao();
                baocao.Show();
                Window hi = BaoCao.GetWindow(this);
                if (hi != null)
                {
                    // Đóng cửa sổ cha
                    hi.Close();
                }
            }
            else
            {
                // Hiển thị thông báo không đủ quyền truy cập
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng này", "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnMuonSach_Click(object sender, RoutedEventArgs e)
        {
            MuonSach muon = new MuonSach();
            muon.Show();

            Window hi = MuonSach.GetWindow(this);
            if (hi != null)
            {
                // Đóng cửa sổ cha
                hi.Close();
            }
        }

        private void BtnNhanVien_Click(object sender, RoutedEventArgs e)
        {
            if (DangNhapViewModel.Role == true)
            {
                NhanVien nhanvien = new NhanVien();
                nhanvien.Show();

                Window hi = NhanVien.GetWindow(this);
                if (hi != null)
                {
                    // Đóng cửa sổ cha
                    hi.Close();
                }
            }
            else
            {
                // Hiển thị thông báo không đủ quyền truy cập
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng này", "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDangXuat_Click(object sender, RoutedEventArgs e)
        {
            DangNhap dangxuat = new DangNhap();
            dangxuat.Show();

            Window hi = DangNhap.GetWindow(this);
            if (hi != null)
            {
                // Đóng cửa sổ cha
                hi.Close();
            }
        }

        private void BtnDoiMatKhau_Click(object sender, RoutedEventArgs e)
        {

            DoiMatKhau doiMatKhauWindow = new DoiMatKhau();
            doiMatKhauWindow.Show();

            Window hi = DoiMatKhau.GetWindow(this);
            if (hi != null)
            {
                // Đóng cửa sổ cha (sidebar)
                hi.Close();
            }
        }
    }
}