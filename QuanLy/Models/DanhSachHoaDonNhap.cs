namespace QuanLy.Models
{
    public class DanhSachHoaDonNhap
    {
        public List<HoaDonNhap> hoaDonNhap { get; set; }
        public List<CTHDN> cTHDN { get; set; }
        public List<LoaiHang> loaiHang { get; set;}
        public List<SanPham> sanPham { get; set; }
        public List<NhaCC> nhaCC { get; set; }

    }
}
