using System.ComponentModel.DataAnnotations;

namespace QuanLy.Models
{
    public class LoaiHang
    {
        [Key]
        public int ID { get; set; }
        public string TenLH { get; set; }
    }
}
