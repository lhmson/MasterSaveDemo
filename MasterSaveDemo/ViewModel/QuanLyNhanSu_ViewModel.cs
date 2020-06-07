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
    public class QuanLyNhanSu_ViewModel : BaseViewModel
    {
        #region Variables
        private ObservableCollection<NGUOIDUNG> _ListNguoiDung;
        public ObservableCollection<NGUOIDUNG> ListNguoiDung
        {
            get => _ListNguoiDung;
            set { _ListNguoiDung = value; OnPropertyChanged(); }
        }

        private ObservableCollection<NHOMNGUOIDUNG> _ListNhomNguoiDung;
        public ObservableCollection<NHOMNGUOIDUNG> ListNhomNguoiDung
        {
            get => _ListNhomNguoiDung;
            set { _ListNhomNguoiDung = value; OnPropertyChanged(); }
        }

        private NGUOIDUNG _SelectedItemNguoiDung;
        public NGUOIDUNG SelectedItemNguoiDung
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

        
        private string _CbxTenNhom;
        public string CbxTenNhom
        {
            get { return _CbxTenNhom; }
            set { _CbxTenNhom = value; }
        }

        private string _SelectedTenNhom;
        public string SelectedTenNhom
        {
            get { return _SelectedTenNhom; }
            set { _SelectedTenNhom = value; }
        }



        #endregion

        #region Function
        private void LoadData()
        {
            ListNguoiDung = new ObservableCollection<NGUOIDUNG>(DataProvider.Ins.DB.NGUOIDUNGs);
            ListNhomNguoiDung = new ObservableCollection<NHOMNGUOIDUNG>(DataProvider.Ins.DB.NHOMNGUOIDUNGs);
            TenNhom = new List<string>();
            foreach (var item in ListNhomNguoiDung)
            {
                // sao cho nay no khong hien ra danh sach dung ta, need to edit (Son) aaaaaa why
                TenNhom.Add(item.TenNhom);
                MessageBox.Show(item.TenNhom);
            }

            VisibilityOfAdd = Visibility.Hidden;
            VisibilityOfEdit = Visibility.Hidden;

            // choosing list NguoiDung
            SelectedIndexCbb = 0;
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
                if (string.IsNullOrEmpty(CbxTenNhom))
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
                
            }
            return false;
        }
        private void ResetTextbox()
        {
            TenDangNhap = "";
            MatKhau = "";
            HoTen = "";
            CbxTenNhom = "";

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
                        var nguoiDung = new NGUOIDUNG()
                        {
                            TenDangNhap = TenDangNhap,
                            MatKhau = MatKhau,
                            HoTen = HoTen,
                            //need to edit (Son) de them vao database va hien thi len cai listview
                            // MaNhom = ... (them vao cai gi ne ??)
                        };
                        DataProvider.Ins.DB.NGUOIDUNGs.Add(nguoiDung);
                        DataProvider.Ins.DB.SaveChanges();

                        ListNguoiDung.Add(nguoiDung);
                    }
                    else if (VisibilityOfEdit == Visibility.Visible)
                    {
                        //cai phan nay la copy ben Tien nen comment lai, dung duoc gi thi dung khong thi xoa nha
                        //var temp = SelectedItemLTK;
                        //var nguoiDung = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.MaLoaiTietKiem == SelectedItemLTK.MaLoaiTietKiem).SingleOrDefault();
                        //nguoiDung.LaiSuat = float.Parse(LaiSuat);
                        //nguoiDung.ThoiGianGuiToiThieu = Int32.Parse(ThoiGianGuiToiThieu);
                        //DataProvider.Ins.DB.SaveChanges();

                        ////find index of selected item in list, then remove and add the changed item
                        //int length = ListNguoiDung.Count();
                        //for (int i = 0; i < length; i++)
                        //{
                        //    if (ListNguoiDung[i].MaLoaiTietKiem == SelectedItemLTK.MaLoaiTietKiem)
                        //    {
                        //        ListNguoiDung.RemoveAt(i);
                        //        ListNguoiDung.Insert(i, nguoiDung);
                        //        break;
                        //    }
                        //}

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
