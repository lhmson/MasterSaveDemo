//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MasterSaveDemo.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class SOTIETKIEM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SOTIETKIEM()
        {
            this.PHIEUGUIs = new HashSet<PHIEUGUI>();
            this.PHIEURUTs = new HashSet<PHIEURUT>();
        }
    
        public string MaSoTietKiem { get; set; }
        public string MaLoaiTietKiem { get; set; }
        public string TenKhachHang { get; set; }
        public string DiaChi { get; set; }
        public decimal SoTienGuiBanDau { get; set; }
        public string SoCMND { get; set; }
        public System.DateTime NgayMoSo { get; set; }
        public double LaiSuatApDung { get; set; }
        public System.DateTime NgayDaoHanKeTiep { get; set; }
        public decimal SoDu { get; set; }
        public Nullable<System.DateTime> NgayDongSo { get; set; }
    
        public virtual LOAITIETKIEM LOAITIETKIEM { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUGUI> PHIEUGUIs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEURUT> PHIEURUTs { get; set; }
    }
}
