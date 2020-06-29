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
using MessageBox = System.Windows.Forms.MessageBox;

namespace MasterSaveDemo.ViewModel
{
    public class MoSo_ViewModel : BaseViewModel
    {

        #region Singleton
        private static MoSo_ViewModel _ins;
        public static MoSo_ViewModel Ins
        {
            get
            {
                if (_ins == null) _ins = new MoSo_ViewModel();
                return _ins;
            }
            set
            {
                _ins = value;
            }
        }

        public void reset_changepage()
        {
            MaSoTietKiem = "";
            resetCombobox_LoaiTietKiem();
            TenKhachHang = "";
            CMND = "";
            DiaChi = "";
            NgayDaoHanKeTiep = "";
            SoTienGuiBanDau = "";
        }
        #endregion
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

        private double search_LaiSuat(string MaLTK)
        {
            double ls = 0;
            ObservableCollection<LOAITIETKIEM> List_MaLTK = new ObservableCollection<LOAITIETKIEM>(DataProvider.Ins.DB.LOAITIETKIEMs);
            foreach (LOAITIETKIEM LTK in List_MaLTK)
                if (LTK.MaLoaiTietKiem == MaLTK)
                    return LTK.LaiSuat;
            return ls;
        }
        #endregion
        #region CheckValid
        private string CheckValid()
        {
            string error = "";
            ObservableCollection<THAMSO> listThamSo = new ObservableCollection<THAMSO>(DataProvider.Ins.DB.THAMSOes);

            if (SoTienGuiBanDau == "" || SoTienGuiBanDau == null)
                error += "\nSố tiền gửi của khách hàng chưa được nhập";
            else
            {
                foreach (var item in listThamSo)
                {
                    if (item.TenThamSo == "SoTienGuiToiThieu")
                    {
                        if (item.GiaTri > int.Parse(SoTienGuiBanDau))
                        {                          
                            error += "Số tiền gửi ban đầu phải lớn hơn hoặc bằng " + item.GiaTri.ToString() + "\n";
                        }
                    }
                }
            }

            
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

        private bool _DialogOpen;
        public bool DialogOpen { get => _DialogOpen; set { _DialogOpen = value; OnPropertyChanged(); } }

        private string _ThongBao;
        public string ThongBao { get => _ThongBao; set { _ThongBao = value; OnPropertyChanged(); } }

        private bool isMoSo = false;
        private bool _Enable_KiemTraHopLe;
        public bool Enable_KiemTraHopLe { get => _Enable_KiemTraHopLe; set { _Enable_KiemTraHopLe = value; OnPropertyChanged(); } }

        private bool _Enable_ThucHienGiaoDich;
        public bool Enable_ThucHienGiaoDich { get => _Enable_ThucHienGiaoDich; set { _Enable_ThucHienGiaoDich = value; OnPropertyChanged(); } }

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

        #endregion

        public ICommand CheckValidCommand { get; set; }
        public ICommand ResetLTKCommand { get; set; }
        public ICommand GetCodeSTKcommand { get; set; }
        public ICommand MoSoCommand { get; set; }
        public ICommand TenKH_TextChangedCommand { get; set; }
        public ICommand DiaChi_TextChangedCommand { get; set; }
        public ICommand SoTienGui_TextChangedCommand { get; set; }
        public ICommand LTK_SelectionChangedCommand { get; set; }
        public ICommand CMND_TextChangedCommand { get; set; }
        public ICommand DialogOK { get; set; }

        // Test change page
        public ICommand LostFocusPageCommand { get; set; }

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

            //DialogHost
            DialogOK = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                DialogOpen = false;
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
                if (error == "")
                {
                    //System.Windows.MessageBox.Show("Thông tin sổ này hợp lệ! Đã có thể mở sổ");
                    DialogOpen = true;
                    ThongBao = "Thông tin sổ này hợp lệ! Đã có thể mở sổ";
                    isMoSo = true;
                }
                else
                    System.Windows.MessageBox.Show(error, "Thông tin không hợp lệ", MessageBoxButton.OK); 
            });

            //Button Mo So 
            MoSoCommand = new RelayCommand<object>((p) =>
            {
                return isMoSo;
            }, (p) =>
            {
                string error = CheckValid();
                if (error != "") System.Windows.MessageBox.Show(error, "Thông tin không hợp lệ", MessageBoxButton.OK);
                else
                {
                    //System.Windows.MessageBox.Show("Đã tao một sổ mới");
                    DialogOpen = true;
                    ThongBao = "Đã tao một sổ mới";
                    SOTIETKIEM SoTietKiem = new SOTIETKIEM();
                    SoTietKiem.MaSoTietKiem = MaSoTietKiem;
                    SoTietKiem.SoCMND = CMND;
                    SoTietKiem.DiaChi = DiaChi;
                    SoTietKiem.TenKhachHang = TenKhachHang;
                    SoTietKiem.SoTienGuiBanDau = Decimal.Parse(SoTienGuiBanDau);
                    SoTietKiem.NgayMoSo = DateTime.Parse(NgayMoSo);
                    SoTietKiem.SoDu = Decimal.Parse(SoTienGuiBanDau);
                    //SoTietKiem.NgayDongSo = new DateTime(2030, 1, 1);
                    SoTietKiem.NgayDaoHanKeTiep = DateTime.Parse(NgayDaoHanKeTiep);
                    SoTietKiem.MaLoaiTietKiem = search_MaLTK(SelectedTenLoaiTietKiem);
                    SoTietKiem.LaiSuatApDung = search_LaiSuat(search_MaLTK(SelectedTenLoaiTietKiem));
                    DataProvider.Ins.DB.SOTIETKIEMs.Add(SoTietKiem);
                    DataProvider.Ins.DB.SaveChanges();
                }
            });

            LostFocusPageCommand = new RelayCommand<Page>((p) => { return true; }, (p) => {
            });

            #region TextChanged Events
            TenKH_TextChangedCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                isMoSo = false;
            });

            DiaChi_TextChangedCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                isMoSo = false;
            });

            SoTienGui_TextChangedCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                isMoSo = false;
            });

            LTK_SelectionChangedCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                isMoSo = false;
            });

            CMND_TextChangedCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                isMoSo = false;
            });
            #endregion
        }
    }
}
