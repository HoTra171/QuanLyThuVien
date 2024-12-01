using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien.Models
{
    [Table("READER")]
    public class Reader
    {
        //V
        [Column("ID")]
        [Key]
        public int Id { get; set; }

        //V
        [Column("USER_NAME_TEXT")]
        [Required]
        [MaxLength(255)]
        public string UserNameText { get; set; }

        [Column("EMAIL")]
        [Required]
        [MaxLength(255)]
        public string? Email { get; set; }

        //V
        [Column("DOB")]
        [Required]
        public DateTime? Dob { get; set; }

        //V
        [Column("DATECREATED")]
        [Required]
        public DateTime? DateCreated { get; set; }

        //V
        [Column("ADDRESS")]
        [MaxLength(255)]
        public string? Address { get; set; }

        //V
        [Column("DEBT")]
        [MaxLength(255)]
        public Decimal? Debt { get; set; }

        //V
        [Column("READERTYPE")]
        [MaxLength(255)]
        public string? ReaderType { get; set; }

        public ICollection<CardReader> CardReaders { get; set; }
    }
}
