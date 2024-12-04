using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Formats.Tar;

namespace QuanLyThuVien.Models
{

    [Table("LIST_BORROWED")]
    public class ListBorrowed
    {
            [Column("ID")]
            [Key]
            public int Id { get; set; }

            [Column("ID_READER")]
            [Required]
            public int IdReader { get; set; }
            public Reader Reader { get; set; } // Khóa ngoại đến CardReader

            [Column("ID_BOOK")]
            [Required]
            public int IdBook { get; set; }
            public Book Book { get; set; } // Khóa ngoại đến Book

            [Column("DATE_BORROWED")]
            [Required]
            public DateTime DateBorrowed { get; set; }


            [Column("DATE_EXPIRED")]
            [Required]
            public DateTime DateExpired { get; set; }

            [Column("STATUS_BORROW")]
            [Required]
            [MaxLength(20)]
            public string Status { get; set; } = "Mới";  // Giá trị mặc định trong C#



    }
}
