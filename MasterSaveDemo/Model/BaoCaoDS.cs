using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterSaveDemo.Model
{
    public class BaoCaoDS
    {
        public int SoThuTu { get; set; }
        public string TenLoaiTietKiem { get; set; }
        public string TongThu { get; set; }
        public string TongChi { get; set; }
        public string ChenhLech { get; set; }

        public BaoCaoDS(int stt, string tenLoaiTK, string thu, string chi, string chenhLech)
        {
            this.SoThuTu = stt;
            this.TenLoaiTietKiem = tenLoaiTK;
            this.TongThu = thu;
            this.TongChi = chi;
            this.ChenhLech = chenhLech;
        }
    }
}
