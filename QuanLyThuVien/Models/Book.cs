﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;

namespace QuanLyThuVien.Models
{

    [Table("BOOK")]
    public class Book 
    {
        [Key]

        [Column("ID")]

        public int Id { get; set; }

        [Required]
        [MaxLength(255)]

        [Column("BOOK_NAME")]
        public string BookName { get; set; }
        
        [Required]
        [Column("CATEGORY")]
        public string Category {  get; set; }
        
        [Required]
        [Column("PUBLISHER")]
        public string Publisher { get; set; }   

        [Required]
        [MaxLength(255)]
        [Column("AUTHOR")]
        public string Author { get; set; }

        [Column("DATE_PUBLISH")]
        public DateTime? DatePublish { get; set; }

        [Column("DATE_IMPORT")]
        public DateTime? DateImport{ get; set; }

        [Column("PRICE")]
        public decimal? Price { get; set; }

        public ICollection<ListBorrowed> ListBorrowed { get; set; }

    }
}
