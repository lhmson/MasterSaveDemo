using MasterSaveDemo.Helper;
using MasterSaveDemo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.ServiceModel.Configuration;
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

        private ObservableCollection<BangPhanQuyen> _ListPhanQuyen;
        public ObservableCollection<BangPhanQuyen> ListPhanQuyen
        {
            get => _ListPhanQuyen;
            set { _ListPhanQuyen = value; OnPropertyChanged(); }
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

        private BangPhanQuyen _SelectedPhanQuyen;
        public BangPhanQuyen SelectedPhanQuyen
        {
            get => _SelectedPhanQuyen;
            set { _SelectedPhanQuyen = value; OnPropertyChanged(); }
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

        private Visibility _VisibilityOfListPhanQuyen;
        public Visibility VisibilityOfListPhanQuyen
        {
            get => _VisibilityOfListPhanQuyen;
            set { _VisibilityOfListPhanQuyen = value; OnPropertyChanged(); }
        }

        private Visibility _VisibilityOfTenNhomQuyen;
        public Visibility VisibilityOfTenNhomQuyen
        {
            get => _VisibilityOfTenNhomQuyen;
            set { _VisibilityOfTenNhomQuyen = value; OnPropertyChanged(); }
        }
        //TenDangNhap
        private string _TenDangNhap;
        public string TenDangNhap
        {
            get => _TenDangNhap;
            set { _TenDangNhap = value; OnPropertyChanged(); }
        }

        private string _txtTenNhomQuyen;
        public string txtTenNhomQuyen
        {
            get => _txtTenNhomQuyen;
            set { _txtTenNhomQuyen = value; OnPropertyChanged(); }
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
        private int CreateCodeNhomNguoiDung()
        {
            ObservableCollection<NHOMNGUOIDUNG> listNhom = new ObservableCollection<NHOMNGUOIDUNG>(DataProvider.Ins.DB.NHOMNGUOIDUNGs);
            int max = 0;
            foreach (var item in listNhom)
                if (max < item.MaNhom)
                    max = item.MaNhom;
            return max + 1;
        }
         
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

        private void LoadDataPhanQuyen()
        {
            ListPhanQuyen = new ObservableCollection<BangPhanQuyen>();
            ObservableCollection<NHOMNGUOIDUNG> nhomNguoiDung = new ObservableCollection<NHOMNGUOIDUNG>(DataProvider.Ins.DB.NHOMNGUOIDUNGs);

            foreach (var item in nhomNguoiDung)
                ListPhanQuyen.Add(new BangPhanQuyen(item.TenNhom,false));
        }

        private string check_DeleteNhomNguoiDung(int maNhom)
        {
            ObservableCollection<NGUOIDUNG> ngDung = new ObservableCollection<NGUOIDUNG>(DataProvider.Ins.DB.NGUOIDUNGs);

            string res = "";
            foreach (var item in ngDung)
                if (item.MaNhom == maNhom)
                    res += item.HoTen + "\n";
            return res;

        }

        private void ResetCbxTenNhom()
        {
            CbxTenNhom.Clear();
            
            ObservableCollection<NHOMNGUOIDUNG> List_Nhom = new ObservableCollection<NHOMNGUOIDUNG>(DataProvider.Ins.DB.NHOMNGUOIDUNGs);
            foreach (var Nhom in List_Nhom)
            {
                CbxTenNhom.Add(Nhom.TenNhom);
                System.Windows.Forms.MessageBox.Show(CbxTenNhom.Last());
            }
            string res = "";
            foreach (var item in CbxTenNhom)
                res += item;
            System.Windows.Forms.MessageBox.Show(res);
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

        private void Add_PhanQuyen(int maNhom, int maChucNang)
        {
            PHANQUYEN PQ = new PHANQUYEN();
            PQ.MaNhom = maNhom;
            PQ.MaChucNang = maChucNang;
            DataProvider.Ins.DB.PHANQUYENs.Add(PQ);
            DataProvider.Ins.DB.SaveChanges();
        }

        private void Delete_PhanQuyen(int maNhom)
        {
            ObservableCollection<PHANQUYEN> listPhanQuyen = new ObservableCollection<PHANQUYEN>(DataProvider.Ins.DB.PHANQUYENs);
            foreach (var pq in listPhanQuyen)
                if (pq.MaNhom == maNhom)
                    DataProvider.Ins.DB.PHANQUYENs.Remove(pq);
            DataProvider.Ins.DB.SaveChanges();
        }

        private void Delete_NhomNguoiDung(string tenNhom)
        {
            ObservableCollection<NHOMNGUOIDUNG> nhom = new ObservableCollection<NHOMNGUOIDUNG>(DataProvider.Ins.DB.NHOMNGUOIDUNGs);
            foreach (var item in nhom)
                if (item.TenNhom == tenNhom)
                    DataProvider.Ins.DB.NHOMNGUOIDUNGs.Remove(item);
            DataProvider.Ins.DB.SaveChanges();
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
            LoadDataPhanQuyen();
            VisibilityOfListPhanQuyen = Visibility.Hidden;
            VisibilityOfTenNhomQuyen = Visibility.Hidden;
            // show elements used for adding
            AddNguoiDungCommand = new RelayCommand<object>((p) => {
                if (VisibilityOfListPhanQuyen == Visibility.Visible)
                    if (SelectedPhanQuyen != null)
                        if (SelectedPhanQuyen.EnabledCheckBox == true)
                            return false;

                return true; },
                (p) => {
                    if (VisibilityOfListPhanQuyen == Visibility.Hidden)
                    {
                        VisibilityOfAdd = Visibility.Visible;
                        VisibilityOfEdit = Visibility.Hidden;
                        VisibilityOfTenNhomQuyen = Visibility.Hidden;
                        // reset value for textbox because these textbox still keep value if you are editing and then change to add
                        ResetTextbox();
                    }
                    else
                    {
                        VisibilityOfAdd = Visibility.Hidden;
                        VisibilityOfEdit = Visibility.Hidden;
                        VisibilityOfTenNhomQuyen = Visibility.Visible;

                        if (SelectedPhanQuyen != null)
                        {
                            BangPhanQuyen PQ = SelectedPhanQuyen;
                            PQ.EnabledCheckBox = false;
                            foreach (var pq in ListPhanQuyen)
                                if (pq.TenNhomQuyen == PQ.TenNhomQuyen)
                                {
                                    ListPhanQuyen.Remove(pq);
                                    ListPhanQuyen.Add(PQ);
                                    SelectedPhanQuyen = PQ;
                                    break;
                                }
                        }
                    }
                }
            );

            // show elements used for editing
            EditNguoiDungCommand = new RelayCommand<object>((p) => 
            { return (SelectedPhanQuyen != null || SelectedItemNguoiDung != null); },
                (p) => {
                   
                    if (VisibilityOfListNguoiDung==Visibility.Visible && SelectedItemNguoiDung != null) // Edit Nhóm người dùng
                    {
                        VisibilityOfEdit = Visibility.Visible;
                        VisibilityOfAdd = Visibility.Hidden;
                        MatKhau = SelectedItemNguoiDung.MatKhau;
                        HoTen = SelectedItemNguoiDung.HoTen;
                        SelectedTenNhom = SelectedItemNguoiDung.TenNhom;
                    }

                    if (VisibilityOfListPhanQuyen == Visibility.Visible && SelectedPhanQuyen != null) // Edit Bảng phân quyền
                    {
                        VisibilityOfTenNhomQuyen = Visibility.Hidden;
                        BangPhanQuyen PQ = SelectedPhanQuyen;
                        PQ.EnabledCheckBox = true;
                        foreach(var pq in ListPhanQuyen)
                           if (pq.TenNhomQuyen == PQ.TenNhomQuyen)
                            {
                                ListPhanQuyen.Remove(pq);
                                ListPhanQuyen.Add(PQ);
                                SelectedPhanQuyen = PQ;
                                break;
                            }
                        
                    }
                }
            );

            DeleteNguoiDungKCommand = new RelayCommand<object>((p) => { return (SelectedItemNguoiDung != null || SelectedPhanQuyen!= null); },
                (p) => {
                    if (VisibilityOfListNguoiDung == Visibility.Visible)
                    { 
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
                    else
                    {
                        VisibilityOfTenNhomQuyen = Visibility.Hidden;
                        DialogResult kq = System.Windows.Forms.MessageBox.Show("Bạn chắc xóa nhóm người dùng này không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (kq == DialogResult.Yes)
                        {
                            string error = check_DeleteNhomNguoiDung(search_MaNhom(SelectedPhanQuyen.TenNhomQuyen));
                            if (error == "")
                            {
                                System.Windows.Forms.MessageBox.Show("Đã xóa nhóm " + SelectedPhanQuyen.TenNhomQuyen + " thành công!");
                                int maNhom = search_MaNhom(SelectedPhanQuyen.TenNhomQuyen);
                                Delete_PhanQuyen(maNhom);
                                Delete_NhomNguoiDung(SelectedPhanQuyen.TenNhomQuyen);
                                ResetCbxTenNhom();
                                ListPhanQuyen.Remove(SelectedPhanQuyen);

                            }
                            else
                                System.Windows.Forms.MessageBox.Show("Không thể xóa. Hãy xóa danh sách người dùng này để thực hiện: " + error);
                        }
                    }
                }
            );

            // Button: Confirm for adding NguoiDung
            ConfirmCommand = new RelayCommand<object>((p) =>
            {
            if (VisibilityOfAdd == Visibility.Visible)
                if (CheckValidData()) return true;
                else return false;
                if (VisibilityOfTenNhomQuyen == Visibility.Visible)
                    if (txtTenNhomQuyen != null && txtTenNhomQuyen != "") return true;
                    else return false;
                if (VisibilityOfEdit == Visibility.Visible)
                    if (CheckValidData()) return true;
                    else return false;
                if (SelectedPhanQuyen != null)
                    if (SelectedPhanQuyen.EnabledCheckBox == true)
                        return true;
                    else return false;
                return false;
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
                        VisibilityOfAdd = Visibility.Hidden;
                        ResetTextbox();
                        VisibilityOfAdd = Visibility.Hidden;
                    }
                    else if (VisibilityOfEdit == Visibility.Visible)
                    {
                        var temp = SelectedItemNguoiDung;
                        var nguoiDung = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TenDangNhap == SelectedItemNguoiDung.TenDangNhap).SingleOrDefault();
                        nguoiDung.MatKhau = MatKhau;
                        nguoiDung.MaNhom = search_MaNhom(SelectedTenNhom);
                        nguoiDung.HoTen = HoTen;
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

                        VisibilityOfEdit = Visibility.Hidden;
                        ////After confirming, selected item will die huhu, this line is used for making selected item reborn. 
                        ////You can continue change value without choosing item again if unnecessary
                        //SelectedItemLTK = temp;
                    }
                    else if (VisibilityOfTenNhomQuyen == Visibility.Visible)
                    {
                        if (txtTenNhomQuyen == null || txtTenNhomQuyen == "")
                        {
                            System.Windows.Forms.MessageBox.Show("Mời nhập lại tên nhóm quyền!");
                        }
                        else
                        {
                            BangPhanQuyen bpq = new BangPhanQuyen(txtTenNhomQuyen, false);
                            ListPhanQuyen.Add(bpq);
                            //Add Database nhóm người dùng mới
                            NHOMNGUOIDUNG nhom = new NHOMNGUOIDUNG();
                            nhom.MaNhom = CreateCodeNhomNguoiDung();
                            nhom.TenNhom = txtTenNhomQuyen;
                            DataProvider.Ins.DB.NHOMNGUOIDUNGs.Add(nhom);
                            DataProvider.Ins.DB.SaveChanges();
                            ResetCbxTenNhom();
                            txtTenNhomQuyen = "";
                            VisibilityOfTenNhomQuyen = Visibility.Hidden;
                           
                        }
                    }
                    else if (SelectedPhanQuyen.EnabledCheckBox == true)
                    {
                        int maNhom = search_MaNhom(SelectedPhanQuyen.TenNhomQuyen);

                        Delete_PhanQuyen(maNhom);
                        if (SelectedPhanQuyen.chkQLNS) Add_PhanQuyen(maNhom, 1);
                        if (SelectedPhanQuyen.chkMSTK) Add_PhanQuyen(maNhom, 2);
                        if (SelectedPhanQuyen.chkLPG) Add_PhanQuyen(maNhom, 3);
                        if (SelectedPhanQuyen.chkLPR) Add_PhanQuyen(maNhom, 4);
                        if (SelectedPhanQuyen.chkTCS) Add_PhanQuyen(maNhom, 5);
                        if (SelectedPhanQuyen.chkBCDS) Add_PhanQuyen(maNhom, 6);
                        if (SelectedPhanQuyen.chkBCMD) Add_PhanQuyen(maNhom, 7);
                        if (SelectedPhanQuyen.chkTDQD) Add_PhanQuyen(maNhom, 8);
                        if (SelectedPhanQuyen.chkNLVV) Add_PhanQuyen(maNhom, 9);
                        System.Windows.Forms.MessageBox.Show("Chỉnh sửa quyền thành công cho nhóm " + SelectedPhanQuyen.TenNhomQuyen);

                        BangPhanQuyen PQ = SelectedPhanQuyen;
                        PQ.EnabledCheckBox = false;
                        foreach (var pq in ListPhanQuyen)
                            if (pq.TenNhomQuyen == PQ.TenNhomQuyen)
                            {
                                ListPhanQuyen.Remove(pq);
                                ListPhanQuyen.Add(PQ);
                                SelectedPhanQuyen = PQ;
                                break;
                            }

                    }
                   
                }
                catch (Exception e) { };
            });

            CancelCommand = new RelayCommand<object>((p) =>
            {
                return (VisibilityOfTenNhomQuyen == Visibility.Visible || VisibilityOfAdd == Visibility.Visible || VisibilityOfEdit == Visibility.Visible);
            }, (p) =>
            {
                if (VisibilityOfAdd == Visibility.Visible)
                {
                    ResetTextbox();
                    VisibilityOfAdd = Visibility.Hidden;
                }
                else if (VisibilityOfEdit == Visibility.Visible)
                {
                    if (SelectedItemNguoiDung != null)
                    {
                        MatKhau = SelectedItemNguoiDung.MatKhau;
                        HoTen = SelectedItemNguoiDung.HoTen;
                        SelectedTenNhom = SelectedItemNguoiDung.TenNhom;
                    }
                    VisibilityOfEdit = Visibility.Hidden;
                }
                else if (VisibilityOfTenNhomQuyen == Visibility.Visible)
                {
                    VisibilityOfTenNhomQuyen = Visibility.Hidden;
                    txtTenNhomQuyen = "";
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
                    VisibilityOfListPhanQuyen = Visibility.Hidden;
                }
                else
                {
                    VisibilityOfListNguoiDung = Visibility.Hidden;
                    VisibilityOfListPhanQuyen = Visibility.Visible;
                    // co muon an may cai nay ko?
                    VisibilityOfAdd = VisibilityOfEdit = Visibility.Hidden;
                }

            });
        }
    }
}
