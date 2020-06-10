using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using MasterSaveDemo.Model;
using System.Windows.Input;

namespace MasterSaveDemo.ViewModel
{
    public class BaoCaoMoDong_ViewModel : BaseViewModel
    {
        #region Variables
        //--------------
        private List<string> _List_LTK;

        public List<string> List_LTK
        {
            get { return _List_LTK; }
            set { _List_LTK = value; OnPropertyChanged(); }
        }
        //--------------
        private string _Selected_LTK;

        public string Selected_LTK
        {
            get { return _Selected_LTK; }
            set { _Selected_LTK = value; OnPropertyChanged(); }
        }
        //--------------
        private string _LoaiTietKiem;

        public string LoaiTietKiem
        {
            get { return _LoaiTietKiem; }
            set { _LoaiTietKiem = value; OnPropertyChanged(); }
        }
        //------------
        private string _Selected_Thang;

        public string Selected_Thang
        {
            get { return _Selected_Thang; }
            set { _Selected_Thang = value; OnPropertyChanged(); }
        }
        //--------------
        private List<string> _List_Thang;

        public List<string> List_Thang
        {
            get { return _List_Thang; }
            set { _List_Thang = value; OnPropertyChanged(); }
        }
        //--------------
        private List<string> _List_Nam;

        public List<string> List_Nam
        {
            get { return _List_Nam; }
            set { _List_Nam = value; OnPropertyChanged(); }
        }
        //-----------------
        private string _Selected_Nam;

        public string Selected_Nam
        {
            get { return _Selected_Nam; }
            set { _Selected_Nam = value; OnPropertyChanged(); }
        }
        //----------------
        private List<ListBaoCaoDongMo> _ListBaoCaoDM;

        public List<ListBaoCaoDongMo> ListBaoCaoDM
        {
            get { return _ListBaoCaoDM; }
            set { _ListBaoCaoDM = value; OnPropertyChanged(); }
        }
        //---------------
        private string _STT;

        public string STT
        {
            get { return _STT; }
            set { _STT = value; OnPropertyChanged(); }
        }
        //---------------
        private string _Ngay;

        public string Ngay
        {
            get { return _Ngay; }
            set { _Ngay = value; OnPropertyChanged(); }
        }
        //---------------
        private string _SoMo;

        public string SoMo
        {
            get { return _SoMo; }
            set { _SoMo = value; OnPropertyChanged(); }
        }
        //---------------
        private string _SoDong;

        public string SoDong
        {
            get { return _SoDong; }
            set { _SoDong = value; OnPropertyChanged(); }
        }
        //---------------
        private string _ChenhLech;

        public string ChenhLech
        {
            get { return _ChenhLech; }
            set { _ChenhLech = value; OnPropertyChanged(); }
        }
        //---------------
        private List<ListBaoCaoDongMo_DaBaoCao> _ListDaBaoCao;

        public List<ListBaoCaoDongMo_DaBaoCao> ListDaBaoCao
        {
            get { return _ListDaBaoCao; }
            set { _ListDaBaoCao = value; OnPropertyChanged(); }
        }
        //------------
        private string _ThangNamDaBaoCao;

        public string ThangNamDaBaoCao
        {
            get { return _ThangNamDaBaoCao; }
            set { _ThangNamDaBaoCao = value; OnPropertyChanged(); }
        }
        //------------
        private string _LTKDaBaoCao;

        public string LTKDaBaoCao
        {
            get { return _LTKDaBaoCao; }
            set { _LTKDaBaoCao = value; OnPropertyChanged(); }
        }
        //------------
        private ListBaoCaoDongMo_DaBaoCao _SelectedMonth;

        public ListBaoCaoDongMo_DaBaoCao SelectedMonth
        {
            get { return _SelectedMonth; }
            set { _SelectedMonth = value; OnPropertyChanged(); }
        }

        #endregion
        #region function
        #region Code NgayThangNam
        private bool isCheck(int nam)
        {
            return ((nam % 4 == 0 && nam % 100 != 0) || nam % 400 == 0);
        }

        private int Daysinmonth(int thang, int nam)
        {
            switch (thang)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    return 31;
                case 4:
                case 6:
                case 9:
                case 11:
                    return 30;
                case 2:
                    if (isCheck(nam))
                        return 29;
                    else
                        return 28;
                default: return 0;
            }
        }

