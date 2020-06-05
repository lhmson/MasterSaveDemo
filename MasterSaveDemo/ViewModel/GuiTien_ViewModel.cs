using MasterSaveDemo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
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
                    return LTK.QuyDinhSoTienRut;

                //debug += "\n" + LTK.TenLoaiTietKiem + "a";
            }
            return 0;
        }
        #endregion
        #region CheckValid
        private string CheckValid()
        {
           try
           {
                SOTIETKIEM stk = search_MaSo(MaSoTietKiem);
                if (MaSoTietKiem == "" || MaSoTietKiem == null)
                    Notify += "Sổ chưa được tạo mã sổ";
                if (decimal.Parse(SoTienGui) <= decimal.Parse(search_STG(stk.MaLoaiTietKiem).ToString()))
                    Notify += "\n Số tiền gửi tối thiểu không đủ";
                if (NgayGui != NgayDaoHanKeTiep)
                    Notify += "\n Không thể gửi hôm nay";
                return Notify;
            }
            catch (Exception ex)
            {
                return ".....";
            }

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
        private string _Notify;

        public string Notify
        {
            get { return _Notify; }
            set { _Notify = value; OnPropertyChanged(); }
        }

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

        private string _TenLoaiTietKiem;
        public string TenLoaiTietKiem
        {
            get { return _TenLoaiTietKiem; }
            set { _TenLoaiTietKiem = value; OnPropertyChanged(); }
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
                    NgayDaoHanKeTiep = formatDate(stk.NgayDaoHanKeTiep.ToString());
                    TenLoaiTietKiem = search_TenLTK(stk.MaLoaiTietKiem);
                    SoCMND = stk.SoCMND;
                }
            });
            CheckCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                Notify = "";
                Notify = CheckValid();
                if (Notify == "") Notify+="\nThông tin này hợp lệ! Đã có thể tạo phiếu";
                else
                    Notify+="\nThông tin không hợp lệ";
            });

            //Button Mo So 
            CheckoutCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                Notify = "";
                Notify = CheckValid();
                if (Notify != "") Notify+= "\nThông tin không hợp lệ";
                else
                {
                    Notify+="\nĐã tao phiếu gửi";
                    PHIEUGUI Phieugui = new PHIEUGUI()
                    {
                        MaPhieuGui = GetCodeMPG(),
                        MaSoTietKiem = MaSoTietKiem,
                        NgayGui = DateTime.Parse(NgayGui),
                        SoTienGui = int.Parse(SoTienGui),
                    };
                    //edit PhieuGui
                    DataProvider.Ins.DB.PHIEUGUIs.Add(Phieugui);
                    DataProvider.Ins.DB.SaveChanges();
                    //edit SoTietKiem
                    var SotietKiem = DataProvider.Ins.DB.SOTIETKIEMs.Where(x => x.MaSoTietKiem == MaSoTietKiem).SingleOrDefault();
                    SotietKiem.SoDu += decimal.Parse(SoTienGui);             
                    DataProvider.Ins.DB.SaveChanges();
                }
            });

        }
    }
}
