using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MasterSaveDemo.ViewModel
{
    public class GuiTien_ViewModel : BaseViewModel
    {
        #region function 
        public string formatDate(string st)
        {
            string s ="";
            int i = 0;
            while (st[i] != ' ')
            {
                s += st[i];
                i++;
            }
            return s;
        }
        #endregion
        #region variables
        private string _NgayGui;
        public string NgayGui
        {
            get => _NgayGui; 
            set { _NgayGui = value; OnPropertyChanged(); }
        }

        private string _MaSoTietKiem;
        public string MaSoTietKiem
        {
            get => _MaSoTietKiem; 
            set { _MaSoTietKiem = value;OnPropertyChanged(); }
        }

        private string _SoTienGui;
        public string SoTienGui
        {
            get { return _SoTienGui; }
            set { _SoTienGui = value; OnPropertyChanged(); }
        }

        private string _TenKhachHang;
        public string TenKhachHang
        {
            get { return _TenKhachHang; }
            set { _TenKhachHang = value; OnPropertyChanged(); }
        }

        private string _TenLoaiLoaiTietKiem;
        public string TenLoaiLoaiTietKiem
        {
            get { return _TenLoaiLoaiTietKiem; }
            set { _TenLoaiLoaiTietKiem = value; OnPropertyChanged(); }
        }
        
        private string _SoCMND;
        public string SoCMND
        {
            get { return _SoCMND; }
            set { _SoCMND = value; OnPropertyChanged(); }
        }

        private string _NgayDaoHanKeTiep;
        public string NgayDaoHanKeTiep
        {
            get { return _NgayDaoHanKeTiep; }
            set { _NgayDaoHanKeTiep = value; OnPropertyChanged(); }
        }
        #endregion
        public ICommand CheckoutCommand { get; set; }
        public GuiTien_ViewModel()
        {
            NgayGui = formatDate(DateTime.Now.ToString());
            
        }
    }
}
