using System.ComponentModel.DataAnnotations;
namespace QuanLy.Models
{
    public class CTHDB
    {
        [Key]
        public int ID { get; set; }
        [Required] public int HoaDonBanID { get; set; }
        public int SoLuong { get; set; }
        [Required] public int SanPhamID { get; set; }
        public HoaDonBan HoaDonBan { get; set; }
        public SanPham SanPham { get; set; }

    }
}
