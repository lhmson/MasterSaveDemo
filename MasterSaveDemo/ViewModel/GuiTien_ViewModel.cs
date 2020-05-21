using MasterSaveDemo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private SOTIETKIEM search_MaSo(string MaSo)
        {
            ObservableCollection<SOTIETKIEM> List_STK = new ObservableCollection<SOTIETKIEM>(DataProvider.Ins.DB.SOTIETKIEMs);

            foreach (SOTIETKIEM STK in List_STK)
            {
                if (STK.MaSoTietKiem == MaSo)
                    return STK;

                //debug += "\n" + LTK.TenLoaiTietKiem + "a";
            }
            return null;
        }
        private string search_TenLTK(string MaLTK)
        {
            ObservableCollection<LOAITIETKIEM> List_LTK = new ObservableCollection<LOAITIETKIEM>(DataProvider.Ins.DB.LOAITIETKIEMs);

            foreach (LOAITIETKIEM LTK in List_LTK)
            {
                if (LTK.MaLoaiTietKiem == MaLTK)
                    return LTK.TenLoaiTietKiem;

                //debug += "\n" + LTK.TenLoaiTietKiem + "a";
            }
            return "0";
        }
        #endregion
        #region CheckValid
        private string CheckValid()
        {
            string error = "";
            if (MaSoTietKiem == "" || MaSoTietKiem == null)
                error += "Sổ chưa được tạo mã sổ";
            //if (CbxTenLoaiTietKiem == "" || CbxTenLoaiTietKiem == null)
              //  error += "\nSổ chưa chọn hình thức loại tiết kiệm";
            if (TenKhachHang == "" || TenKhachHang == null)
                error += "\nTên khách hàng chưa được nhập";
            //if (CMND == "" || CMND == null)
             //   error += "\nCMND chưa được nhập";
           // if (DiaChi == "" || DiaChi == null)
            //    error += "\nĐịa chỉ chưa được nhập";
            //if (SoTienGuiBanDau == "" || SoTienGuiBanDau == null)
           //     error += "\nSố tiền gửi của khách hàng chưa được nhập";
          // if (DonVi == "" || DonVi == null)
           //     error += "\nĐơn vị tiền tệ chưa được xác định";
            return error;

        }

        private string Cal_NgayDaoHan(int days)
        {
            double d = Double.Parse(days.ToString());
            string date_NgayDaoHan = DateTime.Today.AddDays(d).ToString();
            date_NgayDaoHan = formatDate(date_NgayDaoHan);
            return date_NgayDaoHan;
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
        public ICommand CheckCommand { get; set; }
        public GuiTien_ViewModel()
        {
            NgayGui = formatDate(DateTime.Now.ToString());
            SOTIETKIEM stk = search_MaSo(MaSoTietKiem);
            if (stk!=null)
            {
                TenKhachHang = stk.TenKhachHang;
                NgayDaoHanKeTiep = stk.NgayDaoHanKeTiep.ToString();
                TenLoaiLoaiTietKiem = search_TenLTK(stk.MaLoaiTietKiem);
                SoCMND = stk.SoCMND;
            }
            
        }
    }
}
