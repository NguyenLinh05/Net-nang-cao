using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using QuanLy.Models;

namespace QuanLy.Controllers
{
    public class NhanViensController : BaseController
    {
        private readonly QLCuaHangContext _context;

        public NhanViensController(QLCuaHangContext context)
        {
            _context = context;
        }

        // GET: NhanViens
        public async Task<IActionResult> Index()
        {
              return _context.NhanViens != null ? 
                          View(await _context.NhanViens.ToListAsync()) :
                          Problem("Entity set 'QLCuaHangContext.NhanViens'  is null.");
        }

        // GET: NhanViens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NhanViens == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens
                .FirstOrDefaultAsync(m => m.ID == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // GET: NhanViens/Create
        public IActionResult Create()
        {
            //ViewData["TaiKhoanID"] = new SelectList(_context.TaiKhoans, "ID", "Password");
            return View();
        }

        // POST: NhanViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TaiKhoanID,TenNV,NgaySinh,GioiTinh,SDT,DiaChi")] NhanVien nhanVien, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhanVien);
                await _context.SaveChangesAsync();
                string filename = Upload(nhanVien.ID.ToString(), file);
                nhanVien.Anh = string.Format("{0}/{1}", nhanVien.ID, filename);
                _context.Update(nhanVien);
                await _context.SaveChangesAsync(); 
                return RedirectToAction(nameof(Index));
            }
            ViewData["TaiKhoanID"] = new SelectList(_context.TaiKhoans, "ID", "Pass", nhanVien.TaiKhoanID);
            return View(nhanVien);
        }

        

        // GET: NhanViens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NhanViens == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien == null)
            {
                return NotFound();
            }
            var taiKhoan = await _context.NhanViens
            .Include(t => t.TaiKhoan)
                .FirstOrDefaultAsync(m => m.TaiKhoanID == id);
            return View(nhanVien);
        }

        // POST: NhanViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TaiKhoanID,TenNV,NgaySinh,GioiTinh,SDT,DiaChi,Anh")] NhanVien nhanVien, IFormFile? file)
        {
            if (id != nhanVien.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(file != null)
                {
                    DeleteAvatar(nhanVien.ID.ToString());
                    string filename = Upload(nhanVien.ID.ToString(), file);
                    nhanVien.Anh = string.Format("{0}/{1}", nhanVien.ID, filename);
                    _context.Update(nhanVien);
                    await _context.SaveChangesAsync();
                }
                
                    else
                    {
                    _context.Update(nhanVien);
                    await _context.SaveChangesAsync();

                }
                return RedirectToAction(nameof(Index));
            }
            var taiKhoan = await _context.NhanViens
            .Include(t => t.TaiKhoan)
                .FirstOrDefaultAsync(m => m.TaiKhoanID == id);
            return View(nhanVien);
        }

        // GET: NhanViens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            string confirmationMessage = "confirmDelete()";

            //// Xóa nhân viên
            string sql = "delete from NhanVien where ID = " + id;
            _context.Database.ExecuteSqlRaw(sql);

            //// Chuyển hướng về trang danh sách nhân viên
            return RedirectToAction(nameof(Index));



            //if (id == null || _context.NhanViens == null)
            //{
            //    return NotFound();
            //}

            //var nhanVien = await _context.NhanViens
            //    .FirstOrDefaultAsync(m => m.ID == id);
            //if (nhanVien == null)
            //{
            //    return NotFound();
            //}

            //return View(nhanVien);
        }



        // POST: NhanViens/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{

        //    string confirmationMessage = "confirmDelete()";

        //    // Xóa nhân viên
        //    string sql = "delete from NhanVien where ID = " + id;
        //    _context.Database.ExecuteSqlRaw(sql);

        //    // Chuyển hướng về trang danh sách nhân viên
        //    return RedirectToAction(nameof(Index));




        //    //if (_context.NhanViens == null)
        //    //{
        //    //    return Problem("Entity set 'QLCuaHangContext.NhanViens'  is null.");
        //    //}
        //    //var nhanVien = await _context.NhanViens.FindAsync(id);
        //    //if (nhanVien != null)
        //    //{
        //    //    _context.NhanViens.Remove(nhanVien);
        //    //}
            
        //    //await _context.SaveChangesAsync();
        //    //return RedirectToAction(nameof(Index));
        //}

        private bool NhanVienExists(int id)
        {
          return (_context.NhanViens?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        [NonAction]
        public string Upload(string id, IFormFile file)
        {
            if (file != null)
            {
                var folderPath = Path.GetFullPath("./Data");
                folderPath = string.Format(@"{0}\{1}", folderPath, id);
                Directory.CreateDirectory(folderPath);
                var filePath = string.Format(@"{0}\{1}", folderPath, file.FileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(stream);
                return string.Format("/{0}", file.FileName);
            }
            return String.Empty;
        }
        private bool DeleteAvatar(string id)
        {
            try
            {
                string path = Path.GetFullPath("./Data/" + id);
                Directory.Delete(path, true);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
