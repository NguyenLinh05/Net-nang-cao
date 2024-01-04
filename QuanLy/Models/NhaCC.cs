using System.ComponentModel.DataAnnotations;

namespace QuanLy.Models
{
    public class NhaCC
    {
        [Key]
        public int ID { get; set; }
        public string TenNCC { get; set; }
        public string? DiaChi { get; set; }  
        public string? SDT { get; set;
        }
    }
}

