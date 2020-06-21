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
        #region Management Page Selections

        private Home_Page HomePage = new Home_Page();
        private MoSo_Page MoSoPage = new MoSo_Page();
        private GuiTien_Page GuiTienPage = new GuiTien_Page();
        private RutTien_Page RutTienPage = new RutTien_Page();
        private BaoCaoDoanhSo_Page BaoCaoDoanhSoPage = new BaoCaoDoanhSo_Page();
        private BaoCaoMoDong_Page BaoCaoMoDongPage = new BaoCaoMoDong_Page();
        private ThayDoiQuyDinh_Page ThayDoiQuyDinhPage = new ThayDoiQuyDinh_Page();
        private QuanLyNhanSu_Page QuanLyNhanSuPage = new QuanLyNhanSu_Page();
        private CaiDatKhac_Page CaiDatKhacPage = new CaiDatKhac_Page();
        private TraCuu_Page TraCuuPage = new TraCuu_Page();
        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Home_Page_Selected(object sender, RoutedEventArgs e)
        {
            main.Content = HomePage;
        }

        private void MoSo_Page_Selected(object sender, RoutedEventArgs e)
        {
            main.Content = MoSoPage;
        }

        private void GuiTien_Page_Selected(object sender, RoutedEventArgs e)
        {

            if (main.Content is MoSo_Page)
            {
                var context = (MoSo_ViewModel)MoSoPage.DataContext;
                context.reset_changepage();
            }

            GuiTien_Page page = new GuiTien_Page();
            main.Content = page;
        }

        private void RutTien_Page_Selected(object sender, RoutedEventArgs e)
        {
            main.Content = RutTienPage;
        }

        private void TraCuu_Page_Selected(object sender, RoutedEventArgs e)
        {
            main.Content = TraCuuPage;
        }

        private void BaoCaoDoanhSo_Page_Selected(object sender, RoutedEventArgs e)
        {
            main.Content = BaoCaoDoanhSoPage;
        }

        private void BaoCaoMoDong_Page_Selected(object sender, RoutedEventArgs e)
        {
            main.Content = BaoCaoMoDongPage;
        }

        private void ThayDoiQuyDinh_Page_Selected(object sender, RoutedEventArgs e)
        {
            main.Content = ThayDoiQuyDinhPage;
        }

        private void QuanLyNhanSu_Page_Selected(object sender, RoutedEventArgs e)
        {
            main.Content = QuanLyNhanSuPage;
        }

        private void CaiDatKhac_Page_Selected(object sender, RoutedEventArgs e)
        {
            main.Content = CaiDatKhacPage;
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
