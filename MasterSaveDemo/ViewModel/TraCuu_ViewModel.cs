using MasterSaveDemo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MasterSaveDemo.Helper;
using System.Data;

namespace MasterSaveDemo.ViewModel
{
    public class TraCuu_ViewModel : BaseViewModel
    {
        #region The sub funtions by Sanh

        private void reset_Control()
        {
            TenKH = "";
            MaSTK = "";
            SelectedLTK = null;
            SelectedMucSoDu = null;
        }

        private void Search_Mode()
        {
            Content = "Tìm Kiếm";
            Visibility_Search = Visibility.Visible;
            Visibility_Edit = Visibility.Hidden;
        }

        private void Edit_Mode()
        {
            Content = "Xác nhận";
            Visibility_Search = Visibility.Hidden;
            Visibility_Edit = Visibility.Visible;
        }

        private void XetQuyenNhapLai()
        {
            Enable_NhapLaiVaoVon = false;
          
            if (LoginViewModel.TaiKhoanSuDung == null)
                return;

            // Tìm mã nhóm quyền của user
            int ma = LoginViewModel.TaiKhoanSuDung.MaNhom;

            ObservableCollection<PHANQUYEN> nhom = new ObservableCollection<PHANQUYEN>(DataProvider.Ins.DB.PHANQUYENs);

            foreach (var item in nhom)
                if (item.MaNhom == ma && item.MaChucNang == 9)
                    Enable_NhapLaiVaoVon = true;
        }

        private string Delete_ThapPhan(string number)
        {
            string res = "";
            for (int i = 0; i < number.Length; i++)
                if (number[i] == '.') break; 
            else
                res += number[i];
            return res;
        }

        #endregion

        #region Variables

        private bool QuyenNhapLai;
        private string _Content;
        public string Content { get => _Content; set { _Content = value; OnPropertyChanged(); } }

        private Visibility _Visibility_Edit;
        public Visibility Visibility_Edit { get => _Visibility_Edit; set { _Visibility_Edit = value; OnPropertyChanged(); } }

        private Visibility _Visibility_Search;
        public Visibility Visibility_Search { get => _Visibility_Search; set { _Visibility_Search = value; OnPropertyChanged(); } }

        private bool _Enable_NhapLaiVaoVon;
        public bool Enable_NhapLaiVaoVon { get => _Enable_NhapLaiVaoVon; set { _Enable_NhapLaiVaoVon = value; OnPropertyChanged(); } }

        private List<string> _LoaiTietKiem;
        public List<string> LoaiTietKiem { get => _LoaiTietKiem; set { _LoaiTietKiem = value; OnPropertyChanged(); } }

        private ObservableCollection<STKDuocTraCuu> _ListSoTietKiem;
        public ObservableCollection<STKDuocTraCuu> ListSoTietKiem { get => _ListSoTietKiem; set { _ListSoTietKiem = value; OnPropertyChanged(); } }

        private string _MaSTK;
        public string MaSTK { get => _MaSTK; set { _MaSTK = value; OnPropertyChanged(); } }

        private string _SelectedLTK;
        public string SelectedLTK { get => _SelectedLTK; set { _SelectedLTK = value; OnPropertyChanged(); } }

        private string _SelectedMucSoDu;
        public string SelectedMucSoDu { get => _SelectedMucSoDu; set { _SelectedMucSoDu = value; OnPropertyChanged(); } }

        private string _TenKH;
        public string TenKH { get => _TenKH; set { _TenKH = value; OnPropertyChanged(); } }

        private string _SoDu;
        public string SoDu { get => _SoDu; set { _SoDu = value; OnPropertyChanged(); } }

        private DateTime _NgayDaoHan;
        public DateTime NgayDaoHan { get => _NgayDaoHan; set { _NgayDaoHan = value; OnPropertyChanged(); } }

        private List<string> _MucSoDu;
        public List<string> MucSoDu { get => _MucSoDu; set { _MucSoDu = value; OnPropertyChanged(); } }

        private STKDuocTraCuu _SelectedSTK;
        public STKDuocTraCuu SelectedSTK { get => _SelectedSTK; set { _SelectedSTK = value; OnPropertyChanged(); } }
        #endregion

