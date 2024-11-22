using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;

namespace QuanLyThuVien.ViewModels
{
    internal class DieuKhienViewModal
    {
        public ICommand LoadDieuKhienCommand { get; set; }

        public DieuKhienViewModal()
        {
            LoadDieuKhienCommand = new RelayCommand<Button>(p => { return p == null ? false : true; }, p =>
            {

            });
        }
    }
}
