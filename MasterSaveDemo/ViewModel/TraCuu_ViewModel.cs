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
using System.Data;

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

                if (SelectedMucSoDu == "Tất cả")
                {
                    min = 0;
                    max = -1;
                }

                var results = from stk in STK_TABLE
                              join ltk in LTK_TABLE on stk.MaLoaiTietKiem equals ltk.MaLoaiTietKiem
                              where (stk.MaSoTietKiem == MaSTK || MaSTK == "") && (stk.TenKhachHang.Contains(TenKH))
                                    && (String.IsNullOrEmpty(SoDu) || Delete_ThapPhan(stk.SoDu.ToString()) == SoDu)
                                    && (String.IsNullOrEmpty(SelectedLTK) || ltk.TenLoaiTietKiem == SelectedLTK)
                                    && (stk.SoDu >= min && (stk.SoDu <= max || max ==-1))                            
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
