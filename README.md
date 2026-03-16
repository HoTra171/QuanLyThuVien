# 📚 Dự án Quản Lý Thư Viện (Library Management System)

Dự án Quản Lý Thư Viện là một ứng dụng Desktop được phát triển bằng **C# WPF** (Windows Presentation Foundation) dựa trên mô hình kiến trúc **MVVM** (Model-View-ViewModel). Ứng dụng cung cấp các tính năng toàn diện cho việc quản lý nhà kho sách, độc giả, quá trình mượn - trả sách, cũng như quản lý nhân viên và báo cáo thống kê trong một thư viện.

Giao diện người dùng được thiết kế hiện đại, trực quan thông qua việc sử dụng thư viện **Material Design In XAML Toolkit**.

---

## 🛠 Công nghệ sử dụng

- **Ngôn ngữ lập trình:** C#
- **Nền tảng/Framework:** .NET Framework 4.7.2, WPF
- **Kiến trúc:** MVVM (Model - View - ViewModel)
- **Cơ sở dữ liệu:** SQL Server (sử dụng Entity Framework 6.2.0)
- **Giao diện (UI):** MaterialDesignThemes 3.2.0, MaterialDesignColors 1.2.7
- **Dependencies khác:** System.Windows.Interactivity.WPF

---

## 🌟 Các tính năng chính

Ứng dụng bao gồm các phân hệ (module) chính như sau:

1. **🔒 Hệ thống (System):**
   - Đăng nhập (Login).
   - Đổi mật khẩu (Change Password).
   - Phân quyền theo vai trò (Roles) trong hệ thống.

2. **📖 Quản lý Sách (Book Management):**
   - Thêm, sửa, xóa, tìm kiếm sách.
   - Quản lý các thông tin về sách, thể loại, tác giả, nhà xuất bản, v.v.

3. **👥 Quản lý Độc Giả (Reader Management):**
   - Quản lý hồ sơ mở thẻ độc giả.
   - Thêm, sửa, xóa, tra cứu thông tin độc giả dễ dàng.

4. **🔄 Quản lý Mượn / Trả Sách (Borrowing/Returning):**
   - Lập phiếu mượn sách.
   - Lập phiếu trả sách.
   - Quản lý chi tiết tình trạng sách đang được mượn.

5. **👔 Quản lý Nhân Viên (Employee Management):**
   - Quản lý danh sách nhân sự làm việc tại thư viện.
   - Thêm, sửa, xóa thông tin cá nhân của quản trị viên và thủ thư.

6. **📊 Báo cáo Thống kê (Reports & Charts):**
   - Thống kê tình trạng hoạt động mượn trả sách.
   - Hiển thị dữ liệu trực quan bằng biểu đồ (BaoCaoChart).
   - Báo cáo số liệu người dùng, độc giả, đầu sách hiện tại.

---

## 📁 Cấu trúc thư mục dự án

```text
QuanLyThuVien/
│
├── Models/        # Các class biểu diễn cấu trúc dữ liệu, giao tiếp với Entity Framework Database
├── ViewModels/    # Các lớp xử lý logic cho ứng dụng, liên kết dữ liệu View và Model (MVVM)
│   ├── BookViewModel.cs
│   ├── DangNhapViewModel.cs
│   ├── DocGiaViewModal.cs
│   ├── MuonSachViewModal.cs
│   ├── ...
│
├── Views/         # Giao diện người dùng (User Control / Window) được viết bằng XAML
│   ├── BaoCao.xaml
│   ├── DocGia.xaml
│   ├── MuonSach.xaml
│   ├── Sach.xaml
│   ├── ...
│
├── Windows/       # Các màn hình gốc (Window Container)
├── SQL/           # Tập lệnh hoặc file đính kèm liên quan đến cấu trúc Cơ sở dữ liệu
├── Img/ & icon/   # Chứa các file hình ảnh, biểu tượng (assets)
└── ResourceXAML/  # Các file resource dictionary của WPF phục vụ tái sử dụng styles, templates
```

---

## 🚀 Hướng dẫn Cài đặt & Khởi chạy

1. **Yêu cầu hệ thống:**
   - Cài đặt [Visual Studio 2019](https://visualstudio.microsoft.com/) hoặc mới hơn (Hỗ trợ tốt nhất _Desktop development with .NET_).
   - Có sẵn (hoặc tiến hành cài đặt) **SQL Server**.

2. **Cài đặt Cơ sở dữ liệu:**
   - Mở SQL Server Management Studio (SSMS).
   - Chạy các script tạo database (có thể tìm thấy trong thư mục `SQL/` nếu có) hoặc cấu hình Connection String tham chiếu đến Database.
   - Đảm bảo **App.config** có chứa `connectionString` đúng với thiết lập SQL Server cục bộ của bạn.

3. **Mở và Build Dự án:**
   - Mở tệp Solution `QuanLyThuVien.sln` bằng Visual Studio.
   - Click chuột phải vào tên Solution (trong _Solution Explorer_) và chọn **Restore NuGet Packages** để tự động tải về các thư viện (Entity Framework, MaterialDesign, v.v).
   - Nhấn **F5** hoặc chọn **Start** để build và chạy ứng dụng.

---

## 💡 Đóng góp và Phát triển

Nếu bạn muốn tùy biến hoặc đóng góp thêm cho ứng dụng, bạn có thể tham khảo kỹ hơn cấu trúc mã trong thư mục `ViewModels/` và `Views/`. 
Các màn hình chức năng mới cần tuân thủ quy tắc thêm View vào thư mục `Views` và Logic vào `ViewModels`, kế thừa `BaseViewModel` để tiếp tục xây dựng theo chuẩn MVVM đồng nhất.

---
_Cảm ơn bạn đã quan tâm và sử dụng dự án này!_
