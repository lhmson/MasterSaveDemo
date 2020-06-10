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
    /*
     Format của list BCDS để hiển thị lúc lấy từ database lên khác với format lúc tạo (0 khác 0.0000)
         */
    public class BaoCaoDoanhSo_ViewModel : BaseViewModel
    {
        #region Variables
        // date chosen for reporting
        private DateTime _SelectedDateReport;
        public DateTime SelectedDateReport
        {
            get => _SelectedDateReport;
            set { _SelectedDateReport = value; OnPropertyChanged(); }
        }
        // date chosen for displaying
        private DateTime _SelectedDateReportDisplay;
        public DateTime SelectedDateReportDisplay
        {
            get => _SelectedDateReportDisplay;
            set { _SelectedDateReportDisplay = value; OnPropertyChanged(); }
        }
        // list of BAOCAODOANHSOes in the date chosen
        private ObservableCollection<BAOCAODOANHSO> _ListBaoCaoDoanhSo;
        public ObservableCollection<BAOCAODOANHSO> ListBaoCaoDoanhSo
        {
            get => _ListBaoCaoDoanhSo;
            set { _ListBaoCaoDoanhSo = value; OnPropertyChanged(); }
        }
        // 
        private ObservableCollection<DateTime> _ListNgayBaoCao;
        public ObservableCollection<DateTime> ListNgayBaoCao
        {
            get => _ListNgayBaoCao;
            set { _ListNgayBaoCao = value; OnPropertyChanged(); }
        }
        // list of BAOCAODOANHSOes in the date chosen for display
        private ObservableCollection<BaoCaoDS> _ListBaoCaoDisplay;
        public ObservableCollection<BaoCaoDS> ListBaoCaoDisplay
        {
            get => _ListBaoCaoDisplay;
            set { _ListBaoCaoDisplay = value; OnPropertyChanged(); }
        }
        // list Loaitietkiem
        private ObservableCollection<LOAITIETKIEM> _ListLTK;
        public ObservableCollection<LOAITIETKIEM> ListLTK
        {
            get => _ListLTK;
            set { _ListLTK = value; OnPropertyChanged(); }
        }
        // list Phieugui
        private ObservableCollection<PHIEUGUI> _ListPhieuGui;
        public ObservableCollection<PHIEUGUI> ListPhieuGui
        {
            get => _ListPhieuGui;
            set { _ListPhieuGui = value; OnPropertyChanged(); }
        }
        // list Phieurut
        private ObservableCollection<PHIEURUT> _ListPhieuRut;
        public ObservableCollection<PHIEURUT> ListPhieuRut
        {
            get => _ListPhieuRut;
            set { _ListPhieuRut = value; OnPropertyChanged(); }
        }
        // list Sotietkiem
        private ObservableCollection<SOTIETKIEM> _ListSTK;
        public ObservableCollection<SOTIETKIEM> ListSTK
        {
            get => _ListSTK;
            set { _ListSTK = value; OnPropertyChanged(); }
        }
        //TenLoaiTietKiem
        private string _TenLoaiTietKiem;
        public string TenLoaiTietKiem
        {
            get => _TenLoaiTietKiem;
            set { _TenLoaiTietKiem = value; OnPropertyChanged(); }
        }
        //So Thu Tu  
        private int _SoThuTu;
        public int SoThuTu
        {
            get => _SoThuTu;
            set { _SoThuTu = value; OnPropertyChanged(); }
        }
        // tong thu
        private decimal _TongThu;
        public decimal TongThu
        {
            get => _TongThu;
            set { _TongThu = value; OnPropertyChanged(); }
        }
        // tong chi
        private decimal _TongChi;
        public decimal TongChi
        {
            get => _TongChi;
            set { _TongChi = value; OnPropertyChanged(); }
        }
        // chenh lech thu chi
        private decimal _ChenhLech;
        public decimal ChenhLech
        {
            get => _ChenhLech;
            set { _ChenhLech = value; OnPropertyChanged(); }
        }
        //
        private string _FormattedDate;
        public string FormattedDate
        {
            get => _FormattedDate;
            set { _FormattedDate = value; OnPropertyChanged(); }
        }
        #endregion

        #region Function
        private void LoadData()
        {
            var listLTK_Using = DataProvider.Ins.DB.LOAITIETKIEMs.Where(x => x.DangSuDung != 0);
            ListLTK = new ObservableCollection<LOAITIETKIEM>(listLTK_Using);

            ListSTK = new ObservableCollection<SOTIETKIEM>(DataProvider.Ins.DB.SOTIETKIEMs);
            ListPhieuGui = new ObservableCollection<PHIEUGUI>(DataProvider.Ins.DB.PHIEUGUIs);
            ListPhieuRut = new ObservableCollection<PHIEURUT>(DataProvider.Ins.DB.PHIEURUTs);
            ListBaoCaoDoanhSo = new ObservableCollection<BAOCAODOANHSO>(DataProvider.Ins.DB.BAOCAODOANHSOes);
            ListBaoCaoDisplay = new ObservableCollection<BaoCaoDS>();

            var listNgay = (from bc in ListBaoCaoDoanhSo
                            orderby bc.NgayDoanhSo descending
                            select bc.NgayDoanhSo).Distinct();
            ListNgayBaoCao = new ObservableCollection<DateTime>(listNgay);

            SelectedDateReport = DateTime.Now;
        }

        private BAOCAODOANHSO FindBaoCao(LOAITIETKIEM ltk)
        {
            var baoCao = (from bc in ListBaoCaoDoanhSo
                         where bc.MaLoaiTietKiem == ltk.MaLoaiTietKiem && bc.NgayDoanhSo == SelectedDateReport
                         select bc).SingleOrDefault();
            return baoCao;
        }
        private string Create_MaBCDS(int stt)
        {
            string res = "BCDS";
            for (int i = 5; i <= 11 - stt.ToString().Length; i++)
                res += '0';
            res += stt.ToString();
            return res;
        }
        private void GetBaoCaoToDisplay(int i, LOAITIETKIEM ltk, BAOCAODOANHSO baoCao)
        {
            SoThuTu = i + 1;
            TenLoaiTietKiem = ltk.TenLoaiTietKiem;
            TongThu += baoCao.TongThu;
            TongChi += baoCao.TongChi;
            ChenhLech = baoCao.ChechLech;

            BaoCaoDS baoCaoDisplay = new BaoCaoDS(SoThuTu, TenLoaiTietKiem, TongThu, TongChi, ChenhLech);
            ListBaoCaoDisplay.Add(baoCaoDisplay);
        }
        private BAOCAODOANHSO CreateBaoCao(int i, LOAITIETKIEM ltk)
        {
            SoThuTu = i + 1;
            TenLoaiTietKiem = ltk.TenLoaiTietKiem;

            int index = ListBaoCaoDoanhSo.Count() + 1;
            string maBaoCao = Create_MaBCDS(index);
            string maLoai = ltk.MaLoaiTietKiem;

            var listThu = from phieuGui in ListPhieuGui
                          join stk in ListSTK on phieuGui.MaSoTietKiem equals stk.MaSoTietKiem
                          where (stk.MaLoaiTietKiem == ltk.MaLoaiTietKiem && phieuGui.NgayGui == SelectedDateReport)
                          select phieuGui.SoTienGui;
            TongThu += listThu.Sum();

            var listChi = from phieuRut in ListPhieuRut
                          join stk in ListSTK on phieuRut.MaSoTietKiem equals stk.MaSoTietKiem
                          where (stk.MaLoaiTietKiem == ltk.MaLoaiTietKiem && phieuRut.NgayRut == SelectedDateReport)
                          select phieuRut.SoTienRut;
            TongChi += listChi.Sum();

            ChenhLech = (TongThu - TongChi);

            BaoCaoDS baoCaoDisplay = new BaoCaoDS(SoThuTu, TenLoaiTietKiem, TongThu, TongChi, ChenhLech);
            ListBaoCaoDisplay.Add(baoCaoDisplay);

            BAOCAODOANHSO baoCao = new BAOCAODOANHSO(maBaoCao, SelectedDateReport, maLoai, TongThu, TongChi, ChenhLech);
            return baoCao;
        }
        #endregion

        #region ICommand
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand CreateReportCommand { get; set; }

        #endregion

        public BaoCaoDoanhSo_ViewModel()
        {
            LoadData();

            SelectionChangedCommand = new RelayCommand<object>((p) => { return true; },
                (p) => {
                    ListBaoCaoDisplay.Clear();
                    SelectedDateReport = SelectedDateReportDisplay;
                    var listBaoCao = (from bc in ListBaoCaoDoanhSo
                                     where bc.NgayDoanhSo == SelectedDateReport
                                     select bc).ToList();
                    for(int i=0; i<listBaoCao.Count(); i++)
                    {
                        TongThu = TongChi = 0;
                        GetBaoCaoToDisplay(i, listBaoCao[i].LOAITIETKIEM, listBaoCao[i]);
                    }
                }
            );

            // button for finding report and create if ... not available
            CreateReportCommand = new RelayCommand<object>((p) => { return true; },
                (p) => {
                    bool isNew = false;
                    // clear all elements of ListBaoCaoDisplay to clear screen
                    ListBaoCaoDisplay.Clear();
                    
                    for (int i = 0; i < ListLTK.Count(); i++)
                    {
                        TongThu = TongChi = 0;
                        BAOCAODOANHSO baoCao = FindBaoCao(ListLTK[i]);

                        if(baoCao != null)
                        {
                            isNew = false;
                            GetBaoCaoToDisplay(i, ListLTK[i], baoCao);
                        }
                        else
                        {
                            isNew = true;
                            baoCao = CreateBaoCao(i, ListLTK[i]);
                            DataProvider.Ins.DB.BAOCAODOANHSOes.Add(baoCao);
                            DataProvider.Ins.DB.SaveChanges();

                            ListBaoCaoDoanhSo.Add(baoCao);
                        }
                    }
                    if (isNew)
                    {
                        if (ListNgayBaoCao.Count() == 0 || SelectedDateReport < ListNgayBaoCao[ListNgayBaoCao.Count() - 1])
                            ListNgayBaoCao.Add(SelectedDateReport);
                        for (int i = 0; i < ListNgayBaoCao.Count(); i++)
                        {
                            if (SelectedDateReport > ListNgayBaoCao[i])
                            {
                                ListNgayBaoCao.Insert(i, SelectedDateReport);
                                break;
                            }
                        }
                    }
                }

            );
        }
    }
}
