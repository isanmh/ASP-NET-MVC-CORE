using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcApp.Data;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    public class EmployeeController : Controller
    {
        // membuat constructor untuk koneksi antara dbcontext dengan server
        private readonly EmployeeDbContext _db;

        public EmployeeController(EmployeeDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            // if tenarry (kondisi) ? nilai true : false
            return _db.Employees != null ?
                    View(await _db.Employees.ToListAsync())
                    : Problem("Data is not available");
        }

        // ini untuk route ke halaman create employee
        public IActionResult Create()
        {
            return View();
        }

        // fungsi untuk post ADD employee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Id,Name,Division,Role")] Employee employee)
        {
            // validation pada AspNetCore
            if (ModelState.IsValid)
            {
                _db.Add(employee);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }

        // ini untuk ke halaman edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            // logika pengambilan id data
            if (id == null || _db.Employees == null)
            {
                return NotFound();
            }
            // jika ada
            var employee = await _db.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // Lakukan update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(int id, [Bind("Id,Name,Division,Role")] Employee employee)
        {
            // logika jika data id ada
            if (id != employee.Id)
            {
                return NotFound();
            }
            // logika jika valid request sesuai data berdasarkan Id 
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(employee);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExits(employee.Id))
                    // if (employee.Id == null)
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
            return View(employee);
        }

        // logika fungsi jika id ditemukan di employee
        private bool EmployeeExits(int id)
        {
            return (_db.Employees?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // fungsi untuk mengambil data yang ingin di delete beredasarkan ID
        public async Task<IActionResult> Delete(int? id)
        {
            // logika jika ada id
            if (id == null || _db.Employees == null)
            {
                return NotFound();
            }
            // var untuk menampung data
            var employee = await _db.Employees.FirstOrDefaultAsync(m => m.Id == id);
            // cek apakah data ada
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // fungsi untuk delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            // id kosong
            if (_db.Employees == null)
            {
                return Problem("Data yang ingin di hapus tidak ada atau kosong.");
            }
            // logika jika ada id
            var employee = await _db.Employees.FindAsync(id);
            if (employee != null)
            {
                _db.Employees.Remove(employee);
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}