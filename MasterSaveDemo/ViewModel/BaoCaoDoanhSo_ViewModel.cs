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
    public class BaoCaoDoanhSo_ViewModel : BaseViewModel
    {
        #region Variables
        // date chosen for reporting
        private DateTime _DateReport;
        public DateTime DateReport
        {
            get => _DateReport;
            set { _DateReport = value; OnPropertyChanged(); }
        }
        // day chosen for reporting
        private int _SelectedDay;
        public int SelectedDay
        {
            get => _SelectedDay;
            set { _SelectedDay = value; OnPropertyChanged(); }
        }
        // month chosen for reporting
        private int _SelectedMonth;
        public int SelectedMonth
        {
            get => _SelectedMonth;
            set { _SelectedMonth = value; OnPropertyChanged(); }
        }
        // year chosen for reporting
        private int _SelectedYear;
        public int SelectedYear
        {
            get => _SelectedYear;
            set { _SelectedYear = value; OnPropertyChanged(); }
        }
        // list of BAOCAODOANHSOes in the date chosen
        private ObservableCollection<BAOCAODOANHSO> _ListBaoCaoDoanhSo;
        public ObservableCollection<BAOCAODOANHSO> ListBaoCaoDoanhSo
        {
            get => _ListBaoCaoDoanhSo;
            set { _ListBaoCaoDoanhSo = value; OnPropertyChanged(); }
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
        private string _TongThu;
        public string TongThu
        {
            get => _TongThu;
            set { _TongThu = value; OnPropertyChanged(); }
        }
        // tong chi
        private string _TongChi;
        public string TongChi
        {
            get => _TongChi;
            set { _TongChi = value; OnPropertyChanged(); }
        }
        // chenh lech thu chi
        private string _ChechLech;
        public string ChechLech
        {
            get => _ChechLech;
            set { _ChechLech = value; OnPropertyChanged(); }
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
            ListLTK = new ObservableCollection<LOAITIETKIEM>(DataProvider.Ins.DB.LOAITIETKIEMs);
        }
        private bool CheckDateReport(DateTime date)
        {
            if (date.Day == SelectedDay && date.Month == SelectedMonth && date.Year == SelectedYear)
                return true;
            return false;
        }
        private BAOCAODOANHSO FindBaoCao( LOAITIETKIEM loaiTietKiem)
        {
            foreach( var baoCao in ListBaoCaoDoanhSo)
            {
                if (baoCao.MaLoaiTietKiem == loaiTietKiem.MaLoaiTietKiem && CheckDateReport(baoCao.NgayDoanhSo))
                    return baoCao;
            }
            return null;
        }

        #endregion

        #region ICommand
        public ICommand SelectedDateChangedCommand { get; set; }
        public ICommand CreateReportCommand { get; set; }

        #endregion

        public BaoCaoDoanhSo_ViewModel()
        {
            LoadData();

            // store day, month, year when the report date is chosen
            SelectedDateChangedCommand = new RelayCommand<object>((p) => { return true; },
                (p) => {
                    SelectedDay = DateReport.Day;
                    SelectedMonth = DateReport.Month;
                    SelectedYear = DateReport.Year;

                    FormattedDate = DateReport.ToString("dd/MM/yyyy");
                    //MessageBox.Show(DateReport.ToString("dd/MM/yyyy"));
                }
            );

            // button create report
            CreateReportCommand = new RelayCommand<object>((p) => { return true; },
                (p) => {
                    foreach( var maLoaiTietKiem in ListLTK)
                    {
                        BAOCAODOANHSO baoCaoDS = FindBaoCao(maLoaiTietKiem);
                        if( baoCaoDS != null)
                        {
                            ListBaoCaoDoanhSo.Add(baoCaoDS);
                        }
                        else
                        {

                        }
                    }
                }
            );
        }
    }
}
