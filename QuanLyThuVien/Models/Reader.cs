using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Models
{
    public class Reader
    {
        [Key]
        public int Id { get; set; }

        [Required]  
        [MaxLength(255)]
        public string UserNameText { get; set; }

        [Required]
        public DateTime Dob { get; set; }

        [Required]
        [MaxLength(255)]
        public string PhoneNumber { get; set; }

        public ICollection<CardReader> CardReaders { get; set; }
    }
}
