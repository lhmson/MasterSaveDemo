using MasterSaveDemo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        private int search_KyHan(string MaLTK)
        {
            ObservableCollection<LOAITIETKIEM> List_LTK = new ObservableCollection<LOAITIETKIEM>(DataProvider.Ins.DB.LOAITIETKIEMs);

            foreach (LOAITIETKIEM LTK in List_LTK)
            {
                if (LTK.MaLoaiTietKiem == MaLTK)
                    return LTK.KyHan;

                //debug += "\n" + LTK.TenLoaiTietKiem + "a";
            }
            return 0;
        }
        private decimal search_STG(string MaLTK)
        {
            ObservableCollection<LOAITIETKIEM> List_LTK = new ObservableCollection<LOAITIETKIEM>(DataProvider.Ins.DB.LOAITIETKIEMs);

            foreach (LOAITIETKIEM LTK in List_LTK)
            {
                if (LTK.MaLoaiTietKiem == MaLTK)
                    return LTK.SoTienDuocRut;

                //debug += "\n" + LTK.TenLoaiTietKiem + "a";
            }
            return 0;
        }
        #endregion
        #region CheckValid
        private string CheckValid()
        {
            SOTIETKIEM stk = search_MaSo(MaSoTietKiem);
            string error = "";
            if (MaSoTietKiem == "" || MaSoTietKiem == null)
                error += "Sổ chưa được tạo mã sổ";

            if (int.Parse(SoTienGui) <= int.Parse(search_STG(stk.MaLoaiTietKiem).ToString()))
                error += "\n Số tiền gửi tối thiểu không đủ";
            if (NgayGui != NgayDaoHanKeTiep)
                error += "\n Không thể gửi hôm nay";
            return error;

        }

        private string Cal_NgayDaoHan(int days)
        {
            double d = Double.Parse(days.ToString());
            string date_NgayDaoHan = DateTime.Today.AddDays(d).ToString();
            date_NgayDaoHan = formatDate(date_NgayDaoHan);
            return date_NgayDaoHan;
        }
        private string GetCodeMPG()
        {
            ObservableCollection<PHIEUGUI> List_PG = new ObservableCollection<PHIEUGUI>(DataProvider.Ins.DB.PHIEUGUIs);
            int tmp = List_PG.Count();
            return "PG" + (tmp + 1).ToString();
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
        public ICommand ShowINFO { get; set; }
        public GuiTien_ViewModel()
        {

            NgayGui = formatDate(DateTime.Now.ToString());
            ShowINFO = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                SOTIETKIEM stk = search_MaSo(MaSoTietKiem);
                if (stk != null)
                {
                    TenKhachHang = stk.TenKhachHang;
                    NgayDaoHanKeTiep = stk.NgayDaoHanKeTiep.ToString();
                    TenLoaiLoaiTietKiem = search_TenLTK(stk.MaLoaiTietKiem);
                    SoCMND = stk.SoCMND;
                }
            });
            CheckCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                string error = CheckValid();
                if (error == "") System.Windows.MessageBox.Show("Thông tin sổ này hợp lệ! Đã có thể mở sổ");
                else
                    System.Windows.MessageBox.Show(error, "Thông tin không hợp lệ", MessageBoxButton.OK);
            });

            //Button Mo So 
            CheckoutCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                string error = CheckValid();
                if (error != "") System.Windows.MessageBox.Show(error, "Thông tin không hợp lệ", MessageBoxButton.OK);
                else
                {
                    System.Windows.MessageBox.Show("Đã tao phiếu gửi");
                    PHIEUGUI Phieugui = new PHIEUGUI();
                    Phieugui.MaPhieuGui = GetCodeMPG();
                    Phieugui.MaSoTietKiem = MaSoTietKiem;
                    Phieugui.NgayGui = DateTime.Parse(NgayGui);
                    Phieugui.SoTienGui = int.Parse(SoTienGui);
                    
                    DataProvider.Ins.DB.PHIEUGUIs.Add(Phieugui);
                    DataProvider.Ins.DB.SaveChanges();

                    var SotietKiem = DataProvider.Ins.DB.SOTIETKIEMs.Where(x => x.MaSoTietKiem == MaSoTietKiem).SingleOrDefault();
                    SotietKiem.SoDu += decimal.Parse(SoTienGui);
                    SotietKiem.NgayDaoHanKeTiep = DateTime.Parse(Cal_NgayDaoHan(search_KyHan(SotietKiem.MaLoaiTietKiem)));
                    DataProvider.Ins.DB.SaveChanges();
                }
            });

        }
    }
}
