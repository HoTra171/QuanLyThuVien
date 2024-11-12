using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Models
{
   public class CardReader
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdReader { get; set; }

        public DateTime? CreateAt { get; set; }
        public int TypeReader { get; set; }

        [ForeignKey("IdReader")]
        public Reader Reader { get; set; }

        public ICollection<ListBorrowed> ListBorrowed { get; set; }
    }
}
