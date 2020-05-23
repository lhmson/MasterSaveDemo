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

        private string _SelectedItemCbb;
        public string SelectedItemCbb
        {
            get => _SelectedItemCbb;
            set { _SelectedItemCbb = value; OnPropertyChanged(); }
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

        //SoTienDuocRut
        private string _SoTienDuocRut;
        public string SoTienDuocRut
        {
            get => _SoTienDuocRut;
            set { _SoTienDuocRut = value; OnPropertyChanged(); }
        }

        //SoTienGuiToiThieu
        private string _SoTienGuiToiThieu;
        public string SoTienGuiToiThieu
        {
            get => _SoTienGuiToiThieu;
            set { _SoTienGuiToiThieu = value; OnPropertyChanged(); }
        }
        #endregion

        #region Function
        private void LoadData()
        {
            ListLTK = new ObservableCollection<LOAITIETKIEM>(DataProvider.Ins.DB.LOAITIETKIEMs);
            ListThamSo = new ObservableCollection<THAMSO>(DataProvider.Ins.DB.THAMSOes);
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
                if (string.IsNullOrEmpty(SoTienDuocRut))
                {
                    return false;
                }

                var maLoai = DataProvider.Ins.DB.LOAITIETKIEMs.Where(x => x.MaLoaiTietKiem == MaLoaiTietKiem);
                if (maLoai == null || maLoai.Count() != 0)
                    return false;
                return true;
            } else if (VisibilityOfEdit == Visibility.Visible)
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
            VisibilityOfAdd = Visibility.Hidden;
            VisibilityOfEdit = Visibility.Hidden;

            LoadData();                 

            // show elements used for adding
            AddLTKCommand = new RelayCommand<object>((p) => { return true; }, 
                (p) => { 
                    VisibilityOfAdd = Visibility.Visible;
                    VisibilityOfEdit = Visibility.Hidden;

                    // reset value for textbox because these textbox still keep value if you are editing and then change to add
                    ThoiGianGuiToiThieu = "";
                    LaiSuat = "";
                }
            );

            // show elements used for editing
            EditLTKCommand = new RelayCommand<object>((p) => { return true; },
                (p) => {
                    if(SelectedItemLTK != null)
                    {
                        ThoiGianGuiToiThieu = SelectedItemLTK.ThoiGianGuiToiThieu.ToString();
                        LaiSuat = SelectedItemLTK.LaiSuat.ToString();


                        VisibilityOfEdit = Visibility.Visible;
                        VisibilityOfAdd = Visibility.Hidden;
                    }
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
                        var loaiTietKiem = new LOAITIETKIEM()
                        {
                            MaLoaiTietKiem = MaLoaiTietKiem,
                            TenLoaiTietKiem = TenLoaiTietKiem,
                            KyHan = Int32.Parse(KyHan),
                            LaiSuat = float.Parse(LaiSuat),
                            ThoiGianGuiToiThieu = Int32.Parse(ThoiGianGuiToiThieu),
                            SoTienDuocRut = Int32.Parse(SoTienDuocRut)
                        };
                        DataProvider.Ins.DB.LOAITIETKIEMs.Add(loaiTietKiem);
                        DataProvider.Ins.DB.SaveChanges();

                        ListLTK.Add(loaiTietKiem);
                    } else if(VisibilityOfEdit == Visibility.Visible)
                    {
                        var temp = SelectedItemLTK;
                        var loaiTietKiem = DataProvider.Ins.DB.LOAITIETKIEMs.Where(x => x.MaLoaiTietKiem == SelectedItemLTK.MaLoaiTietKiem).SingleOrDefault();
                        loaiTietKiem.LaiSuat = float.Parse(LaiSuat);
                        loaiTietKiem.ThoiGianGuiToiThieu = Int32.Parse(ThoiGianGuiToiThieu);
                        DataProvider.Ins.DB.SaveChanges();

                        //find index of selected item in list, then remove and add the changed item
                        int length = ListLTK.Count();
                        for( int i=0; i<length; i++)
                        {
                            if( ListLTK[i].MaLoaiTietKiem == SelectedItemLTK.MaLoaiTietKiem)
                            {
                                ListLTK.RemoveAt(i);
                                ListLTK.Insert(i, loaiTietKiem);
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
                    MaLoaiTietKiem = "";
                    TenLoaiTietKiem = "";
                    KyHan = "";
                    LaiSuat = "";
                    ThoiGianGuiToiThieu = "";
                    SoTienDuocRut = "";
                }
                else if (VisibilityOfEdit == Visibility.Visible)
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
                //System.Windows.MessageBox.Show(SelectedItemCbb.ToString());
                //OnPropertyChanged("SelectedItemCbb");
            });
        }
    }
}
