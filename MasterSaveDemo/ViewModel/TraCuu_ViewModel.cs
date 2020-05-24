using MasterSaveDemo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MasterSaveDemo.Model;

namespace MasterSaveDemo.ViewModel
{
    public class TraCuu_ViewModel : BaseViewModel
    {
        #region The sub funtions by Sanh
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

        private List<string> _LoaiTietKiem;
        public List<string> LoaiTietKiem { get => _LoaiTietKiem; set { _LoaiTietKiem = value; OnPropertyChanged(); } }

        private ObservableCollection<ListTraCuuSTK> _ListSoTietKiem;
        public ObservableCollection<ListTraCuuSTK> ListSoTietKiem { get => _ListSoTietKiem; set { _ListSoTietKiem = value; OnPropertyChanged(); } }

        private string _MaSTK;
        public string MaSTK { get => _MaSTK; set { _MaSTK = value; OnPropertyChanged(); } }

        private string _SelectedLTK;
        public string SelectedLTK { get => _SelectedLTK; set { _SelectedLTK = value; OnPropertyChanged(); } }

        private string _TenKH;
        public string TenKH { get => _TenKH; set { _TenKH = value; OnPropertyChanged(); } }

        private string _SoDu;
        public string SoDu { get => _SoDu; set { _SoDu = value; OnPropertyChanged(); } }

        private DateTime _NgayDaoHan;
        public DateTime NgayDaoHan { get => _NgayDaoHan; set { _NgayDaoHan = value; OnPropertyChanged(); } }
        #endregion

        public ICommand SeeAllCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        public TraCuu_ViewModel()
        {
            NgayDaoHan = DateTime.Now;
            // Init Combobox LoaiTietKiem
            ObservableCollection<LOAITIETKIEM> _List = new ObservableCollection<LOAITIETKIEM>(DataProvider.Ins.DB.LOAITIETKIEMs);
            LoaiTietKiem = new List<string>();
            foreach (LOAITIETKIEM LTK in _List)
                LoaiTietKiem.Add(LTK.TenLoaiTietKiem);

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
                              select new ListTraCuuSTK(stk.MaSoTietKiem, ltk.TenLoaiTietKiem, stk.TenKhachHang, 
                              Delete_ThapPhan(stk.SoDu.ToString()), stk.NgayDaoHanKeTiep.ToString("dd-MM-yyyy"), stk.LaiSuatApDung.ToString());
                ListSoTietKiem = new ObservableCollection<ListTraCuuSTK>(results);
                #region fill id 
                int count = 1;
                for (int i = 0; i < ListSoTietKiem.Count(); i++)
                {
                    ListSoTietKiem[i].STT = count.ToString();
                    count++;
                }
                #endregion
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

                var results = from stk in STK_TABLE
                              join ltk in LTK_TABLE on stk.MaLoaiTietKiem equals ltk.MaLoaiTietKiem
                              where (stk.MaSoTietKiem == MaSTK || MaSTK == "") && (stk.TenKhachHang.Contains(TenKH))
                                    && (String.IsNullOrEmpty(SoDu) || Delete_ThapPhan(stk.SoDu.ToString()) == SoDu)
                                    && (NgayDaoHan == null || NgayDaoHan.ToString("dd-MM-yyyy") == stk.NgayDaoHanKeTiep.ToString("dd-MM-yyyy"))
                                    && (String.IsNullOrEmpty(SelectedLTK) || ltk.TenLoaiTietKiem == SelectedLTK)
                              select new ListTraCuuSTK(stk.MaSoTietKiem, ltk.TenLoaiTietKiem, stk.TenKhachHang, Delete_ThapPhan(stk.SoDu.ToString()), stk.NgayDaoHanKeTiep.ToString("dd-MM-yyyy"), stk.LaiSuatApDung.ToString());

                ListSoTietKiem = new ObservableCollection<ListTraCuuSTK>(results);
                #region fill id 
                int count = 1;
                for (int i=0; i<ListSoTietKiem.Count(); i++)
                {
                    ListSoTietKiem[i].STT = count.ToString();
                    count++;
                }
                #endregion
            });
        }
    }
}
