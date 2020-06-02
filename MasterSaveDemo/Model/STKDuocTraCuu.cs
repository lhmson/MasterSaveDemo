using Microsoft.Expression.Interactivity.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterSaveDemo.Model
{
    public class STKDuocTraCuu
    {
            public string STT { get; set; }
            public string Ma { get; set; }
            public string LoaiTietKiem { get; set; }
            public string TenKH { get; set; }
            public string SoDu { get; set; }
            public string NgayDaoHan { get; set; }
            public string LaiSuat { get; set; }

            public STKDuocTraCuu(string MaSo, string TenLTK, string KH, string SoDu, string NgayDH, string LS)
            {
                STT = "";
                Ma = MaSo;
                LoaiTietKiem = TenLTK;
                TenKH = KH;
                this.SoDu = SoDu;
                NgayDaoHan = NgayDH;
                LaiSuat = LS;
            }
    }
}
