using MasterSaveDemo.View;
using MasterSaveDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MasterSaveDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Home_Page_Selected(object sender, RoutedEventArgs e)
        {
            Home_Page page = new Home_Page();
            main.Content = page;
        }

        private void MoSo_Page_Selected(object sender, RoutedEventArgs e)
        {
            MoSo_Page page = new MoSo_Page();
            main.Content = page;
        }

        private void GuiTien_Page_Selected(object sender, RoutedEventArgs e)
        {
            GuiTien_Page page = new GuiTien_Page();
            main.Content = page;
        }

        private void RutTien_Page_Selected(object sender, RoutedEventArgs e)
        {
            RutTien_Page page = new RutTien_Page();
            main.Content = page;
        }

        private void TraCuu_Page_Selected(object sender, RoutedEventArgs e)
        {
            TraCuu_Page page = new TraCuu_Page();
            main.Content = page;
        }

        private void BaoCaoDoanhSo_Page_Selected(object sender, RoutedEventArgs e)
        {
            BaoCaoDoanhSo_Page page = new BaoCaoDoanhSo_Page();
            main.Content = page;
        }

        private void BaoCaoMoDong_Page_Selected(object sender, RoutedEventArgs e)
        {
            BaoCaoMoDong_Page page = new BaoCaoMoDong_Page();
            main.Content = page;
        }

        private void ThayDoiQuyDinh_Page_Selected(object sender, RoutedEventArgs e)
        {
            ThayDoiQuyDinh_Page page = new ThayDoiQuyDinh_Page();
            main.Content = page;
        }

        private void QuanLyNhanSu_Page_Selected(object sender, RoutedEventArgs e)
        {
            QuanLyNhanSu_Page page = new QuanLyNhanSu_Page();
            main.Content = page;
        }

        private void CaiDatKhac_Page_Selected(object sender, RoutedEventArgs e)
        {
            CaiDatKhac_Page page = new CaiDatKhac_Page();
            main.Content = page;
        }

        private void Logout_Button_Selected(object sender, RoutedEventArgs e)
        {
            DialogResult kq = System.Windows.Forms.MessageBox.Show("Bạn có chắc đăng xuất tài khoản này không?", "Đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == System.Windows.Forms.DialogResult.No)
                return;

            this_.Hide();
            LoginWindow loginWindow = new LoginWindow();
            LoginViewModel.TaiKhoanSuDung = null;
            loginWindow.ShowDialog();
            MainViewModel.Start_Timer();
            this_.Show();
        }
    }
}
