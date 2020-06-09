using MasterSaveDemo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace MasterSaveDemo.ViewModel
{
    public class QuanLyNhanSu_ViewModel : BaseViewModel
    {
        #region Variables
        private ObservableCollection<NhanVien> _ListNhanVien;
        public ObservableCollection<NhanVien> ListNhanVien
        {
            get => _ListNhanVien;
            set { _ListNhanVien = value; OnPropertyChanged(); }
        }

        private ObservableCollection<NHOMNGUOIDUNG> _ListNhomNguoiDung;
        public ObservableCollection<NHOMNGUOIDUNG> ListNhomNguoiDung
        {
            get => _ListNhomNguoiDung;
            set { _ListNhomNguoiDung = value; OnPropertyChanged(); }
        }

        private NhanVien _SelectedItemNguoiDung;
        public NhanVien SelectedItemNguoiDung
        {
            get => _SelectedItemNguoiDung;
            set { _SelectedItemNguoiDung = value; OnPropertyChanged(); }
        }

        private NHOMNGUOIDUNG _SelectedItemNhomNguoiDung;
        public NHOMNGUOIDUNG SelectedItemNhomNguoiDung
        {
            get => _SelectedItemNhomNguoiDung;
            set { _SelectedItemNhomNguoiDung = value; OnPropertyChanged(); }
        }

        private int _SelectedIndexCbb;
        public int SelectedIndexCbb
        {
            get => _SelectedIndexCbb;
            set { _SelectedIndexCbb = value; OnPropertyChanged(); }
        }

        // Visibility of add elements
        private Visibility _VisibilityOfAdd;
        public Visibility VisibilityOfAdd
        {
            get => _VisibilityOfAdd;
            set { _VisibilityOfAdd = value; OnPropertyChanged(); }
        }

        // Visibility of edit elements
        private Visibility _VisibilityOfEdit;
        public Visibility VisibilityOfEdit
        {
            get => _VisibilityOfEdit;
            set { _VisibilityOfEdit = value; OnPropertyChanged(); }
        }

        // Visibility of listview NGUOIDUNG
        private Visibility _VisibilityOfListNguoiDung;
        public Visibility VisibilityOfListNguoiDung
        {
            get => _VisibilityOfListNguoiDung;
            set { _VisibilityOfListNguoiDung = value; OnPropertyChanged(); }
        }

        //TenDangNhap
        private string _TenDangNhap;
        public string TenDangNhap
        {
            get => _TenDangNhap;
            set { _TenDangNhap = value; OnPropertyChanged(); }
        }

        //MatKhau
        private string _MatKhau;
        public string MatKhau
        {
            get => _MatKhau;
            set { _MatKhau = value; OnPropertyChanged(); }
        }

        //Hoten
        private string _HoTen;
        public string HoTen
        {
            get => _HoTen;
            set { _HoTen = value; OnPropertyChanged(); }
        }

        //Ten nhomquyen
        private List<string> _TenNhom;
        public List<string> TenNhom
        {
            get => _TenNhom;
            set { _TenNhom = value; OnPropertyChanged(); }
        }

        
        private List<string> _CbxTenNhom;
        public List<string> CbxTenNhom
        {
            get { return _CbxTenNhom; }
            set { _CbxTenNhom = value; OnPropertyChanged(); }
        }

        private string _TextTenNhom;
        public string TextTenNhom
        {
            get { return _TextTenNhom; }
            set { _TextTenNhom = value; OnPropertyChanged(); }
        }
        private string _SelectedTenNhom;
        public string SelectedTenNhom
        {
            get { return _SelectedTenNhom; }
            set { _SelectedTenNhom = value; OnPropertyChanged(); }
        }



        #endregion

        #region Function
        private void LoadData()
        {
            ObservableCollection<NGUOIDUNG> ListNguoiDung = new ObservableCollection<NGUOIDUNG>(DataProvider.Ins.DB.NGUOIDUNGs);
            ListNhomNguoiDung = new ObservableCollection<NHOMNGUOIDUNG>(DataProvider.Ins.DB.NHOMNGUOIDUNGs);

            var nhanvien = from ng_Dung in ListNguoiDung
                           join nhom in ListNhomNguoiDung on ng_Dung.MaNhom equals nhom.MaNhom
                           select new NhanVien(ng_Dung.TenDangNhap, ng_Dung.MatKhau, ng_Dung.HoTen, nhom.TenNhom);
            ListNhanVien = new ObservableCollection<NhanVien>(nhanvien);

            VisibilityOfAdd = Visibility.Hidden;
            VisibilityOfEdit = Visibility.Hidden;

            // choosing list NguoiDung
            SelectedIndexCbb = 0;

            CbxTenNhom = new List<string>();
            foreach (var Nhom in ListNhomNguoiDung)
                CbxTenNhom.Add(Nhom.TenNhom);
        }

        private int search_MaNhom(string TenNhom)
        {
            int ma = 0;

            ObservableCollection<NHOMNGUOIDUNG> ListNhomNguoiDung = new ObservableCollection<NHOMNGUOIDUNG>(DataProvider.Ins.DB.NHOMNGUOIDUNGs);

            foreach (var nhom in ListNhomNguoiDung)
                if (nhom.TenNhom == TenNhom)
                    return nhom.MaNhom;
            return ma;
        }

        private bool CheckValidData()
        {
            if (VisibilityOfAdd == Visibility.Visible)
            {
                if (string.IsNullOrEmpty(TenDangNhap))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(MatKhau))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(HoTen))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(SelectedTenNhom))
                {
                    return false;
                }
                var tenDangNhap = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TenDangNhap == TenDangNhap);
                if (tenDangNhap == null || tenDangNhap.Count() != 0)
                    return false;
                return true;
            }
            else if (VisibilityOfEdit == Visibility.Visible)
            {
                if (string.IsNullOrEmpty(MatKhau))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(HoTen))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(SelectedTenNhom))
                {
                    return false;
                }
            }
            return true;
        }
        private void ResetTextbox()
        {
            TenDangNhap = "";
            MatKhau = "";
            HoTen = "";
            TextTenNhom = "";
            SelectedTenNhom = null;
        }
        #endregion

        #region ICommand
        public ICommand AddNguoiDungCommand { get; set; }
        public ICommand EditNguoiDungCommand { get; set; }
        public ICommand DeleteNguoiDungKCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand CbbSelectionChangedCommand { get; set; }
        #endregion

        public QuanLyNhanSu_ViewModel()
        {
            LoadData();

            // show elements used for adding
            AddNguoiDungCommand = new RelayCommand<object>((p) => { return true; },
                (p) => {
                    VisibilityOfAdd = Visibility.Visible;
                    VisibilityOfEdit = Visibility.Hidden;

                    // reset value for textbox because these textbox still keep value if you are editing and then change to add
                    ResetTextbox();
                }
            );

            // show elements used for editing
            EditNguoiDungCommand = new RelayCommand<object>((p) => { return true; },
                (p) => {
                   
                    if (SelectedItemNguoiDung != null)
                    {
                        VisibilityOfEdit = Visibility.Visible;
                        VisibilityOfAdd = Visibility.Hidden;
                        MatKhau = SelectedItemNguoiDung.MatKhau;
                        HoTen = SelectedItemNguoiDung.HoTen;
                        SelectedTenNhom = SelectedItemNguoiDung.TenNhom;
                    }
                }
            );

            DeleteNguoiDungKCommand = new RelayCommand<object>((p) => { return (SelectedItemNguoiDung != null); },
                (p) => {
                        VisibilityOfEdit = Visibility.Hidden;
                        VisibilityOfAdd = Visibility.Hidden;
                    DialogResult kq = System.Windows.Forms.MessageBox.Show("Bạn chắc xóa người dùng này không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    var nguoiDung = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TenDangNhap == SelectedItemNguoiDung.TenDangNhap).SingleOrDefault();
                    DataProvider.Ins.DB.NGUOIDUNGs.Remove(nguoiDung);
                    DataProvider.Ins.DB.SaveChanges();
                    if (kq == DialogResult.Yes)
                    {
                        int length = ListNhanVien.Count();
                        for (int i = 0; i < length; i++)
                        {
                            if (ListNhanVien[i].TenDangNhap == SelectedItemNguoiDung.TenDangNhap)
                            {
                                ListNhanVien.RemoveAt(i);
                                break;
                            }
                        }
                        System.Windows.Forms.MessageBox.Show("Xóa người dùng thành công");
                    }
                }
            );

            // Button: Confirm for adding NguoiDung
            ConfirmCommand = new RelayCommand<object>((p) =>
            {
                return CheckValidData();
            }, (p) =>
            {
                try
                {
                    if (VisibilityOfAdd == Visibility.Visible)
                    {
                        int Ma_Nhom = search_MaNhom(SelectedTenNhom);
                        var nguoiDung = new NGUOIDUNG()
                        {
                            TenDangNhap = TenDangNhap,
                            MatKhau = MatKhau,
                            HoTen = HoTen,
                            MaNhom = Ma_Nhom
                        };
                        DataProvider.Ins.DB.NGUOIDUNGs.Add(nguoiDung);
                        DataProvider.Ins.DB.SaveChanges();


                        NhanVien nv = new NhanVien(TenDangNhap, MatKhau, HoTen, SelectedTenNhom);
                        ListNhanVien.Add(nv);
                    }
                    else if (VisibilityOfEdit == Visibility.Visible)
                    {
                        var temp = SelectedItemNguoiDung;
                        var nguoiDung = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TenDangNhap == SelectedItemNguoiDung.TenDangNhap).SingleOrDefault();
                        nguoiDung.MatKhau = MatKhau;
                        nguoiDung.MaNhom = search_MaNhom(SelectedTenNhom);
                        DataProvider.Ins.DB.SaveChanges();

                        NhanVien nv = new NhanVien(nguoiDung.TenDangNhap, nguoiDung.MatKhau, nguoiDung.HoTen, SelectedTenNhom);
                        int length = ListNhanVien.Count();
                        for (int i = 0; i < length; i++)
                        {
                            if (ListNhanVien[i].TenDangNhap == SelectedItemNguoiDung.TenDangNhap)
                            {
                                ListNhanVien.RemoveAt(i);
                                ListNhanVien.Insert(i, nv);
                                break;
                            }
                        }

                        ////After confirming, selected item will die huhu, this line is used for making selected item reborn. 
                        ////You can continue change value without choosing item again if unnecessary
                        //SelectedItemLTK = temp;
                    }
                }
                catch (Exception e) { };
            });

            CancelCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                if (VisibilityOfAdd == Visibility.Visible)
                {
                    ResetTextbox();
                }
                else if (VisibilityOfEdit == Visibility.Visible)
                {
                    if (SelectedItemNguoiDung != null)
                    {
                        MatKhau = SelectedItemNguoiDung.MatKhau;
                        HoTen = SelectedItemNguoiDung.HoTen;
                        SelectedTenNhom = SelectedItemNguoiDung.TenNhom;
                    }
                }
            });

            CbbSelectionChangedCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                // selected index = 0: choosing list of NGUOIDUNG
                // selected index = 1: choosing list of PhanQuyen
                if (SelectedIndexCbb == 0)
                {
                    VisibilityOfListNguoiDung = Visibility.Visible;
                }
                else
                {
                    VisibilityOfListNguoiDung = Visibility.Hidden;

                    // co muon an may cai nay ko?
                    VisibilityOfAdd = VisibilityOfEdit = Visibility.Hidden;
                }

            });
        }
    }
}
