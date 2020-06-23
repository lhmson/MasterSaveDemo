using MasterSaveDemo.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MasterSaveDemo.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private Page _FrameContent;

        public Page FrameContent
        { 
            get { return _FrameContent; }
            set { _FrameContent = value; OnPropertyChanged(); }
        }

        #region ICommand
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand Home_Page_SelectedCommand { get; set; }
        public ICommand MoSo_Page_SelectedCommand { get; set; }
        public ICommand GuiTien_Page_SelectedCommand { get; set; }
        public ICommand RutTien_Page_SelectedCommand { get; set; }
        public ICommand TraCuu_Page_SelectedCommand { get; set; }
        public ICommand BaoCaoDoanhSo_Page_SelectedCommand { get; set; }
        public ICommand BaoCaoMoDong_Page_SelectedCommand { get; set; }
        public ICommand ThayDoiQuyDinh_Page_SelectedCommand { get; set; }
        public ICommand QuanLyNhanSu_Page_SelectedCommand { get; set; }
        public ICommand CaiDatKhac_Page_SelectedCommand { get; set; }
        #endregion


        public bool isLoaded = false;

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
                FrameContent = new Home_Page();

            });

            Home_Page_SelectedCommand = new RelayCommand<HamburgerMenu.HamburgerMenu>((p) => { return true; }, (p) => {
                FrameContent = new Home_Page();
                FrameContent.DataContext = new Home_PageViewModel();
            });

            MoSo_Page_SelectedCommand = new RelayCommand<HamburgerMenu.HamburgerMenu>((p) => { return true; }, (p) => {
                FrameContent = new MoSo_Page();
                //FrameContent.DataContext = new MoSo_ViewModel();
            });

            GuiTien_Page_SelectedCommand = new RelayCommand<HamburgerMenu.HamburgerMenu>((p) => { return true; }, (p) => {
                FrameContent = new GuiTien_Page();
                FrameContent.DataContext = new GuiTien_ViewModel();
            });

            RutTien_Page_SelectedCommand = new RelayCommand<HamburgerMenu.HamburgerMenu>((p) => { return true; }, (p) => {
                FrameContent = new RutTien_Page();
                FrameContent.DataContext = new RutTien_ViewModel();
            });
            TraCuu_Page_SelectedCommand = new RelayCommand<HamburgerMenu.HamburgerMenu>((p) => { return true; }, (p) => {
                FrameContent = new TraCuu_Page();
                FrameContent.DataContext = new TraCuu_ViewModel();
            });
            BaoCaoDoanhSo_Page_SelectedCommand = new RelayCommand<HamburgerMenu.HamburgerMenu>((p) => { return true; }, (p) => {
                FrameContent = new BaoCaoDoanhSo_Page();
                FrameContent.DataContext = new BaoCaoDoanhSo_ViewModel();
            });
            BaoCaoMoDong_Page_SelectedCommand = new RelayCommand<HamburgerMenu.HamburgerMenu>((p) => { return true; }, (p) => {
                FrameContent = new BaoCaoMoDong_Page();
                FrameContent.DataContext = new BaoCaoMoDong_ViewModel();
            });
            ThayDoiQuyDinh_Page_SelectedCommand = new RelayCommand<HamburgerMenu.HamburgerMenu>((p) => { return true; }, (p) => {
                FrameContent = new ThayDoiQuyDinh_Page();
                FrameContent.DataContext = new ThayDoiQuyDinh_ViewModel();
            });
            QuanLyNhanSu_Page_SelectedCommand = new RelayCommand<HamburgerMenu.HamburgerMenu>((p) => { return true; }, (p) => {
                FrameContent = new QuanLyNhanSu_Page();
                FrameContent.DataContext = new QuanLyNhanSu_ViewModel();
            });
            CaiDatKhac_Page_SelectedCommand = new RelayCommand<HamburgerMenu.HamburgerMenu>((p) => { return true; }, (p) => {
                FrameContent = new CaiDatKhac_Page();
                //FrameContent.DataContext = new ThayDoiQuyDinh_ViewModel();
            });
        }

    }
}
