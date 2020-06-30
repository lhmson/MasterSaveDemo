using MasterSaveDemo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

        private ObservableCollection<string> _ListQuyDinh;
        public ObservableCollection<string> ListQuyDinh
        {
            get => _ListQuyDinh;
            set { _ListQuyDinh = value; OnPropertyChanged(); }
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

        private string _SelectedQuyDinh;
        public string SelectedQuyDinh
        {
            get => _SelectedQuyDinh;
            set { _SelectedQuyDinh = value; OnPropertyChanged(); }
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

        // Visibility of confirm button
        private Visibility _VisibilityOfConfirm;
        public Visibility VisibilityOfConfirm
        {
            get => _VisibilityOfConfirm;
            set { _VisibilityOfConfirm = value; OnPropertyChanged(); }
        }

        // Visibility of cancel button
        private Visibility _VisibilityOfCancel;
        public Visibility VisibilityOfCancel
        {
            get => _VisibilityOfCancel;
            set { _VisibilityOfCancel = value; OnPropertyChanged(); }
        }

        // Visibility of popup box for row1 (maloaitietkiem, thoigianguitoithieu, tenthamso)
        private Visibility _VisibilityPopup1;
        public Visibility VisibilityPopup1
        {
            get => _VisibilityPopup1;
            set { _VisibilityPopup1 = value; OnPropertyChanged(); }
        }

        // Visibility of popup box for row2 ()
        private Visibility _VisibilityPopup2;
        public Visibility VisibilityPopup2
        {
            get => _VisibilityPopup2;
            set { _VisibilityPopup2 = value; OnPropertyChanged(); }
        }

        // Visibility of popup box for row3 ()
        private Visibility _VisibilityPopup3;
        public Visibility VisibilityPopup3
        {
            get => _VisibilityPopup3;
            set { _VisibilityPopup3 = value; OnPropertyChanged(); }
        }

        // Visibility of popup box for row4 ()
        private Visibility _VisibilityPopup4;
        public Visibility VisibilityPopup4
        {
            get => _VisibilityPopup4;
            set { _VisibilityPopup4 = value; OnPropertyChanged(); }
        }

        // Visibility of popup box for row5 ()
        private Visibility _VisibilityPopup5;
        public Visibility VisibilityPopup5
        {
            get => _VisibilityPopup5;
            set { _VisibilityPopup5 = value; OnPropertyChanged(); }
        }

        // Visibility of popup box for row6 ()
        private Visibility _VisibilityPopup6;
        public Visibility VisibilityPopup6
        {
            get => _VisibilityPopup6;
            set { _VisibilityPopup6 = value; OnPropertyChanged(); }
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
        private string _GiaTri;
        public string GiaTri
        {
            get => _GiaTri;
            set { _GiaTri = value; OnPropertyChanged(); }
        }

        //
        private string _NameOfList;
        public string NameOfList
        {
            get => _NameOfList;
            set { _NameOfList = value; OnPropertyChanged(); }
        }

        private bool _TextBoxReadOnly;
        public bool TextBoxReadOnly
        {
            get => _TextBoxReadOnly;
            set { _TextBoxReadOnly = value; OnPropertyChanged(); }
        }

        private bool _HitTestVisibleCbb;
        public bool HitTestVisibleCbb
        {
            get => _HitTestVisibleCbb;
            set { _HitTestVisibleCbb = value; OnPropertyChanged(); }
        }

        private string _PopupContent1;
        public string PopupContent1
        {
            get => _PopupContent1;
            set { _PopupContent1 = value; OnPropertyChanged(); }
        }

        private string _PopupContent2;
        public string PopupContent2
        {
            get => _PopupContent2;
            set { _PopupContent2 = value; OnPropertyChanged(); }
        }

        private string _PopupContent3;
        public string PopupContent3
        {
            get => _PopupContent3;
            set { _PopupContent3 = value; OnPropertyChanged(); }
        }

        private string _PopupContent4;
        public string PopupContent4
        {
            get => _PopupContent4;
            set { _PopupContent4 = value; OnPropertyChanged(); }
        }

        private string _PopupContent5;
        public string PopupContent5
        {
            get => _PopupContent5;
            set { _PopupContent5 = value; OnPropertyChanged(); }
        }

        private string _PopupContent6;
        public string PopupContent6
        {
            get => _PopupContent6;
            set { _PopupContent6 = value; OnPropertyChanged(); }
        }

        #endregion

        #region Function
        private void HiddenPopupBox()
        {
            VisibilityPopup1 = Visibility.Hidden;
            VisibilityPopup2 = Visibility.Hidden;
            VisibilityPopup3 = Visibility.Hidden;
            VisibilityPopup4 = Visibility.Hidden;
            VisibilityPopup5 = Visibility.Hidden;
            VisibilityPopup6 = Visibility.Hidden;
        }
        private void LoadData()
        {
            var listLTK_Using = DataProvider.Ins.DB.LOAITIETKIEMs;

            ListLTK = new ObservableCollection<LOAITIETKIEM>(listLTK_Using);
            ListThamSo = new ObservableCollection<THAMSO>(DataProvider.Ins.DB.THAMSOes);

            ListQuyDinh = new ObservableCollection<string>();
            ListQuyDinh.Add("0");
            ListQuyDinh.Add("1");

            VisibilityOfAdd = Visibility.Hidden;
            VisibilityOfEditLTK = Visibility.Hidden;
            VisibilityOfEditThamSo = Visibility.Hidden;
            VisibilityOfListThamSo = Visibility.Hidden;
            VisibilityOfConfirm = VisibilityOfCancel = Visibility.Hidden;

            HiddenPopupBox();

            // choosing list LTK
            SelectedIndexCbb = 0;
            NameOfList = "Danh sách loại tiết kiệm";
        }
        private bool CheckValidData()
        {
            bool flag = true;
            if (VisibilityOfAdd == Visibility.Visible)
            {
                //if (string.IsNullOrEmpty(MaLoaiTietKiem))
                //{
                    
                //}
                if (string.IsNullOrEmpty(TenLoaiTietKiem))
                {
                    VisibilityPopup2 = Visibility.Visible;
                    PopupContent2 = "Chưa nhập tên loại tiết kiệm";
                    flag = false;
                }
                // check if null or not a number, return false
                if (string.IsNullOrEmpty(KyHan))
                {
                    VisibilityPopup3 = Visibility.Visible;
                    PopupContent3 = "Chưa nhập kỳ hạn";
                    flag = false;
                }
                if (string.IsNullOrEmpty(LaiSuat))
                {
                    VisibilityPopup4 = Visibility.Visible;
                    PopupContent4 = "Chưa nhập lãi suất";
                    flag = false;
                }
                if (string.IsNullOrEmpty(ThoiGianGuiToiThieu))
                {
                    VisibilityPopup5 = Visibility.Visible;
                    PopupContent5 = "Chưa nhập thời gian gửi tối thiểu";
                    flag = false;
                }
                if (SelectedQuyDinh == null)
                {
                    VisibilityPopup6 = Visibility.Visible;
                    PopupContent6 = "Chưa chọn quy định về số tiền rút";
                    flag = false;
                }

                if ( !IsDeleting)
                {
                    var maLoai = DataProvider.Ins.DB.LOAITIETKIEMs.Where(x => x.MaLoaiTietKiem == MaLoaiTietKiem);
                    var tenLoai = DataProvider.Ins.DB.LOAITIETKIEMs.Where(x => x.TenLoaiTietKiem == TenLoaiTietKiem);

                    if (maLoai.Count() != 0)
                    {
                        VisibilityPopup1 = Visibility.Visible;
                        PopupContent1 = "Mã loại tiết kiệm đã tồn tại";
                        flag = false;
                    }
                    if(tenLoai.Count() != 0)
                    {
                        VisibilityPopup2 = Visibility.Visible;
                        PopupContent2 = "Tên loại tiết kiệm đã tồn tại";
                        flag = false;
                    }
                }
                return flag;
            } 
            else if (VisibilityOfEditLTK == Visibility.Visible)
            {
                if (string.IsNullOrEmpty(LaiSuat))
                {
                    VisibilityPopup2 = Visibility.Visible;
                    PopupContent2 = "Chưa nhập lãi suất mới";
                    flag = false;
                }
                if (string.IsNullOrEmpty(ThoiGianGuiToiThieu))
                {
                    VisibilityPopup1 = Visibility.Visible;
                    PopupContent1 = "Chưa nhập thời gian gửi tối thiểu mới";
                    flag = false;
                }

                // check if more than 1 value are the same
                double laiSuat = double.Parse(LaiSuat);
                int thoiGianGuiToiThieu = int.Parse(ThoiGianGuiToiThieu);
                if (laiSuat == SelectedItemLTK.LaiSuat && thoiGianGuiToiThieu == SelectedItemLTK.ThoiGianGuiToiThieu)
                {
                    VisibilityPopup1 = Visibility.Visible;
                    PopupContent1 = "Thời gian gửi tối thiểu đang được áp dụng";

                    VisibilityPopup2 = Visibility.Visible;
                    PopupContent2 = "Lãi suất đang được áp dụng";

                    flag = false;
                }
                return flag;
            }
            else if (VisibilityOfEditThamSo == Visibility.Visible)
            {
                if (string.IsNullOrEmpty(TenThamSo))
                {
                    VisibilityPopup1 = Visibility.Visible;
                    PopupContent1 = "Chưa nhập tên tham số";
                    flag = false;
                }
                if (string.IsNullOrEmpty(GiaTri))
                {
                    VisibilityPopup2 = Visibility.Visible;
                    PopupContent2 = "Chưa nhập giá trị của tham số";
                    flag = false;
                }

                decimal giaTri = decimal.Parse(GiaTri);
                if (TenThamSo == SelectedItemThamSo.TenThamSo && giaTri == SelectedItemThamSo.GiaTri)
                {
                    VisibilityPopup1 = Visibility.Visible;
                    PopupContent1 = "Tên tham số đang được áp dụng";

                    VisibilityPopup2 = Visibility.Visible;
                    PopupContent2 = "Giá trị của tham số đang được áp dụng";

                    flag = false;
                }
                return flag;
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
            SelectedQuyDinh = null;
        }
        private void SetTextboxValue()
        {
            MaLoaiTietKiem = SelectedItemLTK.MaLoaiTietKiem;
            TenLoaiTietKiem = SelectedItemLTK.TenLoaiTietKiem;
            KyHan = SelectedItemLTK.KyHan.ToString();
            LaiSuat = SelectedItemLTK.LaiSuat.ToString();
            ThoiGianGuiToiThieu = SelectedItemLTK.ThoiGianGuiToiThieu.ToString();
            SelectedQuyDinh = SelectedItemLTK.QuyDinhSoTienRut.ToString();
        }
        private void AddLTK()
        {
            var loaiTietKiem = new LOAITIETKIEM()
            {
                MaLoaiTietKiem = MaLoaiTietKiem,
                TenLoaiTietKiem = TenLoaiTietKiem,
                KyHan = Int32.Parse(KyHan),
                LaiSuat = double.Parse(LaiSuat),
                ThoiGianGuiToiThieu = Int32.Parse(ThoiGianGuiToiThieu),
                QuyDinhSoTienRut = Int32.Parse(SelectedQuyDinh),
                DangSuDung = 1
            };
            DataProvider.Ins.DB.LOAITIETKIEMs.Add(loaiTietKiem);
            DataProvider.Ins.DB.SaveChanges();

            ListLTK.Add(loaiTietKiem);
        }
        private void EditLTK()
        {
            SelectedItemLTK.LaiSuat = float.Parse(LaiSuat);
            SelectedItemLTK.ThoiGianGuiToiThieu = Int32.Parse(ThoiGianGuiToiThieu);
            DataProvider.Ins.DB.SaveChanges();
            var temp = SelectedItemLTK;

            //find index of selected item in list, then remove and add the changed item
            int length = ListLTK.Count();
            for (int i = 0; i < length; i++)
            {
                if (ListLTK[i].MaLoaiTietKiem == SelectedItemLTK.MaLoaiTietKiem)
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
        private void EditThamSo()
        {
            SelectedItemThamSo.TenThamSo = TenThamSo;
            SelectedItemThamSo.GiaTri = Decimal.Parse(GiaTri);
            DataProvider.Ins.DB.SaveChanges();

            var temp = SelectedItemThamSo;

            //find index of selected item in list, then remove and add the changed item
            int length = ListThamSo.Count();
            for (int i = 0; i < length; i++)
            {
                if (ListThamSo[i].TenThamSo == SelectedItemThamSo.TenThamSo)
                {
                    ListThamSo.RemoveAt(i);
                    ListThamSo.Insert(i, temp);
                    break;
                }
            }

            //After confirming, selected item will die huhu, this line is used for making selected item reborn. 
            //You can continue change value without choosing item again if unnecessary
            SelectedItemThamSo = temp;
        }
        private void DeleteLTK()
        {
            // Count SoTietKiem using SelectedLTK as LoaiTietKiem
            List<SOTIETKIEM> list = DataProvider.Ins.DB.SOTIETKIEMs.ToList();
            var listSTK = from stk in list
                          where stk.MaLoaiTietKiem == SelectedItemLTK.MaLoaiTietKiem
                          select stk;

            // if not used by any STKs
            if( listSTK.Count() == 0)
            {
                SelectedItemLTK.DangSuDung = 0;
                DataProvider.Ins.DB.SaveChanges();

                var temp = SelectedItemLTK;
                for (int i = 0; i < ListLTK.Count(); i++)
                {
                    if (ListLTK[i].MaLoaiTietKiem == SelectedItemLTK.MaLoaiTietKiem)
                    {
                        ListLTK.RemoveAt(i);
                        ListLTK.Insert(i, temp);
                        break;
                    }
                }
                ResetTextbox();
            }
            else
            {
                System.Windows.MessageBox.Show("Can't");
            }
        }
        private string Create_MaLTK(int stt)
        {
            string res = "LTK";
            for (int i = 4; i <= 6 - stt.ToString().Length; i++)
                res += '0';
            res += stt.ToString();
            return res;
        }
        private int DisableButton(Visibility add, Visibility edit, bool delete)
        {
            if (edit == Visibility.Visible)
                return 13;
            else if (add == Visibility.Visible && delete == false)
                return 23;
            else if (add == Visibility.Visible && delete == true)
                return 12;
            return 0;
        }
        public void ResetAll()
        {
            VisibilityOfAdd = VisibilityOfEditLTK = Visibility.Hidden;
            VisibilityOfEditThamSo = Visibility.Hidden;
            VisibilityOfConfirm = VisibilityOfCancel = Visibility.Hidden;
            HiddenPopupBox();

            IsDeleting = false;
            SelectedItemLTK = null;
            SelectedItemThamSo = null;
            TextBoxReadOnly = false;
            HitTestVisibleCbb = true;
        }
        
        #endregion

        #region ICommand
        public ICommand AddLTKCommand { get; set; }
        public ICommand EditLTKCommand { get; set; }
        public ICommand DeleteLTKCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand CbbSelectionChangedCommand { get; set; }
        public ICommand GetThoiGianGuiToiThieuCommand { get; set; }

        #endregion

        public ThayDoiQuyDinh_ViewModel()
        {
            LoadData();                 

            // show elements used for adding
            AddLTKCommand = new RelayCommand<object>((p) => 
                {
                    if (SelectedIndexCbb == 0)
                    {
                        int disable = DisableButton(VisibilityOfAdd, VisibilityOfEditLTK, IsDeleting);
                        if (disable == 23 || disable == 0)
                            return true;
                        return false;
                    }
                    return false;
                    //return true;
                }, 
                (p) => {
                    IsDeleting = false;
                    HitTestVisibleCbb = true;
                    VisibilityOfAdd = Visibility.Visible;
                    VisibilityOfEditLTK = Visibility.Hidden;
                    VisibilityOfConfirm = VisibilityOfCancel = Visibility.Visible;
                    HiddenPopupBox();
                    SelectedItemLTK = null;

                    // reset value for textbox because these textbox still keep value if you are editing and then change to add
                    ResetTextbox();

                    // auto create MaLoaiTietKiem
                    int index = ListLTK.Count() + 1;
                    MaLoaiTietKiem = Create_MaLTK(index);
                }
            );

            // show elements used for editing
            EditLTKCommand = new RelayCommand<object>((p) => 
                {
                    if(SelectedIndexCbb == 0)
                    {
                        if (SelectedItemLTK != null && SelectedItemLTK.DangSuDung == 1)
                        {
                            int disable = DisableButton(VisibilityOfAdd, VisibilityOfEditLTK, IsDeleting);
                            if (disable == 13 || disable == 0)
                                return true;
                            return false;
                        }
                        return false;
                    }
                    if (SelectedItemThamSo != null)
                        return true;
                    return false;
                },
                (p) => {
                    HiddenPopupBox();
                    if ( SelectedIndexCbb == 0)
                    {
                        if (SelectedItemLTK != null)
                        {
                            VisibilityOfConfirm = VisibilityOfCancel = Visibility.Visible;
                            ThoiGianGuiToiThieu = SelectedItemLTK.ThoiGianGuiToiThieu.ToString();
                            LaiSuat = SelectedItemLTK.LaiSuat.ToString();

                            IsDeleting = false;
                            VisibilityOfEditLTK = Visibility.Visible;
                            VisibilityOfAdd = Visibility.Hidden;
                        }
                    }
                    else
                    {
                        if( SelectedItemThamSo != null)
                        {
                            VisibilityOfConfirm = VisibilityOfCancel = Visibility.Visible;
                            TenThamSo = SelectedItemThamSo.TenThamSo;
                            GiaTri = SelectedItemThamSo.GiaTri.ToString();

                            VisibilityOfEditThamSo = Visibility.Visible;
                        }
                    }
                }
            );

            // show elements used for deleting
            DeleteLTKCommand = new RelayCommand<object>((p) => 
                {
                    if (SelectedIndexCbb == 0)
                    {
                        if( SelectedItemLTK != null && SelectedItemLTK.DangSuDung == 1)
                        {
                            int disable = DisableButton(VisibilityOfAdd, VisibilityOfEditLTK, IsDeleting);
                            if (disable == 12 || disable == 0)
                                return true;
                            return false;
                        }
                        return false;
                    }
                    return false;
                    //return true;
                },
                (p) => {
                    if(SelectedItemLTK != null)
                    {
                        VisibilityOfConfirm = VisibilityOfCancel = Visibility.Visible;

                        IsDeleting = true;
                        SetTextboxValue();
                        TextBoxReadOnly = true;
                        HitTestVisibleCbb = false;
                        VisibilityOfAdd = Visibility.Visible;
                        VisibilityOfEditLTK = Visibility.Hidden;
                        HiddenPopupBox();
                    }
                }
            );

            // Button: Confirm for adding LOAITIETKIEM
            ConfirmCommand = new RelayCommand<object>((p) => 
            {
                return true;

            }, (p) => 
            {
                try
                {
                    HiddenPopupBox();
                    if (VisibilityOfAdd == Visibility.Visible)
                    {
                        if(!IsDeleting)
                        {
                            if(CheckValidData())
                            {
                                AddLTK();
                                ResetAll();
                            }
                        }
                        else
                        {
                            DeleteLTK();
                            ResetAll();
                        }
                    } 
                    else if(VisibilityOfEditLTK == Visibility.Visible)
                    {
                        if(CheckValidData())
                        {
                            EditLTK();
                            ResetAll();
                        }
                    }
                    else if (VisibilityOfEditThamSo == Visibility.Visible)
                    {
                        if (CheckValidData())
                        {
                            EditThamSo();
                            ResetAll();
                        }
                    }
                }
                catch (Exception e) { };
            });

            CancelCommand = new RelayCommand<object>((p) =>
            {                
                return true;
            }, (p) =>
            {
                ResetAll();
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
                    SelectedItemLTK = null;

                    VisibilityOfListLTK = Visibility.Visible;
                    VisibilityOfListThamSo = Visibility.Hidden;

                    VisibilityOfEditThamSo = Visibility.Hidden;
                    VisibilityOfConfirm = VisibilityOfCancel = Visibility.Hidden;
                    HiddenPopupBox();
                } else
                {
                    NameOfList = "Danh sách tham số";
                    SelectedItemThamSo = null;

                    VisibilityOfListThamSo = Visibility.Visible;
                    VisibilityOfListLTK = Visibility.Hidden;

                    VisibilityOfAdd = VisibilityOfEditLTK = Visibility.Hidden;
                    VisibilityOfConfirm = VisibilityOfCancel = Visibility.Hidden;
                    HiddenPopupBox();
                }

            });
            
            GetThoiGianGuiToiThieuCommand = new RelayCommand<object>((p) =>
            {
                return !IsDeleting; // disable button when deleting
            }, (p) =>
            {
                ThoiGianGuiToiThieu = KyHan;
            });
        }
    }
}
