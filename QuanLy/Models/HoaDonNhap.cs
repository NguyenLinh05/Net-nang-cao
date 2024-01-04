using System.ComponentModel.DataAnnotations;

namespace QuanLy.Models
{
    public class HoaDonNhap
    {
        [Key]
        public int ID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayNhap { get; set; }
        //public double TongTien {
        //    get
        //    {
        //        return 
        //    }
        //}
        
        [Required]  public int NhanVienID { get; set; }
        
        public NhanVien NhanVien { get; set; }

        public CTHDN CTHDN { get; set; }
    }
}
