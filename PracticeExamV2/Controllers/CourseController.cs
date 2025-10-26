using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracticeExamV2.Data;
using PracticeExamV2.Models;
using PracticeExamV2.Utilities;

namespace PracticeExamV2.Controllers
{
    public class CourseController : Controller
    {
        private readonly Context _context;

        public CourseController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            const int pageSize = 4;
            var query = _context.Courses?.Include(d => d.Department)!
                                        .Include(e => e.Enrollment)!
                                        .ThenInclude(s => s.Student)
                                        .OrderBy(c => c.CourseID);
            var totalItems = await query?.CountAsync()!;
            var totalPages = (int)Math.Ceiling(totalItems/(double)pageSize);
            if (totalPages ==0)
            {
                totalPages = 1;
            }
            if (page <1)
            {
                page =1;
            }
            if (page > totalPages)
            {
                page = totalPages;
            }

            var courses = await query.Skip((page -1)*pageSize).Take(pageSize).ToListAsync();

            ViewBag.Courses = courses;
            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var course = await _context.Courses.Include(c => c.Department).FirstOrDefaultAsync(c => c.CourseID == id);

            if (course == null)
            {
                return NotFound();
            }

            ViewBag.Departments = new SelectList
            (
                await _context.Departments.OrderBy(d => d.Name).ToListAsync(),
                "DepartmentID",
                "Name",
                course.DepartmentID
            );

            return View(course);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Course model)
        {
            if (id!=model.CourseID)
            {
                return BadRequest();
            }

            if (model.Title!.Length < 4 || model.Title.Length > 50)
            {
                ModelState.AddModelError(nameof(model.Title), "Title must be between 4 and 50 characters");
            }

            if (model.Credits < 0 || model.Credits > 5)
            {
                ModelState.AddModelError(nameof(model.Credits), "Credits must be between 0 and 5");
            }

            var confirmDepartment = await _context.Departments.AnyAsync(d => d.DepartmentID == model.DepartmentID);
            if (!confirmDepartment)
            {
                ModelState.AddModelError(nameof(model.DepartmentID), "Please select a valid department");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Departments = new SelectList(
                    await _context.Departments.OrderBy(d => d.Name).ToListAsync(),
                    "DepartmentID", "Name", model.DepartmentID);
                return View(model);
            }

            try
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Courses.AnyAsync(c => c.CourseID == id))
                    return NotFound();
                throw;
            }

        }
    }
}
