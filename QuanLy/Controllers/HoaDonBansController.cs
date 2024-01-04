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
    public class HoaDonBansController : BaseController
    {
        private readonly QLCuaHangContext _context;

        public HoaDonBansController(QLCuaHangContext context)
        {
            _context = context;
        }

        // GET: HoaDonBans
        public async Task<IActionResult> Index()
        {
            var qLCuaHangContext = _context.HoaDonBans.Include(h => h.NhanVien);
            return View(await qLCuaHangContext.ToListAsync());
        }

        // GET: HoaDonBans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HoaDonBans == null)
            {
                return NotFound();
            }

            var hoaDonBan = _context.HoaDonBans
                .Include(m => m.NhanVien)
                .Include(m => m.CTHDB)
                .FirstOrDefault(m => m.ID == id);
            var ctb = _context.cTHDBs
                .Include(m => m.SanPham)
                .FirstOrDefault(m => m.HoaDonBanID == hoaDonBan.ID);
            if (hoaDonBan == null)
            {
                return NotFound();
            }
            ViewData["ChiTiet"] = ctb;
            return View(hoaDonBan);

            

        }

        // GET: HoaDonBans/Create
        public IActionResult Create()
        {
            var nv = _context.NhanViens.ToList();
            var ctb = _context.cTHDBs.ToList();
            var sp = _context.SanPhams.ToList();
            ViewData["NhanVien"] = nv;
            ViewData["CTHDB"] = ctb;
            ViewData["SanPham"] = sp;
            return View();
        }

        // POST: HoaDonBans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DateTime NgayBan, int NhanVien, int SanPham, int SoLuong)
        {
            if (ModelState.IsValid)
            {
                string nb = NgayBan.ToString("yyyy/M/d");
                string sql = "insert into HoaDonBan(NgayBan, NhanVienID) values('" + nb + "', " + NhanVien + ")";
                _context.Database.ExecuteSqlRaw(sql);
                var hdb = _context.HoaDonBans.FromSqlRaw("select * from HoaDonBan where ID = (select max(ID) from HoaDonBan)").ToList();
                foreach(HoaDonBan hd in hdb)
                {
                    sql = "insert into CTHDB(HoaDonBanID, SoLuong, SanPhamID) values (" +hd.ID+ ","+SoLuong+", " + SanPham + ")";
                    _context.Database.ExecuteSqlRaw(sql);
                }
                return RedirectToAction(nameof(Index));
            }
            
            return View();
        }

        // GET: HoaDonBans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HoaDonBans == null)
            {
                return NotFound();
            }

            var hoaDonBan = _context.HoaDonBans
                .Include(m => m.NhanVien)
                .Include(m => m.CTHDB)
                .FirstOrDefault(m => m.ID == id);
            var ctb = _context.cTHDBs
                .Include(m => m.SanPham)
                .FirstOrDefault(m => m.HoaDonBanID == hoaDonBan.ID);
            if (hoaDonBan == null)
            {
                return NotFound();
            }
            ViewData["ChiTiet"] = ctb;
            var nv = _context.NhanViens.ToList();
            var sp = _context.SanPhams.ToList();
            ViewData["NhanVien"] = nv;
            ViewData["SanPham"] = sp;
            return View(hoaDonBan);
        }

        // POST: HoaDonBans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ID, DateTime NgayBan, int NhanVien, int SanPham, int SoLuong)
        {
            if (ModelState.IsValid)
            {
                string nb = NgayBan.ToString("yyyy/M/d");
                string sql = "update HoaDonBan set NgayBan = '" + nb + "', NhanVienID = " + NhanVien + " where ID = " + ID;
                _context.Database.ExecuteSqlRaw(sql);
                sql = "update CTHDB set SoLuong = " + SoLuong + ", SanPhamID = " + SanPham+ " where HoaDonBanID = " + ID;
                _context.Database.ExecuteSqlRaw(sql);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: HoaDonBans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            string sql = "delete from CTHDB where HoaDonBanID = " + id;
            _context.Database.ExecuteSqlRaw(sql);
            sql = "delete from HoaDonBan where ID = " + id;
            _context.Database.ExecuteSqlRaw(sql);
            return RedirectToAction(nameof(Index));
        }

        // POST: HoaDonBans/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.HoaDonBans == null)
        //    {
        //        return Problem("Entity set 'QLCuaHangContext.HoaDonBans'  is null.");
        //    }
        //    var hoaDonBan = await _context.HoaDonBans.FindAsync(id);
        //    if (hoaDonBan != null)
        //    {
        //        _context.HoaDonBans.Remove(hoaDonBan);
        //    }
            
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool HoaDonBanExists(int id)
        {
          return (_context.HoaDonBans?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
