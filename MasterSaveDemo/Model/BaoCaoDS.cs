using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterSaveDemo.Model
{
    class BaoCaoDS
    {
        public int STT { get; set; }
        public string LoaiTietKiem { get; set; }
        public decimal TongThu { get; set; }
        public decimal TongChi { get; set; }
        public decimal ChenhLech { get; set; }

        public BaoCaoDS(int stt, string tenLoaiTK, decimal thu, decimal chi, decimal chenhLech)
        {
            this.STT = stt;
            this.LoaiTietKiem = tenLoaiTK;
            this.TongThu = thu;
            this.TongChi = chi;
            this.ChenhLech = chenhLech;
        }
    }
}
