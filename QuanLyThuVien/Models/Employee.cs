using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien.Models
{
    [Table("EMPLOYEES")]
    public class Employee
    {
        // Mã số nhân viên (Khóa chính)
        [Column("EMPLOYEE_ID")]
        [Key]
        public int EmployeeId { get; set; }

        // Họ tên nhân viên
        [Column("FULL_NAME")]
        [Required]
        [MaxLength(255)]
        public string FullName { get; set; }

        // Địa chỉ nhân viên
        [Column("ADDRESS")]
        [MaxLength(255)]
        public string Address { get; set; }

        // Số điện thoại nhân viên
        [Column("PHONE_NUMBER")]
        [Required]
        [MaxLength(255)]
        public string PhoneNumber { get; set; }

        // Ngày sinh nhân viên
        [Column("DOB")]
        [Required]
        public DateTime Dob { get; set; }

        // Tên tài khoản liên kết với ACCOUNT_USER (Khóa ngoại)
        [Column("USER_ACCOUNT")]
        [Required]
        [MaxLength(255)]
        public string UserAccount { get; set; }

    }
}
