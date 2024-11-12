using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Models
{
 public   class ListBorrowed
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdCard { get; set; }

        public int? IdBook { get; set; }
        public bool StatusBorrow { get; set; }

        [Required]
        public DateTime DateBorrowed { get; set; }

        [Required]
        public DateTime DateExpired { get; set; }

        [ForeignKey("IdCard")]
        public CardReader CardReader { get; set; }

        [ForeignKey("IdBook")]
        public Book Book { get; set; }
    }
}
