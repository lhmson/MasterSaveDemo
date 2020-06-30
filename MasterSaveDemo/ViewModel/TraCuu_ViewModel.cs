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
using System.Windows.Documents;
using MaterialDesignColors;

namespace MasterSaveDemo.ViewModel
{
    public class TraCuu_ViewModel : BaseViewModel
    {
        #region The sub funtions by Sanh
        private void Confirm_STK()
        {
            Visibility_TenKH = Visibility.Hidden;
            Error_KH = "";
            if (TenKHSua == null || TenKHSua == "" || SelectedSTK == null)
            {
                //MessageBox.Show("Tên khách hàng chưa được nhập");
                Visibility_TenKH = Visibility.Visible;
                Error_KH = "Tên khách hàng chưa được nhập";
                return;
            }
            
            SOTIETKIEM STK = new SOTIETKIEM();

            ObservableCollection<SOTIETKIEM> list_STK = new ObservableCollection<SOTIETKIEM>(DataProvider.Ins.DB.SOTIETKIEMs);

            foreach (var item in list_STK)
                if (item.MaSoTietKiem == SelectedSTK.Ma)
                    STK = item;

            DataProvider.Ins.DB.SOTIETKIEMs.Remove(STK);

            STK.TenKhachHang = TenKHSua;
            DataProvider.Ins.DB.SOTIETKIEMs.Add(STK);
            //MessageBox.Show("Chỉnh sửa sổ tiết kiệm thành công");
            DialogOpen = true;
            ThongBao = "Chỉnh sửa sổ tiết kiệm thành công";


            STKDuocTraCuu STK_New = SelectedSTK;
            STK_New.TenKH = TenKHSua;
            for (int i = 0; i < ListSoTietKiem.Count(); i++)
                if (ListSoTietKiem[i].Ma == SelectedSTK.Ma)
                {
                    ListSoTietKiem.RemoveAt(i);
                    ListSoTietKiem.Insert(i, STK_New);
                    break;
                }
        }

        private void Search_STK()
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
        }
        private void reset_Control()
        {
            TenKH = "";
            MaSTK = "";
            SelectedLTK = null;
            SelectedMucSoDu = null;
            Visibility_TenKH = Visibility.Hidden;
            Error_KH = "";
        }

        private void Search_Mode()
        {
            Content = "Tìm Kiếm";
            Enable_NhapLaiVaoVon = QuyenNhapLai;
            Visibility_Search = Visibility.Visible;
            Visibility_Edit = Visibility.Hidden;
            Visibility_TenKH = Visibility.Hidden;
            Error_KH = "";
        }

        private void Edit_Mode()
        {
            Content = "Xác nhận";
            Enable_NhapLaiVaoVon = false;
            Visibility_Search = Visibility.Hidden;
            Visibility_Edit = Visibility.Visible;
            TenKHSua = SelectedSTK.TenKH;
            Visibility_TenKH = Visibility.Hidden;
            Error_KH = "";
        }

