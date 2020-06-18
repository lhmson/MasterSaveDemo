using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using MasterSaveDemo.Model;
using MasterSaveDemo.Helper;

namespace MasterSaveDemo.ViewModel
{
    public class RutTien_ViewModel : BaseViewModel
	{
		//Bien chua ket qua kiem tra hop le
		private bool Result_KiemTraHopLe;
		private bool Result_KiemTraNhapLai; //true = da nhap lai moi nhat, false = chua nhap lai moi nhat
		//Khai bao cac command
		public ICommand Click_MSTKCommand { get; set; }
		public ICommand MSTK_TextChangedCommand { get; set; }
		public ICommand KiemTraCommand { get; set; }
		public ICommand Click_GiaoDichCommand { get; set; }
		public ICommand STR_TextChangedCommand { get; set; }
		public ICommand TKH_TextChangedCommand { get; set; }
		public ICommand Click_CapNhatCommand { get; set; }
		public ICommand STKEnterKeyDown_Command { get; set; }
		public ICommand SoTienRutEnterKeyDown_Command { get; set; }
		public ICommand Click_CopySoDuSTRCommand { get; set; }

		#region Binding tu view
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
		//Thong Bao
		private string _ThongBao;

		public string ThongBao
		{
			get { return _ThongBao; }
			set { _ThongBao = value; OnPropertyChanged(); }
		}

