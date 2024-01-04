using System.ComponentModel.DataAnnotations;

namespace QuanLy.Models
{
    public class CTHDN
    {
        [Key]
        public int ID { get; set; }
        [Required] public int HoaDonNhapID { get; set; }
        public int SoLuong { get; set; }
        public int DonGia { get; set; }
        [Required] public int NhaCCID { get; set; }
        [Required] public int LoaiHangID { get; set; }

        public NhaCC NhaCC { get; set; }

        public LoaiHang LoaiHang { get; set; }
    }
}