        private void XetQuyenNhapLai()
        {
            Enable_NhapLaiVaoVon = false;

            QuyenNhapLai = false;

            if (LoginViewModel.TaiKhoanSuDung == null)
                return;

            // Tìm mã nhóm quyền của user
            int ma = LoginViewModel.TaiKhoanSuDung.MaNhom;

            ObservableCollection<PHANQUYEN> nhom = new ObservableCollection<PHANQUYEN>(DataProvider.Ins.DB.PHANQUYENs);

            foreach (var item in nhom)
                if (item.MaNhom == ma && item.MaChucNang == 9)
                {
                    Enable_NhapLaiVaoVon = true;
                    QuyenNhapLai = true;
                }
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

        private string _Error_KH;
        public string Error_KH { get => _Error_KH; set { _Error_KH = value; OnPropertyChanged(); } }

        private Visibility _Visibility_TenKH;
        public Visibility Visibility_TenKH { get => _Visibility_TenKH; set { _Visibility_TenKH = value; OnPropertyChanged(); } }

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

        private string _TenKHSua;
        public string TenKHSua { get => _TenKHSua; set { _TenKHSua = value; OnPropertyChanged(); } }

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

        #region Variables cua Thang
        private string _ThongBao;
        public string ThongBao
        {
            get { return _ThongBao; }
            set { _ThongBao = value; OnPropertyChanged(); }
        }

        private bool _DialogOpen;
        public bool DialogOpen
        {
            get { return _DialogOpen; }
            set { _DialogOpen = value; OnPropertyChanged(); }
        }
        #endregion

        public ICommand SeeAllCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand NhapLaiVaoVon_All { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        //lenh ben duoi do Thang them vao
        public ICommand DialogOK { get; set; }


        public TraCuu_ViewModel()
        {
            Visibility_TenKH = Visibility.Hidden;
            Error_KH = "";

            Search_Mode();
            XetQuyenNhapLai();
            NgayDaoHan = DateTime.Now;
            // Init Combobox LoaiTietKiem
            ObservableCollection<LOAITIETKIEM> _List = new ObservableCollection<LOAITIETKIEM>(DataProvider.Ins.DB.LOAITIETKIEMs);
            LoaiTietKiem = new List<string>();
            LoaiTietKiem.Add("Tất cả");
            foreach (LOAITIETKIEM LTK in _List)
                LoaiTietKiem.Add(LTK.TenLoaiTietKiem);

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
                Visibility_TenKH = Visibility.Hidden;
                Error_KH = "";
                Visibility_Edit = Visibility.Hidden;
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
                if (Content == "Tìm Kiếm") Search_STK();
                else
                {
                    Confirm_STK();
                    if (Visibility_TenKH == Visibility.Hidden)
                    Search_Mode();
                }
            });

            //Phan nay do Thang them vao
            NhapLaiVaoVon_All = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                if (DateTime.Now.TimeOfDay.TotalMinutes >= 8 * 60 + 30)
                {
                    List<string>DS_STK =  NhapLaiVaoVon.Ins.AllToday();
                    ListSoTietKiem = new ObservableCollection<STKDuocTraCuu>();
                    if (DS_STK.Count > 0)
                    {
                        ObservableCollection<SOTIETKIEM> STK_TABLE = new ObservableCollection<SOTIETKIEM>(DataProvider.Ins.DB.SOTIETKIEMs);
                        ObservableCollection<LOAITIETKIEM> LTK_TABLE = new ObservableCollection<LOAITIETKIEM>(DataProvider.Ins.DB.LOAITIETKIEMs);
                        foreach (string mstk in DS_STK)
                        {
                            SOTIETKIEM stk = STK_TABLE.Where(x => x.MaSoTietKiem == mstk).Single();
                            LOAITIETKIEM ltk = LTK_TABLE.Where(x => x.MaLoaiTietKiem == stk.MaLoaiTietKiem).Single();
                            STKDuocTraCuu temp = new STKDuocTraCuu(stk.MaSoTietKiem, ltk.TenLoaiTietKiem, stk.TenKhachHang, Delete_ThapPhan(stk.SoDu.ToString()), stk.NgayDaoHanKeTiep.ToString("dd-MM-yyyy"), stk.LaiSuatApDung.ToString());
                            ListSoTietKiem.Add(temp);
                        }
                        int count = 1;
                        for (int i = 0; i < ListSoTietKiem.Count(); i++)
                        {
                            ListSoTietKiem[i].STT = count.ToString();
                            count++;
                        }
                        DialogOpen = true;
                        ThongBao = "Đã nhập lãi vào vốn cho " + DS_STK.Count().ToString() + " sổ tiết kiệm.";
                    }
                    else
                    {
                        DialogOpen = true;
                        ThongBao = "Không có sổ tiết kiệm nào cần nhập lãi hôm nay.";
                    }
                }
                else
                {
                    DialogOpen = true;
                    ThongBao = "Chưa tới giờ nhập lãi.";
                }
            });
            DialogOK = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                DialogOpen = false;
            });
            ///Khởi tạo xem toàn bộ sổ lần đầu tiên 
            SeeAllCommand.Execute(new object());
        }
    }
}
