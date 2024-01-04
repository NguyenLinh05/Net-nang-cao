using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLy.Models
{
    public class NhanVien
    {
        [Key] public int ID { get; set; }

        //[Column("IDTK")]
        //[Required] public int TaiKhoanID { get; set; }
        [Required] public int TaiKhoanID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage ="Phải nhập vào tên nhân viên")] 
        public string TenNV { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Phải nhập ngày sinh")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgaySinh { get; set; }

        public bool GioiTinh { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public string? Anh { get; set; }

        public TaiKhoan? TaiKhoan { get; set; }

    }
}