        private string FormatBaoCao(string mbc)
        {
            for (int i = 1; i <= 7 - mbc.Length; i++)
            {
                mbc = "0" + mbc;
            }
            return mbc;
        }
        #endregion
        private string CreateThangNam(string thang, string nam)
        {
            string res = thang + "-" + nam;
            if (int.Parse(thang) < 10) res = "0" + res;
            return res;
        }
        private void BindingListDaBaoCao()
        {
            ListDaBaoCao = new List<ListBaoCaoDongMo_DaBaoCao>();
            ObservableCollection<LOAITIETKIEM> LTK = new ObservableCollection<LOAITIETKIEM>(DataProvider.Ins.DB.LOAITIETKIEMs);
            ObservableCollection<BAOCAOMODONG> BCMD = new ObservableCollection<BAOCAOMODONG>(DataProvider.Ins.DB.BAOCAOMODONGs);
            /*foreach (BAOCAOMODONG bcmd_temp in BCMD)
            { 
                string ThangNam = bcmd_temp.ThangBaoCao + "/" + bcmd_temp.NamBaoCao;
                if (bcmd_temp.ThangBaoCao < 10)
                    ThangNam = "0" + ThangNam;
                string LoaiTK = "";
                foreach (var _ltk in LTK)
                    if (_ltk.MaLoaiTietKiem == bcmd_temp.MaLoaiTietKiem)
                    {
                        LoaiTK = _ltk.TenLoaiTietKiem;
                        break;
                    }
                ListDaBaoCao.Add(new ListBaoCaoDongMo_DaBaoCao(ThangNam, LoaiTK));
            }*/
            ///
            var baocao = from bcmd in BCMD
                         join ltk in LTK on bcmd.MaLoaiTietKiem equals ltk.MaLoaiTietKiem
                         orderby bcmd.NamBaoCao descending, bcmd.ThangBaoCao descending, ltk.TenLoaiTietKiem ascending
                         select new ListBaoCaoDongMo_DaBaoCao(CreateThangNam(bcmd.ThangBaoCao.ToString(), bcmd.NamBaoCao.ToString()), ltk.TenLoaiTietKiem);

            foreach (var bc in baocao)
                ListDaBaoCao.Add(bc);
        }
        private string GetCodeMBCMD()
        {
            ObservableCollection<BAOCAOMODONG> List_BCMD = new ObservableCollection<BAOCAOMODONG>(DataProvider.Ins.DB.BAOCAOMODONGs);
            int tmp = List_BCMD.Count();
            return "BCDM" + FormatBaoCao((tmp + 1).ToString());
        }   
        #endregion
        public ICommand CreateReportCommand { get; set; }
        public ICommand SelectedMonthYear_Command { get; set; }
        public BaoCaoMoDong_ViewModel()
        {



            BindingListDaBaoCao();       

            //Binding LoaiTietKiem_CBB
            ObservableCollection<LOAITIETKIEM> LTK = new ObservableCollection<LOAITIETKIEM>(DataProvider.Ins.DB.LOAITIETKIEMs);
            List_LTK = new List<string>();
            foreach (LOAITIETKIEM temp in LTK)
            {
                List_LTK.Add(temp.TenLoaiTietKiem);
            }

            //Binding List_Nam
            List_Nam = new List<string>();
            DateTime tempNam = DateTime.Today;
            string nowYear = tempNam.Year.ToString();
            for (int i = 1990; i <= int.Parse(nowYear); i++)
            {
                List_Nam.Add(i.ToString());
            }

            //Binding List_Thang_CBB
            List_Thang = new List<string>();
            string Nam = DateTime.Now.ToString("dd/MM/yyyy");
            Nam = Nam.Substring(6, 4);
            
            string formatThang = "Tháng";
            for (int i = 1; i <= 12; i++)
            {
                string month = i.ToString();
                if (i < 10) month = '0' + month;
                string temp = formatThang + ' ' + month;
                List_Thang.Add(temp);
            }
            
            //Binding Selected Item
            SelectedMonthYear_Command = new RelayCommand<object>((p) =>
            {
                return true;
            },
           (p) =>
           {
               if (SelectedMonth != null)
               {
                   ObservableCollection<SOTIETKIEM> stk = new ObservableCollection<SOTIETKIEM>(DataProvider.Ins.DB.SOTIETKIEMs);
                   ObservableCollection<LOAITIETKIEM> ltk = new ObservableCollection<LOAITIETKIEM>(DataProvider.Ins.DB.LOAITIETKIEMs);

                   string ThangChon = SelectedMonth.ThangNamDaBaoCao.Substring(0, 2);
                   string NamChon = SelectedMonth.ThangNamDaBaoCao.Substring(3, 4);
                   int NgayToiDaThang = Daysinmonth(int.Parse(ThangChon), int.Parse(NamChon));

                   ListBaoCaoDM = new List<ListBaoCaoDongMo>();
                   string maltk = "";
                   foreach (var _ltk in ltk)
                       if (_ltk.TenLoaiTietKiem == SelectedMonth.LTKDaBaoCao)
                           maltk = _ltk.MaLoaiTietKiem;

                   for (int date = 1; date <= NgayToiDaThang; date++)
                   {
                       string tempNgay = date.ToString();
                       string SoTT = date.ToString();
                       if ((date >= 1) && date <= 9) tempNgay = '0' + tempNgay;
                       string ngaykt = tempNgay + '-' + ThangChon + "-" + NamChon;
                       int SLSoMo = 0, SLSoDong = 0;
                       foreach (var _stk in stk)
                       {
                           string ngaymo = _stk.NgayMoSo.ToString("dd/MM/yyyy");
                           string ngaydong = _stk.NgayDongSo?.ToString("dd/MM/yyyy");
                           if (_stk.MaLoaiTietKiem == maltk && ngaymo == ngaykt)
                               SLSoMo++;
                           if (_stk.MaLoaiTietKiem == maltk && ngaydong == ngaykt)
                               SLSoDong++;
                       }
                       //add ListBaoCao
                       ListBaoCaoDM.Add(new ListBaoCaoDongMo
                       (
                           STT = SoTT,
                           Ngay = ngaykt,
                           SoMo = SLSoMo.ToString(),
                           SoDong = SLSoDong.ToString(),
                           ChenhLech = Math.Abs(SLSoMo - SLSoDong).ToString()
                       ));
                   }
               }
           });
            //Binding ListBaoCao
            CreateReportCommand = new RelayCommand<object>((p) =>
            {
                return true;
            },
            (p) =>
            {
                ObservableCollection<SOTIETKIEM> stk = new ObservableCollection<SOTIETKIEM>(DataProvider.Ins.DB.SOTIETKIEMs);
                ObservableCollection<LOAITIETKIEM> ltk = new ObservableCollection<LOAITIETKIEM>(DataProvider.Ins.DB.LOAITIETKIEMs);
                ObservableCollection<BAOCAOMODONG> bcmd = new ObservableCollection<BAOCAOMODONG>(DataProvider.Ins.DB.BAOCAOMODONGs);

                
                string mabaocao = GetCodeMBCMD();
                int thangdangchon = int.Parse(Selected_Thang.Substring(6, 2));
                int namdangchon = int.Parse(Selected_Nam);
                string thangdangchon_st = thangdangchon.ToString();
                if (thangdangchon < 10)
                    thangdangchon_st = "0" + thangdangchon_st;
                int NgayToiDaThang = Daysinmonth(thangdangchon,namdangchon);
                ListBaoCaoDM = new List<ListBaoCaoDongMo>();
                string maltk = "";
                foreach (var _ltk in ltk)
                    if (_ltk.TenLoaiTietKiem == Selected_LTK)
                        maltk = _ltk.MaLoaiTietKiem;

             
                //Check already create
                bool CanAdd = true;
                foreach (var _bcmd in bcmd)
                    if (_bcmd.MaLoaiTietKiem == maltk && 
                        _bcmd.ThangBaoCao == thangdangchon && 
                        _bcmd.NamBaoCao== namdangchon
                       )
                        CanAdd = false;
                if (CanAdd==true)
                {
                    BAOCAOMODONG baocaoMD = new BAOCAOMODONG()
                    {
                        MaBaoCaoMoDong = mabaocao,
                        MaLoaiTietKiem = maltk,
                        ThangBaoCao = thangdangchon,
                        NamBaoCao = namdangchon
                    };
                    DataProvider.Ins.DB.BAOCAOMODONGs.Add(baocaoMD);
                    DataProvider.Ins.DB.SaveChanges();
                }
                //Binding List
                

                for (int date = 1; date <= NgayToiDaThang; date++)
                {
                    string tempNgay = date.ToString();
                    string SoTT = date.ToString();
                    if ((date >= 1) && date <= 9) tempNgay = '0' + tempNgay;
                    string ngaykt = tempNgay + '-' + thangdangchon_st + "-" + namdangchon.ToString();
                    int SLSoMo = 0, SLSoDong = 0;
                    foreach (var _stk in stk)
                    {
                        string ngaymo = _stk.NgayMoSo.ToString("dd/MM/yyyy");
                        string ngaydong = _stk.NgayDongSo?.ToString("dd/MM/yyyy");
                        if (_stk.MaLoaiTietKiem == maltk && ngaymo == ngaykt)
                            SLSoMo++;
                        if (_stk.MaLoaiTietKiem == maltk && ngaydong == ngaykt)
                            SLSoDong++;
                    }
                    //add ListBaoCao
                    ListBaoCaoDM.Add(new ListBaoCaoDongMo
                    (
                        STT = SoTT,
                        Ngay = ngaykt,
                        SoMo = SLSoMo.ToString(),
                        SoDong = SLSoDong.ToString(),
                        ChenhLech = Math.Abs(SLSoMo - SLSoDong).ToString()
                    ));
              
                    //lam on sua dum Database
                    if (SLSoDong == 0)
                        SLSoDong = 1;
                    if (SLSoMo == 0)
                        SLSoMo = 1;

                    //Add Database
                    if (CanAdd==true)
                    {
                        CTBCMODONG baocao = new CTBCMODONG()
                        {
                            MaBaoCaoMoDong = mabaocao,
                            NgayXet = DateTime.Parse(ngaykt),
                            SoLuongSoMo = SLSoMo,
                            SoLuongSoDong = SLSoDong,
                            ChenhLech = Math.Abs(SLSoMo - SLSoDong)
                        };
                        DataProvider.Ins.DB.CTBCMODONGs.Add(baocao);
                        DataProvider.Ins.DB.SaveChanges();
                    }
                }
                BindingListDaBaoCao();
            });


        }
    }
}
