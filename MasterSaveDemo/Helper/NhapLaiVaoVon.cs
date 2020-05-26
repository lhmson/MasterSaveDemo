using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterSaveDemo.Model;
using System.Windows;

namespace MasterSaveDemo.Helper
{
    class NhapLaiVaoVon
    {
        private static NhapLaiVaoVon _ins;
        public static NhapLaiVaoVon Ins
        {
            get
            {
                if (_ins == null) _ins = new NhapLaiVaoVon();
                return _ins;
            }
            set
            {
                _ins = value;
            }
        }

        private NhapLaiVaoVon()
        {

        }
        private bool TinhLai(SOTIETKIEM stk, LOAITIETKIEM ltk, double laisuat, int songay)
        {
            try
            {
                decimal laisuat_theongay = (decimal)laisuat/360;
                stk.SoDu = stk.SoDu * (1 + (laisuat_theongay/100)*songay);
                stk.NgayDaoHanKeTiep = stk.NgayDaoHanKeTiep.AddDays(ltk.KyHan);
                DataProvider.Ins.DB.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool StartThis(string MSTK, bool confirm)
        {
            try
            {
                SOTIETKIEM stk = DataProvider.Ins.DB.SOTIETKIEMs.Where(x => x.MaSoTietKiem == MSTK).Single();
                LOAITIETKIEM ltk = DataProvider.Ins.DB.LOAITIETKIEMs.Where(x => x.MaLoaiTietKiem == stk.MaLoaiTietKiem).Single();
                //Truong hop chua toi ngay dao han, nhung van rut ngay (da du so ngay toi thieu)
                if (stk.NgayDaoHanKeTiep > DateTime.Today && (stk.NgayDaoHanKeTiep - DateTime.Today).TotalDays < ltk.KyHan)
                {
                    if (confirm == true) //confirm true neu dang rut tien, nguoi nhan se duoc tinh lai ngay
                    {
                        if (!TinhLai(stk, ltk, DataProvider.Ins.DB.LOAITIETKIEMs.Where(x => x.MaLoaiTietKiem == "001").Single().LaiSuat, ltk.KyHan - (int)(stk.NgayDaoHanKeTiep - DateTime.Today).TotalDays))
                            return false;
                    }
                    else //confirm false trong truong hop dang nhap lai hang loat (chi cong lai cho tai khoan dao han), khong nhap gì
                    {

                    }
                }
                else
                {
                    if (stk.NgayDaoHanKeTiep == DateTime.Today || (stk.NgayDaoHanKeTiep < DateTime.Today && confirm == false))
                    {
                        if (!TinhLai(stk, ltk, stk.LaiSuatApDung, ltk.KyHan))
                            return false;
                    }
                    else
                    {
                        if (stk.NgayDaoHanKeTiep < DateTime.Today && confirm == true)
                        {
                            if (!TinhLai(stk, ltk, stk.LaiSuatApDung, ltk.KyHan))
                                return false;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