		#endregion
		#region Khoi tao
		//Khoi tao viewmodel
		public RutTien_ViewModel()
		{
			Result_KiemTraNhapLai = true;
			//Dat ngay rut theo ngay hom nay
			NgayRut = DateTime.Today.ToString("dd/MM/yyyy");
			//Khi click vao nut ben canh MSTK
			Click_MSTKCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
			{
				if (!CapNhatThongTin())
				{
					Result_KiemTraHopLe = false;
					ThongBao = "Lấy thông tin thất bại";
				}
				else
				{
					KiemTraNhapLai();
					Result_KiemTraHopLe = false;
				}
			});
			//khi nhan enter o mo so
			STKEnterKeyDown_Command = new RelayCommand<Object>((p) => { return true; }, (p) =>
			 {
				 if (!CapNhatThongTin())
				 {
					 Result_KiemTraHopLe = false;
					 ThongBao = "Lấy thông tin thất bại";
				 }
				 else
				 {
					 KiemTraNhapLai();
					 Result_KiemTraHopLe = false;
				 }
			 });
			//khi thay doi textbox MSTK
			MSTK_TextChangedCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
			{
				try
				{
					TenKhachHang = "";
					SoDu = "";
					CMND = "";
					TenLoaiTietKiem = "";
					NgayDaoHan = "";
					ThongBao = "";
					Result_KiemTraHopLe = false;
					Result_KiemTraNhapLai = true;
				}
				catch (Exception e)
				{

				}
			});
			//Lenh kiem tra tinh hop le
			KiemTraCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
			{
				if (!KiemTraHopLe())
				{
					Result_KiemTraHopLe = false;
					Result_KiemTraNhapLai = true;
					ThongBao = "Kiểm tra thất bại.";
				}
				else
				{
					if (Result_KiemTraHopLe == true)
						ThongBao = "Thông tin phiếu rút hợp lệ";
				}
			});
			SoTienRutEnterKeyDown_Command = new RelayCommand<Object>((p) => { return true; }, (p) =>
			{
				if (!KiemTraHopLe())
				{
					Result_KiemTraHopLe = false;
					Result_KiemTraNhapLai = true;
					ThongBao = "Kiểm tra thất bại.";
				}
				else
				{
					if (Result_KiemTraHopLe == true)
						ThongBao = "Thông tin phiếu rút hợp lệ";
				}
			});
			//Lenh thuc hien giao dich
			Click_GiaoDichCommand = new RelayCommand<Button>((p) => { return Result_KiemTraHopLe; }, (p) =>
			{
				if (!ThucHienGiaoDich())
				{
					ThongBao += "Không thể thưc hiện giao dịch do lỗi không xác định.";
				}
				else
				{
					Result_KiemTraHopLe = false;
					ThongBao += "Đã tạo phiếu rút thành công.";
				}
			});
			STR_TextChangedCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
			{
				try
				{
					Result_KiemTraHopLe = false;
				}
				catch (Exception e)
				{

				}
			});
			TKH_TextChangedCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
			{
				try
				{
					Result_KiemTraHopLe = false;
				}
				catch (Exception e)
				{

				}
			});
			Click_CapNhatCommand = new RelayCommand<Button>((p) => { return !Result_KiemTraNhapLai; }, (p) =>
			{
				if (!NhapLaiVaoVon.Ins.StartThis(MaSoTietKiem, true))
				{
					ThongBao = "Có lỗi xảy ra hoặc sổ tiết kiệm này đã được nhập lãi rồi!";
				}
				else
				{
					Result_KiemTraHopLe = false;
					ThongBao = "Đã cập nhật lãi vào số dư";
					if (!CapNhatThongTin())
					{
						ThongBao = "Lấy thông tin thất bại";
					}
					else
					{
						Result_KiemTraHopLe = false;
					}
				}
			});
			Click_CopySoDuSTRCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
			{
				if(!Copy_SD_STR())
				{
					ThongBao = "Sao chép không thành công!";
				}
				else
				{

				}
			});
		}
		private bool CapNhatThongTin()
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
					KiemTraNhapLai();
					ThongBao = "Đã cập nhật thông tin sổ tiết kiệm.";
				}
				else
				{
					if (MaSoTietKiem != null)
					{
						ThongBao = "Không tìm thấy sổ tiết kiệm phù hợp!";
					}
					else
					{
						ThongBao = "Hãy nhập mã sổ tiết kiệm trước khi bấm kiểm tra!";
					}
				}
				return true;
			}
			catch (Exception e)
			{
				return false;
			}
		}
		private bool KiemTraHopLe()
		{
			ThongBao = "";
			Result_KiemTraHopLe = true;
			try
			{
				SOTIETKIEM info_stk = Tim_MSTK(MaSoTietKiem);
				LOAITIETKIEM info_loaitietkiem = Tim_MaLoaiTietKiem(info_stk.MaLoaiTietKiem);
				if(info_stk.NgayMoSo.AddDays(info_loaitietkiem.ThoiGianGuiToiThieu) > DateTime.Today )
				{
					ThongBao += "Chưa đủ số ngày gửi tối thiểu.\n";
					Result_KiemTraHopLe = false;
				}
				else
				{
					if(!KiemTraNhapLai())
					{
						ThongBao += "Lỗi, không xác định được thông tin đáo hạn.\n";
					}
					else
					{
						if(Result_KiemTraNhapLai == false)
						{
							ThongBao += "Cần nhập lãi trước khi rút.\n";
						}
					}
				}
				if (SoTienRut == null)
				{
					ThongBao += "Vui lòng nhập số tiền rút.\n";
					Result_KiemTraHopLe = false;
				}
				else
				{
					if (info_loaitietkiem.QuyDinhSoTienRut == 1 && decimal.Parse(SoTienRut) < decimal.Parse(SoDu))
					{
						ThongBao += "Loại tiết kiệm có kì hạn phải rút toàn bộ.\n";
						Result_KiemTraHopLe = false;
					}
					if (decimal.Parse(SoTienRut) > decimal.Parse(SoDu))
					{
						ThongBao += "Không thể rút nhiều hơn số dư tài khoản.\n";
						Result_KiemTraHopLe = false;
					}
				}
				return true; 
			}
			catch (Exception e)
			{
				Result_KiemTraHopLe = false;
				return false; 
			}
		}
		private bool KiemTraNhapLai()
		{
			try
			{
				Result_KiemTraNhapLai = true;
				SOTIETKIEM info_stk = Tim_MSTK(MaSoTietKiem);
				LOAITIETKIEM info_loaitietkiem = Tim_MaLoaiTietKiem(info_stk.MaLoaiTietKiem);
				if (info_stk.NgayMoSo.AddDays(info_loaitietkiem.ThoiGianGuiToiThieu) > DateTime.Today)
				{
					//khong the tinh lai do chua du so ngay gui toi thieu (xem quy dinh)
				}
				else
				{
					if (info_loaitietkiem.KyHan != 0)
					{
						if (info_stk.NgayDaoHanKeTiep <= DateTime.Today.AddDays(info_loaitietkiem.KyHan))
						{
							Result_KiemTraHopLe = false;
							Result_KiemTraNhapLai = false;
						}
					}
					else
					{
						if (info_stk.NgayDaoHanKeTiep != DateTime.Today.AddDays(1))
						{
							Result_KiemTraHopLe = false;
							Result_KiemTraNhapLai = false;
						}
					}
				}
				return true;
			}
			catch(Exception e)
			{
				Result_KiemTraNhapLai = true;
				return false;
			}
		}
		private bool ThucHienGiaoDich()
		{
			try
			{
				PHIEURUT info_PhieuRut = new PHIEURUT();
				if (DataProvider.Ins.DB.PHIEURUTs.Count() == 0)
				{
					info_PhieuRut.MaPhieuRut = "0000000001";
				}
				else
				{
					info_PhieuRut.MaPhieuRut = "0000000000";
					decimal temp_dem = DataProvider.Ins.DB.PHIEURUTs.Count();
					string temp = (temp_dem+1).ToString();
					info_PhieuRut.MaPhieuRut.Remove(0, temp.Count());
					info_PhieuRut.MaPhieuRut += temp;
				}
				info_PhieuRut.MaSoTietKiem = this.MaSoTietKiem;
				info_PhieuRut.NgayRut = DateTime.Today;
				info_PhieuRut.SoTienRut = decimal.Parse(this.SoTienRut);
				DataProvider.Ins.DB.PHIEURUTs.Add(info_PhieuRut);
				DataProvider.Ins.DB.SaveChanges();

				SOTIETKIEM stk = DataProvider.Ins.DB.SOTIETKIEMs.Where(x => x.MaSoTietKiem == this.MaSoTietKiem).SingleOrDefault();
				stk.SoDu -= decimal.Parse(this.SoTienRut);
				if(stk.SoDu<1)
				{
					stk.SoDu = 0;
				}
				DataProvider.Ins.DB.SaveChanges();

				if (stk.SoDu == 0)
				{
					DongSoTuDong(info_PhieuRut.MaSoTietKiem);
				}

				if (!CapNhatThongTin())
				{
					ThongBao = "Lấy thông tin thất bại";
				}
				else
				{
					Result_KiemTraHopLe = false;	
				}
				SoTienRut = "";
				return true;
			}
			catch(Exception e)
			{
				return false;
			}
		}
		private bool DongSoTuDong(string mstk)
		{
			try
			{
				if(GetThamSo("DongSoTuDong")==1)
				{
					SOTIETKIEM temp = DataProvider.Ins.DB.SOTIETKIEMs.Where(x => x.MaSoTietKiem == mstk).SingleOrDefault();
					temp.NgayDongSo = DateTime.Today;
					DataProvider.Ins.DB.SaveChanges();
					ThongBao = "Đã đóng sổ tiết kiệm.\n";
				}
				else
				{
					ThongBao = "Đã rút tiền thành công.\n";
				}
				return true;
			}
			catch (Exception e)
			{
				return false;
			}
		}
		private bool Copy_SD_STR()
		{
			try
			{
				SoTienRut = SoDu.Replace(",", "");
				return true;
			}
			catch(Exception e)
			{
				return false;
			}
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
		//lay ra tham so can tim
		private decimal GetThamSo(string tenthamso)
		{
			List<THAMSO> List_ThamSo = DataProvider.Ins.DB.THAMSOes.ToList();
			foreach(THAMSO ts in List_ThamSo)
			{
				if (ts.TenThamSo == tenthamso)
					return ts.GiaTri;
			}
			return -1;
		}
        #endregion
    }
}
