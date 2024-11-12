using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace QuanLyThuVien.Models
{
    [Table ("ACCOUNT_USER")]
    public class AccountUser
    {
        
        [Column("ID")]
        [Key]
        public int Id { get; set; }

        [Column ("USER_ACCOUNT")]
        [Required]
        [MaxLength(255)]
        public string UserAccount { get; set; }

        [Column("USER_NAME_TEXT")]
        [Required]
        [MaxLength(255)]
        public string UserNameText { get; set; }

        [Column("PASSWORD_TEXT")]
        [Required]
        [MaxLength(255)]
        public string PasswordText { get; set; }
        [Column("DOB")]
        [Required]
        public DateTime Dob { get; set; }

        [Column("PHONE_NUMBER")]
        [Required]
        [MaxLength(255)]
        public string PhoneNumber { get; set; }
    }
}
