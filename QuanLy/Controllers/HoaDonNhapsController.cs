using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLy.Models;

namespace QuanLy.Controllers
{
    public class HoaDonNhapsController : BaseController
    {
        private readonly QLCuaHangContext _context;

        public HoaDonNhapsController(QLCuaHangContext context)
        {
            _context = context;
        }

        // GET: HoaDonNhaps
        public async Task<IActionResult> Index()
        {
            var qLCuaHangContext = _context.HoaDonNhaps.Include(h => h.NhanVien);
            return View(await qLCuaHangContext.ToListAsync());
        }

        // GET: HoaDonNhaps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HoaDonNhaps == null)
            {
                return NotFound();
            }

            var hoaDonNhap =  _context.HoaDonNhaps
                .Include(m => m.NhanVien)
                .Include(m => m.CTHDN)
                .FirstOrDefault(m => m.ID == id);
            var ct = _context.cTHDNs
                .Include(m => m.LoaiHang)
                .Include(m => m.NhaCC)
                .FirstOrDefault(m => m.HoaDonNhapID == hoaDonNhap.ID);
            if (hoaDonNhap == null)
            {
                return NotFound();
            }
            ViewData["ChiTiet"] = ct;
            return View(hoaDonNhap);
        }

        // GET: HoaDonNhaps/Create
        public IActionResult Create()
        {
            var nv = _context.NhanViens.ToList();
            var ncc = _context.NhaCCs.ToList();
            var lh = _context.LoaiHangs.ToList();
            ViewData["NhanVien"] = nv;
            ViewData["NhaCungCap"] = ncc;
            ViewData["LoaiHang"] = lh;
            return View();
        }

        // POST: HoaDonNhaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DateTime NgayNhap, int NhanVien, int SoLuong, int DonGia, int NhaCungCap, int LoaiHang)
        {
            if (ModelState.IsValid)
            {
                string nn = NgayNhap.ToString("yyyy/M/d");
                string sql = "insert into HoaDonNhap(NgayNhap, NhanVienID) values('" + nn + "'," + NhanVien + ")";
                _context.Database.ExecuteSqlRaw(sql);
                var hdn = _context.HoaDonNhaps.FromSqlRaw("select * from HoaDonNhap where ID = (select max(ID) from HoaDonNhap)").ToList();
                foreach(HoaDonNhap hd in hdn)
                {
                    sql = "insert into CTHDN(HoaDonNhapID,SoLuong,DonGia,NhaCCID,LoaiHangID) values (" + hd.ID + "," + SoLuong + "," + DonGia + "," + NhaCungCap + "," + LoaiHang + ")";
                    _context.Database.ExecuteSqlRaw(sql);
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: HoaDonNhaps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HoaDonNhaps == null)
            {
                return NotFound();
            }

            var hoaDonNhap = _context.HoaDonNhaps
                .Include(m => m.NhanVien)
                .Include(m => m.CTHDN)
                .FirstOrDefault(m => m.ID == id);
            var ct = _context.cTHDNs
                .Include(m => m.LoaiHang)
                .Include(m => m.NhaCC)
                .FirstOrDefault(m => m.HoaDonNhapID == hoaDonNhap.ID);
            if (hoaDonNhap == null)
            {
                return NotFound();
            }
            ViewData["ChiTiet"] = ct;
            var nv = _context.NhanViens.ToList();
            var ncc = _context.NhaCCs.ToList();
            var lh = _context.LoaiHangs.ToList();
            ViewData["NhanVien"] = nv;
            ViewData["NhaCungCap"] = ncc;
            ViewData["LoaiHang"] = lh;
            return View(hoaDonNhap);
        }

        // POST: HoaDonNhaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ID, DateTime NgayNhap, int NhanVien, int SoLuong, int DonGia, int NhaCungCap, int LoaiHang)
        {
            if (ModelState.IsValid)
            {
                string nn = NgayNhap.ToString("yyyy/M/d");
                string sql = "update HoaDonNhap set NgayNhap = '" + nn + "', NhanVienID = " + NhanVien + " where ID = " + ID;
                _context.Database.ExecuteSqlRaw(sql);
                sql = "update CTHDN set SoLuong = " + SoLuong + ", DonGia = " + DonGia + ",NhaCCID = " + NhaCungCap + ",LoaiHangID = " + LoaiHang + " where HoaDonNhapID = " + ID;
                _context.Database.ExecuteSqlRaw(sql);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: HoaDonNhaps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            string sql = "delete from CTHDN where HoaDonNhapID = " + id;
            _context.Database.ExecuteSqlRaw(sql);
            sql = "delete from HoaDonNhap where ID = " + id;
            _context.Database.ExecuteSqlRaw(sql);
            return RedirectToAction(nameof(Index));
        }

        private bool HoaDonNhapExists(int id)
        {
          return (_context.HoaDonNhaps?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