        public ICommand SeeAllCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand NhapLaiVaoVon_All { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public TraCuu_ViewModel()
        {
            Search_Mode();
            XetQuyenNhapLai();
            NgayDaoHan = DateTime.Now;
            // Init Combobox LoaiTietKiem
            ObservableCollection<LOAITIETKIEM> _List = new ObservableCollection<LOAITIETKIEM>(DataProvider.Ins.DB.LOAITIETKIEMs);
            LoaiTietKiem = new List<string>();
            foreach (LOAITIETKIEM LTK in _List)
                LoaiTietKiem.Add(LTK.TenLoaiTietKiem);
            LoaiTietKiem.Add("Tất cả");

            // Combobox MucSoDu
            MucSoDu = new List<string>();
            MucSoDu.Add("Tất cả");
            MucSoDu.Add("0 VNĐ");
            MucSoDu.Add("Dưới 5.000.000 VNĐ");
            MucSoDu.Add("5.000.000 - 100.000.000 VNĐ");
            MucSoDu.Add("Từ 100.000.000 - 1.000.000.000 VNĐ");
            MucSoDu.Add("Trên 1.000.000.000 VNĐ");

            //Button Xem tất cả
            SeeAllCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ObservableCollection<SOTIETKIEM> STK_TABLE = new ObservableCollection<SOTIETKIEM>(DataProvider.Ins.DB.SOTIETKIEMs);
                ObservableCollection<LOAITIETKIEM> LTK_TABLE = new ObservableCollection<LOAITIETKIEM>(DataProvider.Ins.DB.LOAITIETKIEMs);

                var results = from stk in STK_TABLE
                              join ltk in LTK_TABLE on stk.MaLoaiTietKiem equals ltk.MaLoaiTietKiem
                              select new STKDuocTraCuu(stk.MaSoTietKiem, ltk.TenLoaiTietKiem, stk.TenKhachHang,
                              Delete_ThapPhan(stk.SoDu.ToString()), stk.NgayDaoHanKeTiep.ToString("dd-MM-yyyy"), stk.LaiSuatApDung.ToString());
                ListSoTietKiem = new ObservableCollection<STKDuocTraCuu>(results);
                #region fill id 
                int count = 1;
                for (int i = 0; i < ListSoTietKiem.Count(); i++)
                {
                    ListSoTietKiem[i].STT = count.ToString();
                    count++;
                }
                #endregion
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedSTK != null) return true;
                return false;
            }, (p) =>
            {
                Edit_Mode();
                
            });

            CancelCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                reset_Control();
                if (Visibility_Edit == Visibility.Visible)
                    Search_Mode();

            });
            //Button Search SO TIET KIEM
            SearchCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ObservableCollection<SOTIETKIEM> STK_TABLE = new ObservableCollection<SOTIETKIEM>(DataProvider.Ins.DB.SOTIETKIEMs);
                ObservableCollection<LOAITIETKIEM> LTK_TABLE = new ObservableCollection<LOAITIETKIEM>(DataProvider.Ins.DB.LOAITIETKIEMs);

                if (MaSTK == null) MaSTK = "";
                if (TenKH == null) TenKH = "";

                decimal min = 0;
                decimal max = -1;

                if (SelectedMucSoDu == "0 VNĐ") max = 0;
                else if (SelectedMucSoDu == "Dưới 5.000.000 VNĐ") max = 5000000;
                else if (SelectedMucSoDu == "5.000.000 - 100.000.000 VNĐ")
                {
                    min = 5000000;
                    max = 100000000;
                }
                else if (SelectedMucSoDu == "Từ 100.000.000 - 1.000.000.000 VNĐ")
                {
                    min = 100000000;
                    max = 1000000000;
                }
                else
                {
                    min = 1000000000;
                    max = -1;
                }

                if (SelectedMucSoDu == "Tất cả" || SelectedMucSoDu == null)
                {
                    min = 0;
                    max = -1;
                }

                var results = from stk in STK_TABLE
                              join ltk in LTK_TABLE on stk.MaLoaiTietKiem equals ltk.MaLoaiTietKiem
                              where (stk.MaSoTietKiem == MaSTK || MaSTK == "") && (stk.TenKhachHang.Contains(TenKH))
                                    && (String.IsNullOrEmpty(SoDu) || Delete_ThapPhan(stk.SoDu.ToString()) == SoDu)
                                    && (String.IsNullOrEmpty(SelectedLTK) || ltk.TenLoaiTietKiem == SelectedLTK || SelectedLTK == "Tất cả")
                                    && (stk.SoDu >= min && (stk.SoDu <= max || max == -1))
                              select new STKDuocTraCuu(stk.MaSoTietKiem, ltk.TenLoaiTietKiem, stk.TenKhachHang, Delete_ThapPhan(stk.SoDu.ToString()), stk.NgayDaoHanKeTiep.ToString("dd-MM-yyyy"), stk.LaiSuatApDung.ToString());

                ListSoTietKiem = new ObservableCollection<STKDuocTraCuu>(results);
                #region fill id 
                int count = 1;
                for (int i = 0; i < ListSoTietKiem.Count(); i++)
                {
                    ListSoTietKiem[i].STT = count.ToString();
                    count++;
                }
                #endregion
            });

            //Phan nay do Thang them vao
            NhapLaiVaoVon_All = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                if (DateTime.Now.TimeOfDay.TotalMinutes >= 13 * 60 + 30)
                {
                    NhapLaiVaoVon.Ins.AllToday();
                }
                else
                {
                    MessageBox.Show("Chưa tới thời điểm nhập lãi vào vốn!");
                }
            });
        }
    }
}
