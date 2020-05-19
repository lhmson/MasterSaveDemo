using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using MasterSaveDemo.Model;

namespace MasterSaveDemo.ViewModel
{
    public class RutTien_ViewModel : BaseViewModel
    {
		//Khai bao cac command
		public ICommand Click_MSTKCommand { get; set; }
		public ICommand MSTK_TextChangedCommand { get; set; }
		#region Biding tu view
		//Ngay Rut, kieu string
		private string _NgayRut;
		public string NgayRut
		{
			get => _NgayRut;
			set { _NgayRut = value; OnPropertyChanged();}
		}
		//Ma so tiet kiem
		private string _MaSoTietKiem;

		public string MaSoTietKiem
		{
			get { return _MaSoTietKiem; }
			set { _MaSoTietKiem = value; OnPropertyChanged();}
		}
		//Ten Khach hang
		private string _TenKhachHang;

		public string TenKhachHang
		{
			get { return _TenKhachHang; }
			set { _TenKhachHang = value; OnPropertyChanged(); }
		}
		//So Tien Rut
		private string _SoTienRut;

		public string SoTienRut
		{
			get { return _SoTienRut; }
			set { _SoTienRut = value; OnPropertyChanged(); }
		}
		//Ten loai tiet kiem
		private string _TenLoaiTietKiem;

		public string TenLoaiTietKiem
		{
			get { return _TenLoaiTietKiem; }
			set { _TenLoaiTietKiem = value; OnPropertyChanged(); }
		}
		//So du tai khoan
		private string _SoDu;

		public string SoDu
		{
			get { return _SoDu; }
			set { _SoDu = value; OnPropertyChanged(); }
		}
		//CMND
		private string _CMND;

		public string CMND
		{
			get { return _CMND; }
			set { _CMND = value; OnPropertyChanged(); }
		}
		//Ngay dao han ke tiep
		private string _NgayDaoHan;

		public string NgayDaoHan
		{
			get { return _NgayDaoHan; }
			set { _NgayDaoHan = value; OnPropertyChanged(); }
		}

        #endregion
        #region Khoi tao
        //Khoi tao viewmodel
        public RutTien_ViewModel()
		{
			//Dat ngay rut theo ngay hom nay
			NgayRut = DateTime.Today.ToString("dd/MM/yyyy");
			//Khi click vao nut ben canh MSTK
			Click_MSTKCommand = new RelayCommand<Window>((p)=> { return true; },(p) =>
			{
				try
				{
					SOTIETKIEM temp = Tim_MSTK(MaSoTietKiem);
					if (temp != null)
					{
						TenLoaiTietKiem = Tim_MaLoaiTietKiem(temp.MaLoaiTietKiem).TenLoaiTietKiem;
						TenKhachHang = temp.TenKhachHang;
						SoDu = temp.SoDu.ToString("0,000");
						CMND = temp.SoCMND;
						NgayDaoHan = temp.NgayDaoHanKeTiep.ToString("dd/MM/yyyy");
					}
				}
				catch (Exception e)
				{

				}
			});
			MSTK_TextChangedCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
			{
				try
				{
					TenKhachHang = "";
					SoDu = "";
					CMND = "";
					TenLoaiTietKiem = "";
					NgayDaoHan = "";
				}
				catch (Exception e)
				{

				}
			});
		}
		#endregion
		#region Cac ham xu li database
		//lay ra so tiet kiem khi biet Ma so tiet kiem 
		private SOTIETKIEM Tim_MSTK(string mstk)
		{
			List<SOTIETKIEM> List_SoTietKiem = DataProvider.Ins.DB.SOTIETKIEMs.ToList();
			foreach (SOTIETKIEM stk in List_SoTietKiem)
			{
				if (stk.MaSoTietKiem == mstk)
					return stk;
			}
			return null;
		}
		//lay ra loai tiet kiem tu ma loai tiet kiem
		private LOAITIETKIEM Tim_MaLoaiTietKiem(string mltk)
		{
			List<LOAITIETKIEM> List_LoaiTietKiem = DataProvider.Ins.DB.LOAITIETKIEMs.ToList();
			foreach (LOAITIETKIEM ltk in List_LoaiTietKiem)
			{
				if (ltk.MaLoaiTietKiem == mltk)
					return ltk;
			}
			return null;
		}
        #endregion
    }
}
