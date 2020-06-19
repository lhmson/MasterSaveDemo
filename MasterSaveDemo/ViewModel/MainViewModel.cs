using MasterSaveDemo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;

namespace MasterSaveDemo.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region Variable
        static public DispatcherTimer _timer;

        private bool _Enable_Home;
        public bool Enable_Home
        {
            get => _Enable_Home;
            set { _Enable_Home = value; OnPropertyChanged(); }
        }

        private bool _Enable_MoSo;
        public bool Enable_MoSo
        {
            get => _Enable_MoSo;
            set { _Enable_MoSo = value; OnPropertyChanged(); }
        }

        private bool _Enable_GuiTien;
        public bool Enable_GuiTien
        {
            get => _Enable_GuiTien;
            set { _Enable_GuiTien = value; OnPropertyChanged(); }
        }

        private bool _Enable_RutTien;
        public bool Enable_RutTien
        {
            get => _Enable_RutTien;
            set { _Enable_RutTien = value; OnPropertyChanged(); }
        }

        private bool _Enable_TraCuu;
        public bool Enable_TraCuu
        {
            get => _Enable_TraCuu;
            set { _Enable_TraCuu = value; OnPropertyChanged(); }
        }

        private bool _Enable_BCDS;
        public bool Enable_BCDS
        {
            get => _Enable_BCDS;
            set { _Enable_BCDS = value; OnPropertyChanged(); }
        }

        private bool _Enable_BCMD;
        public bool Enable_BCMD
        {
            get => _Enable_BCMD;
            set { _Enable_BCMD = value; OnPropertyChanged(); }
        }

        private bool _Enable_TDQD;
        public bool Enable_TDQD
        {
            get => _Enable_TDQD;
            set { _Enable_TDQD = value; OnPropertyChanged(); }
        }

        private bool _Enable_QLNS;
        public bool Enable_QLNS
        {
            get => _Enable_QLNS;
            set { _Enable_QLNS = value; OnPropertyChanged(); }
        }

        #endregion

        public ICommand LoadedWindowCommand { get; set; }

        public bool isLoaded = false;

        #region Function
        private void Init_Button_User(NGUOIDUNG user)
        {
            Init_Button();
            ObservableCollection<PHANQUYEN> list_PhanQuyen= new ObservableCollection<PHANQUYEN>(DataProvider.Ins.DB.PHANQUYENs);
            foreach (var item in list_PhanQuyen)
                if (item.MaNhom == user.MaNhom)
                {
                    Init_Valid_Button(item.MaChucNang);
                }
        }

        private void Init_Button()
        {
            Enable_Home = Enable_GuiTien = Enable_RutTien = Enable_TraCuu = Enable_BCDS = Enable_BCMD = Enable_QLNS = _Enable_TDQD = Enable_MoSo =  false;
            Enable_Home = true;
        }

        private void Init_Valid_Button(int maChucNang)
        {
            switch (maChucNang)
            {
                case 1:
                    Enable_QLNS = true;
                    break;
                case 2:
                    Enable_MoSo = true;
                    break;
                case 3:
                    Enable_GuiTien = true;
                    break;
                case 4:
                    Enable_RutTien = true;
                    break;
                case 5:
                    Enable_TraCuu = true;
                    break;
                case 6:
                    Enable_BCDS = true;
                    break;
                case 7:
                    Enable_BCMD = true;
                    break;
                case 8:
                    Enable_TDQD = true;
                    break;
                case 9:
                    break;
            }
        }

        static public void Start_Timer()
        {
            _timer.Start();
        }
        static public void LogOut()
        {
            
        }

        #endregion
        public MainViewModel() // all main page handling goes there
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {

                //if (p == null) return;
                p.Hide(); // main view hide in login window
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
                isLoaded = true;

                //if (loginWindow.DataContext == null) return;
                //var loginVM = loginWindow.DataContext as LoginViewModel;
                //if (loginVM.isLogin)
                //{
                p.Show();
                //    LoadRemainsData(); // show remain table
                //}
                //else
                //{

                //}

                _timer = new DispatcherTimer(DispatcherPriority.Render);
                _timer.Interval = TimeSpan.FromSeconds(1);
                _timer.Tick += (sender, args) =>
                {
                    if (LoginViewModel.TaiKhoanSuDung != null)
                    {
                        Init_Button_User(LoginViewModel.TaiKhoanSuDung);
                        _timer.Stop();
                    }
                };
                _timer.Start();
            });
        }

    }
}
