using System.ComponentModel.DataAnnotations;
namespace QuanLy.Models
{
    public class HoaDonBan
    {
        [Key]
        public int ID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayBan { get; set; }
        
        [Required] public int NhanVienID { get; set; }
        
        public NhanVien NhanVien { get; set; }

        public CTHDB CTHDB { get; set; }
    }
}
