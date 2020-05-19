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
    public class MoSo_ViewModel : BaseViewModel
    {
        #region The sub functions by Sanh
        private string FormatDateTime(string date)
        {
            string res = "";
            for (int i = 0; i < date.Length; i++)
                if (date[i] == ' ') break;
             else
                res += date[i];

            return res;
        }

        private void resetCombobox_LoaiTietKiem()
        {
            //CbxTenLoaiTietKiem = "";
            SelectedTenLoaiTietKiem = "";
            TenLoaiTietKiem = new List<string>();
            foreach (LOAITIETKIEM LTK in List)
                TenLoaiTietKiem.Add(LTK.TenLoaiTietKiem);
        }

        private int analysis_CodeSTK(string code)
        {
            string res = "";
            for (int i = 3; i < code.Length; i++)
                res += code[i];
            return int.Parse(res);
        }

        private string create_CodeSTK(int stt)
        {
            string res = "STK";
            for (int i = 4; i <= 10 - stt.ToString().Length; i++)
                res += '0';
            res += stt.ToString();
            return res;
        }

        private string GetCodeSTK()
        {
            ObservableCollection<SOTIETKIEM> List_STK = new ObservableCollection<SOTIETKIEM>(DataProvider.Ins.DB.SOTIETKIEMs);

            int max = 0;
            foreach (SOTIETKIEM STK in List_STK)
                if (max < analysis_CodeSTK(STK.MaSoTietKiem))
                    max = analysis_CodeSTK(STK.MaSoTietKiem);
            max++;
            string res = create_CodeSTK(max);
            return res;
        }

        private int search_KyHan(string TenLTK)
        {
            ObservableCollection<LOAITIETKIEM> List_LTK = new ObservableCollection<LOAITIETKIEM>(DataProvider.Ins.DB.LOAITIETKIEMs);

            string debug = "";

            foreach (LOAITIETKIEM LTK in List_LTK)
            {
                if (LTK.TenLoaiTietKiem == TenLTK)  
                    return LTK.KyHan;

                //debug += "\n" + LTK.TenLoaiTietKiem + "a";
            }
            return 0;
        }
        private string search_MaLTK(string TenLTK)
        {
            ObservableCollection<LOAITIETKIEM> List_LTK = new ObservableCollection<LOAITIETKIEM>(DataProvider.Ins.DB.LOAITIETKIEMs);

            foreach (LOAITIETKIEM LTK in List_LTK)
            {
                if (LTK.TenLoaiTietKiem == TenLTK)
                    return LTK.MaLoaiTietKiem;

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
            if (CbxTenLoaiTietKiem == "" || CbxTenLoaiTietKiem == null)
                error += "\nSổ chưa chọn hình thức loại tiết kiệm";
            if (TenKhachHang == "" || TenKhachHang == null)
                error += "\nTên khách hàng chưa được nhập";
            if (CMND == "" || CMND == null)
                error += "\nCMND chưa được nhập";
            if (DiaChi == "" || DiaChi == null)
                error += "\nĐịa chỉ chưa được nhập";
            if (SoTienGuiBanDau == "" || SoTienGuiBanDau == null)
                error += "\nSố tiền gửi của khách hàng chưa được nhập";
            if (DonVi == "" || DonVi == null)
                error += "\nĐơn vị tiền tệ chưa được xác định";
            return error;

        }

        private string Cal_NgayDaoHan(int days)
        {
            double d = Double.Parse(days.ToString());
            string date_NgayDaoHan = DateTime.Today.AddDays(d).ToString();
            date_NgayDaoHan = FormatDateTime(date_NgayDaoHan);
            return date_NgayDaoHan;
        }

        #endregion
        private ObservableCollection<LOAITIETKIEM> _List;
        public ObservableCollection<LOAITIETKIEM> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        #region Variables
        private string _NgayMoSo;
        public  string NgayMoSo { get => _NgayMoSo; set { _NgayMoSo = value; OnPropertyChanged(); } }

        private string _NgayDaoHanKeTiep;
        public string NgayDaoHanKeTiep { get => _NgayDaoHanKeTiep; set { _NgayDaoHanKeTiep = value; OnPropertyChanged(); } }


        private List<string> _TenLoaiTietKiem;
        public List<string> TenLoaiTietKiem { get => _TenLoaiTietKiem; set { _TenLoaiTietKiem = value; OnPropertyChanged(); } }

        private string _SelectedTenLoaiTietKiem;
        public string SelectedTenLoaiTietKiem { get => _SelectedTenLoaiTietKiem; set 
            { 
                _SelectedTenLoaiTietKiem = value;
                OnPropertyChanged(); 
                if (SelectedTenLoaiTietKiem != null && SelectedTenLoaiTietKiem != "")
                {
                    NgayDaoHanKeTiep = Cal_NgayDaoHan(search_KyHan(SelectedTenLoaiTietKiem));
                }
            } }

        private string _CbxTenLoaiTietKiem;
        public string CbxTenLoaiTietKiem { get => _CbxTenLoaiTietKiem; set { _CbxTenLoaiTietKiem = value; OnPropertyChanged(); } }

        private string _MaSoTietKiem;
        public string MaSoTietKiem { get => _MaSoTietKiem; set { _MaSoTietKiem = value; OnPropertyChanged(); } }

        private string _TenKhachHang;
        public string TenKhachHang { get => _TenKhachHang; set { _TenKhachHang = value; OnPropertyChanged(); } }

        private string _CMND;
        public string CMND { get => _CMND; set { _CMND = value; OnPropertyChanged(); } }

        private string _DiaChi;
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }

        private string _SoTienGuiBanDau;
        public string SoTienGuiBanDau { get => _SoTienGuiBanDau; set { _SoTienGuiBanDau = value; OnPropertyChanged(); } }

        private string _DonVi;
        public string DonVi { get => _DonVi; set { _DonVi = value; OnPropertyChanged(); } }

        #endregion

        public ICommand CheckValidCommand { get; set; }
        public ICommand ResetLTKCommand { get; set; }
        public ICommand GetCodeSTKcommand { get; set; }
        public ICommand MoSoCommand { get; set; }

        public MoSo_ViewModel()
        {
            NgayDaoHanKeTiep = "";
            // Display List LOAITIETKIEM
            List = new ObservableCollection<LOAITIETKIEM>(DataProvider.Ins.DB.LOAITIETKIEMs);

            //Auto display content of NgayMoSo
            DateTime DateTimeNow = DateTime.Now;
            NgayMoSo = FormatDateTime(DateTimeNow.ToString());

            //Display combobox TenLoaiTietKiem
            resetCombobox_LoaiTietKiem();

            //Event Click of Button Reset Combobox LoaiTietKiem
            ResetLTKCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                CbxTenLoaiTietKiem = "";
            });

            //Get code STK
            GetCodeSTKcommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                MaSoTietKiem = GetCodeSTK();
            });

            //Button Check Valid Infor 
            CheckValidCommand = new RelayCommand<object>((p) =>
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
            MoSoCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                string error = CheckValid();
                if (error != "") System.Windows.MessageBox.Show(error, "Thông tin không hợp lệ", MessageBoxButton.OK);
                else
                {
                    System.Windows.MessageBox.Show("Đã tao một sổ mới");
                    SOTIETKIEM SoTietKiem = new SOTIETKIEM();
                    SoTietKiem.MaSoTietKiem = MaSoTietKiem;
                    SoTietKiem.SoCMND = CMND;
                    SoTietKiem.DiaChi = DiaChi;
                    SoTietKiem.TenKhachHang = TenKhachHang;
                    SoTietKiem.SoTienGuiBanDau = Decimal.Parse(SoTienGuiBanDau);
                    SoTietKiem.NgayMoSo = DateTime.Parse(NgayMoSo);
                    SoTietKiem.SoDu = Decimal.Parse(SoTienGuiBanDau);
                    SoTietKiem.NgayDongSo = new DateTime(2030, 1, 1);
                    SoTietKiem.NgayDaoHanKeTiep = DateTime.Parse(NgayDaoHanKeTiep);
                    SoTietKiem.MaLoaiTietKiem = search_MaLTK(SelectedTenLoaiTietKiem);
                    SoTietKiem.LaiSuatApDung = 0.002;

                    DataProvider.Ins.DB.SOTIETKIEMs.Add(SoTietKiem);
                    DataProvider.Ins.DB.SaveChanges();
                }
            });

        }
    }
}
