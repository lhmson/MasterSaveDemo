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
    
    public class ThayDoiQuyDinh_ViewModel : BaseViewModel
    {
        #region Variables
        private ObservableCollection<LOAITIETKIEM> _ListLTK;
        public ObservableCollection<LOAITIETKIEM> ListLTK
        {
            get => _ListLTK;
            set { _ListLTK = value; OnPropertyChanged(); }
        }

        private ObservableCollection<THAMSO> _ListThamSo;
        public ObservableCollection<THAMSO> ListThamSo
        {
            get => _ListThamSo;
            set { _ListThamSo = value; OnPropertyChanged(); }
        }

        private LOAITIETKIEM _SelectedItemLTK;
        public LOAITIETKIEM SelectedItemLTK
        {
            get => _SelectedItemLTK;
            set { _SelectedItemLTK = value; OnPropertyChanged(); }
        }

        private THAMSO _SelectedItemThamSo;
        public THAMSO SelectedItemThamSo
        {
            get => _SelectedItemThamSo;
            set { _SelectedItemThamSo = value; OnPropertyChanged(); }
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

        // Visibility of edit elements of LTK
        private Visibility _VisibilityOfEditLTK;
        public Visibility VisibilityOfEditLTK
        {
            get => _VisibilityOfEditLTK;
            set { _VisibilityOfEditLTK = value; OnPropertyChanged(); }
        }

        // Visibility of edit elements of ThamSo
        private Visibility _VisibilityOfEditThamSo;
        public Visibility VisibilityOfEditThamSo
        {
            get => _VisibilityOfEditThamSo;
            set { _VisibilityOfEditThamSo = value; OnPropertyChanged(); }
        }

        // Visibility of listview Loaitietkiem
        private Visibility _VisibilityOfListLTK;
        public Visibility VisibilityOfListLTK
        {
            get => _VisibilityOfListLTK;
            set { _VisibilityOfListLTK = value; OnPropertyChanged(); }
        }

        // Visibility of listview Thamso
        private Visibility _VisibilityOfListThamSo;
        public Visibility VisibilityOfListThamSo
        {
            get => _VisibilityOfListThamSo;
            set { _VisibilityOfListThamSo = value; OnPropertyChanged(); }
        }

        // check if deleting or not
        private bool _IsDeleting;
        public bool IsDeleting
        {
            get => _IsDeleting;
            set { _IsDeleting = value; OnPropertyChanged(); }
        }

        //MaLoaiTietKiem
        private string _MaLoaiTietKiem;
        public string MaLoaiTietKiem
        {
            get => _MaLoaiTietKiem;
            set { _MaLoaiTietKiem = value; OnPropertyChanged(); }
        }

        //TenLoaiTietKiem
        private string _TenLoaiTietKiem;
        public string TenLoaiTietKiem
        {
            get => _TenLoaiTietKiem;
            set { _TenLoaiTietKiem = value; OnPropertyChanged(); }
        }

        //KyHan
        private string _KyHan;
        public string KyHan
        {
            get => _KyHan;
            set { _KyHan = value; OnPropertyChanged(); }
        }

        //LaiSuat
        private string _LaiSuat;
        public string LaiSuat
        {
            get => _LaiSuat;
            set { _LaiSuat = value; OnPropertyChanged(); }
        }

        //ThoiGianGuiToiThieu
        private string _ThoiGianGuiToiThieu;
        public string ThoiGianGuiToiThieu
        {
            get => _ThoiGianGuiToiThieu;
            set { _ThoiGianGuiToiThieu = value; OnPropertyChanged(); }
        }

        //QuyDinhSoTienRut
        private string _QuyDinhSoTienRut;
        public string QuyDinhSoTienRut
        {
            get => _QuyDinhSoTienRut;
            set { _QuyDinhSoTienRut = value; OnPropertyChanged(); }
        }

        //DangSuDung
        private int _DangSuDung;
        public int DangSuDung
        {
            get => _DangSuDung;
            set { _DangSuDung = value; OnPropertyChanged(); }
        }

        //Ten tham so
        private string _TenThamSo;
        public string TenThamSo
        {
            get => _TenThamSo;
            set { _TenThamSo = value; OnPropertyChanged(); }
        }

        //Gia tri cua tham so
        private string _GiaTriThamSo;
        public string GiaTriThamSo
        {
            get => _GiaTriThamSo;
            set { _GiaTriThamSo = value; OnPropertyChanged(); }
        }

        //
        private string _NameOfList;
        public string NameOfList
        {
            get => _NameOfList;
            set { _NameOfList = value; OnPropertyChanged(); }
        }
        #endregion

        #region Function
        private void LoadData()
        {
            var listLTK_Using = DataProvider.Ins.DB.LOAITIETKIEMs.Where(x => x.DangSuDung != 0);

            ListLTK = new ObservableCollection<LOAITIETKIEM>(listLTK_Using);
            ListThamSo = new ObservableCollection<THAMSO>(DataProvider.Ins.DB.THAMSOes);

            VisibilityOfAdd = Visibility.Hidden;
            VisibilityOfEditLTK = Visibility.Hidden;
            VisibilityOfEditThamSo = Visibility.Hidden;
            VisibilityOfListThamSo = Visibility.Hidden;

            // choosing list LTK
            SelectedIndexCbb = 0;
            NameOfList = "Danh sách loại tiết kiệm";
        }
        private bool CheckValidData()
        {
            if (VisibilityOfAdd == Visibility.Visible)
            {
                if (string.IsNullOrEmpty(MaLoaiTietKiem))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(TenLoaiTietKiem))
                {
                    return false;
                }
                // check if null or not a number, return false
                if (string.IsNullOrEmpty(KyHan) || !int.TryParse(KyHan, out _))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(LaiSuat) || !float.TryParse(LaiSuat, out _))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(ThoiGianGuiToiThieu) || !int.TryParse(ThoiGianGuiToiThieu, out _))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(QuyDinhSoTienRut) || !int.TryParse(QuyDinhSoTienRut, out _))
                {
                    return false;
                }

                if( !IsDeleting)
                {
                    var maLoai = DataProvider.Ins.DB.LOAITIETKIEMs.Where(x => x.MaLoaiTietKiem == MaLoaiTietKiem);
                    if (maLoai == null || maLoai.Count() != 0)
                        return false;
                }
                return true;
            } else if (VisibilityOfEditLTK == Visibility.Visible)
            {
                if (string.IsNullOrEmpty(LaiSuat) || !float.TryParse(LaiSuat, out _))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(ThoiGianGuiToiThieu) || !int.TryParse(ThoiGianGuiToiThieu, out _))
                {
                    return false;
                }

                var laiSuat = DataProvider.Ins.DB.LOAITIETKIEMs.Where(x => x.LaiSuat == float.Parse(LaiSuat));
                var thoiGianGuiToiThieu = DataProvider.Ins.DB.LOAITIETKIEMs.Where(x => x.ThoiGianGuiToiThieu == Int32.Parse(ThoiGianGuiToiThieu));
                if (laiSuat.Count() != 0 && thoiGianGuiToiThieu.Count() != 0)
                    return false;
                return true;
            }
            return false;
        }
        private void ResetTextbox()
        {
            MaLoaiTietKiem = "";
            TenLoaiTietKiem = "";
            KyHan = "";
            LaiSuat = "";
            ThoiGianGuiToiThieu = "";
            QuyDinhSoTienRut = "";
        }
        private void SetTextboxValue()
        {
            MaLoaiTietKiem = SelectedItemLTK.MaLoaiTietKiem;
            TenLoaiTietKiem = SelectedItemLTK.TenLoaiTietKiem;
            KyHan = SelectedItemLTK.KyHan.ToString();
            LaiSuat = SelectedItemLTK.LaiSuat.ToString();
            ThoiGianGuiToiThieu = SelectedItemLTK.ThoiGianGuiToiThieu.ToString();
            QuyDinhSoTienRut = SelectedItemLTK.QuyDinhSoTienRut.ToString();
        }
        #endregion

        #region ICommand
        public ICommand AddLTKCommand { get; set; }
        public ICommand EditLTKCommand { get; set; }
        public ICommand DeleteLTKCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand CbbSelectionChangedCommand { get; set; }
        #endregion

        public ThayDoiQuyDinh_ViewModel()
        {
            IsDeleting = false;
            LoadData();                 

            // show elements used for adding
            AddLTKCommand = new RelayCommand<object>((p) => { return SelectedIndexCbb==0 ? true : false; }, 
                (p) => {
                    IsDeleting = false;
                    VisibilityOfAdd = Visibility.Visible;
                    VisibilityOfEditLTK = Visibility.Hidden;

                    // reset value for textbox because these textbox still keep value if you are editing and then change to add
                    ResetTextbox();
                }
            );

            // show elements used for editing
            EditLTKCommand = new RelayCommand<object>((p) => { return true; },
                (p) => {
                    if( SelectedIndexCbb == 0)
                    {
                        if (SelectedItemLTK != null)
                        {
                            ThoiGianGuiToiThieu = SelectedItemLTK.ThoiGianGuiToiThieu.ToString();
                            LaiSuat = SelectedItemLTK.LaiSuat.ToString();

                            IsDeleting = false;
                            VisibilityOfEditLTK = Visibility.Visible;
                            VisibilityOfAdd = Visibility.Hidden;
                        }
                    }
                }
            );

            // show elements used for deleting
            DeleteLTKCommand = new RelayCommand<object>((p) => { return SelectedIndexCbb == 0 ? true : false; },
                (p) => {
                    IsDeleting = true;
                    SetTextboxValue();
                    VisibilityOfAdd = Visibility.Visible;
                    VisibilityOfEditLTK = Visibility.Hidden;
                }
            );

            // Button: Confirm for adding LOAITIETKIEM
            ConfirmCommand = new RelayCommand<object>((p) => 
            {
                return CheckValidData();
            }, (p) => 
            {
                try
                {
                    if (VisibilityOfAdd == Visibility.Visible)
                    {
                        if(!IsDeleting)
                        {
                            var loaiTietKiem = new LOAITIETKIEM()
                            {
                                MaLoaiTietKiem = MaLoaiTietKiem,
                                TenLoaiTietKiem = TenLoaiTietKiem,
                                KyHan = Int32.Parse(KyHan),
                                LaiSuat = float.Parse(LaiSuat),
                                ThoiGianGuiToiThieu = Int32.Parse(ThoiGianGuiToiThieu),
                                QuyDinhSoTienRut = Int32.Parse(QuyDinhSoTienRut)
                            };
                            DataProvider.Ins.DB.LOAITIETKIEMs.Add(loaiTietKiem);
                            DataProvider.Ins.DB.SaveChanges();

                            ListLTK.Add(loaiTietKiem);
                        }
                        else
                        {
                            SelectedItemLTK.DangSuDung = 0;
                            DataProvider.Ins.DB.SaveChanges();

                            ListLTK.Remove(SelectedItemLTK);
                            ResetTextbox();
                        }
                    } else if(VisibilityOfEditLTK == Visibility.Visible)
                    {
                        SelectedItemLTK.LaiSuat = float.Parse(LaiSuat);
                        SelectedItemLTK.ThoiGianGuiToiThieu = Int32.Parse(ThoiGianGuiToiThieu);
                        DataProvider.Ins.DB.SaveChanges();
                        var temp = SelectedItemLTK;

                        //find index of selected item in list, then remove and add the changed item
                        int length = ListLTK.Count();
                        for( int i=0; i<length; i++)
                        {
                            if( ListLTK[i].MaLoaiTietKiem == SelectedItemLTK.MaLoaiTietKiem)
                            {
                                ListLTK.RemoveAt(i);
                                ListLTK.Insert(i, temp);
                                break;
                            }
                        }

                        //After confirming, selected item will die huhu, this line is used for making selected item reborn. 
                        //You can continue change value without choosing item again if unnecessary
                        SelectedItemLTK = temp;
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
                else if (VisibilityOfEditLTK == Visibility.Visible)
                {
                    ThoiGianGuiToiThieu = SelectedItemLTK.ThoiGianGuiToiThieu.ToString();
                    LaiSuat = SelectedItemLTK.LaiSuat.ToString();
                }
            });

            CbbSelectionChangedCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                // selected index = 0: choosing list of Loaitietkiem
                // selected index = 1: choosing list of Thamso
                if (SelectedIndexCbb == 0)
                {
                    NameOfList = "Danh sách loại tiết kiệm";
                    VisibilityOfListLTK = Visibility.Visible;
                    VisibilityOfListThamSo = Visibility.Hidden;
                } else
                {
                    NameOfList = "Danh sách tham số";
                    VisibilityOfListThamSo = Visibility.Visible;
                    VisibilityOfListLTK = Visibility.Hidden;

                    // co muon an may cai nay ko?
                    VisibilityOfAdd = VisibilityOfEditLTK = Visibility.Hidden;
                }

            });
        }
    }
}
