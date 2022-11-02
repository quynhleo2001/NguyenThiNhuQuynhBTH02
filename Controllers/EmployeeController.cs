using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NguyenThiNhuQuynhBTH02.Models;
using NguyenThiNhuQuynhBTH02.Models.Process;
using NguyenThiNhuQuynhBTH02.Models; 



namespace NguyenThiNhuQuynhBTH02.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MvcMovieContext _context;

        public EmployeeController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
              return _context.Employee != null ? 
                          View(await _context.Employee.ToListAsync()) :
                          Problem("Entity set 'MvcMovieContext.Employee'  is null.");
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeID,EmployeeName,Age")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeID,EmployeeName,Age")] Employee employee)
        {
            if (id != employee.EmployeeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeID))
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

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employee == null)
            {
                return Problem("Entity set 'MvcMovieContext.Employee'  is null.");
            }
            var employee = await _context.Employee.FindAsync(id);
            if (employee != null)
            {
                _context.Employee.Remove(employee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
          return (_context.Employee?.Any(e => e.EmployeeID == id)).GetValueOrDefault();
        }
        private readonly MvcMovieContext _context;
        private ExcelProcess _ExcelProcess = new ExcelProcess();
        public EmployeeController(MvcMovieContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employee.ToListAsync());
        }
        Private bool EmployeeExists(string id)
        {
            return _context.Employee.Any(e => e.EmpID == id);
        }
        public async Task<IActionResult>Upload()
{
    return View();
}
[Httppost]
[ValidateAntiForgeryToken]
public async Task<IActionResult>Upload(IFormFile file)
{
    if (file!=null)
    {
        string fileExtension = Path.GetExtension(file.FileName);
        if (fileExtension != ".xls" && fileExtension != ".xlsx")
        {
            ModelState.AddModelError("", "Please choose excel file to upload!");
        }
        else
        {
            var fileName = DateTime.Now.ToShortTimeString() + fileExtension;
            var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Uploads/Excels", fileName);
            var fileLocation = new FileInfo(filePath).ToString();
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                //save file to server
                await file.CopyToAsync(stream);
                var dt = _ExcelProcess.ExcelToDataTable(fileLocation);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var emp = new Employee();
                    emp.EmpID = dt.Rows[i][0].ToString();
                    emp.EmpName = dt.Rows[i][1].ToString();
                    emp.Address = dt.Rows[i][2].ToString();
                    _context.Employee.Add(emp);
                }
            }
        }
    }
    return View();
}
    }
}
