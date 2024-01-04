using System.ComponentModel.DataAnnotations;

namespace QuanLy.Models
{
    public class SanPham
    {
        [Key] public int ID { get; set; }
        public string TenSP { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NSX { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime HSD { get; set; }
        public int DonGia { get; set; }
        [Required] public int LoaiHangID { get; set; }
        public LoaiHang LoaiHang { get; set; }
    }
}
 