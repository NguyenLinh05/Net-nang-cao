using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLy.Models;

namespace QuanLy.Controllers
{
    public class TaiKhoansController : BaseController
    {
        private readonly QLCuaHangContext _context;
        private readonly UserManager<TaiKhoan> _userManager;

        public TaiKhoansController(UserManager<TaiKhoan> userManager)
        {
            _userManager = userManager;
        }

        public TaiKhoansController(QLCuaHangContext context)
        {
            _context = context;
        }

        //Phân quyền 
        [CustomAuthorization("admin")]
        public IActionResult TaiKhoans()
        {
            return View();
        }

        [CustomAuthorization("quản lý")]
        public IActionResult ManagerPage()
        {
            return View();
        }

        [CustomAuthorization("quản lý kho")]
        public IActionResult HoaDonNhaps()
        {
            return View();
        }

        [CustomAuthorization("nhân viên bán hàng")]
        public IActionResult HoaDonBans()
        {
            return View();
        }


        public async Task<IActionResult> Index()
        {
            return _context.TaiKhoans != null ?
                        View(await _context.TaiKhoans.ToListAsync()) :
                        Problem("Entity set 'QLCuaHangContext.TaiKhoans'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {

            if (id == null || _context.TaiKhoans == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.NhanViens
                .Include(t => t.TaiKhoan)
                .FirstOrDefaultAsync(m => m.TaiKhoanID == id);

            if (taiKhoan == null)
            {
                return NotFound();
            }
            return View(taiKhoan);
        }

        public IActionResult DangNhap()
        {
            return View();
        }

        // GET: TaiKhoans

        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public async IActionResult DangNhap(TaiKhoan model)
        {
            SHA256 hashMethod = SHA256.Create();
            var pass = Util.Cryptography.GetHash(hashMethod, model.Password);
            var login = _context.TaiKhoans.Where(t => t.UserName == model.UserName && t.Password.Equals(pass));
            if (login != null)
            {
                                return RedirectToAction("Index", "NhanViens");

            }
            

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Quyen)
                };
            //    var claimsIdentity = new ClaimsIdentity(
            //claims, CookieAuthenticationDefaults.AuthenticationScheme);

            //    var authProperties = new AuthenticationProperties
            //    {
            //        IsPersistent = model.RememberMe
            //    };

            //    await HttpContext.SignInAsync(
            //        CookieAuthenticationDefaults.AuthenticationScheme,
            //        new ClaimsPrincipal(claimsIdentity),
            //        authProperties);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác.");
            return View(model);

            return View();
        }
        public IActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DangKy([Bind("UserName,Password")] TaiKhoan model)
        {
            if (ModelState.IsValid)
            {
                //Ma hoa mk
                SHA256 hashMethod = SHA256.Create();
                model.Password = Util.Cryptography.GetHash(hashMethod, model.Password);
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(DangNhap));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TaiKhoans == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoans
                .FirstOrDefaultAsync(m => m.ID == id);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            return View(taiKhoan);
        }

        // POST: NhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TaiKhoans == null)
            {
                return Problem("Entity set 'QLCuaHangContext.TaiKhoans'  is null.");
            }
            var taiKhoan = await _context.TaiKhoans.FindAsync(id);
            if (taiKhoan != null)
            {
                _context.TaiKhoans.Remove(taiKhoan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TaiKhoans == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoans.FindAsync(id);
            if (taiKhoan == null)
            {
                return NotFound();
            }
            return View(taiKhoan);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserName,Password,Quyen")] TaiKhoan taiKhoan)
        {
            if (id != taiKhoan.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taiKhoan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaiKhoanExists(taiKhoan.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(taiKhoan);
        }

        public IActionResult Thoat()
        {
            CurrentUser = "";
            return RedirectToAction("DangNhap");
        }
        private bool TaiKhoanExists(int id)
        {
            return (_context.TaiKhoans?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
