
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLy.Models
{
    public class TaiKhoan
    {
        [Key] public int ID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phải nhập vào tên người dùng")]
        //[NotMapped]
        //public int? NhanVienID { get; set; }
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phải nhập vào mật khẩu")]
        public string Password { get; set; }

        public string? Quyen { get; set; }
    }
}
