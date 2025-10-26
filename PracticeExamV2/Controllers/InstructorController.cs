using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeExamV2.Data;
using PracticeExamV2.Models;
using System.Text.RegularExpressions;

namespace PracticeExam.Controllers;


public class InstructorController : Controller
{
    public readonly Context _context;

    public InstructorController(Context context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        const int pageSize = 4;

        var query = _context.Instructors.Include(i => i.CourseAssignment!)
                                                    .ThenInclude(ca => ca.Course)
                                                    .ThenInclude(c => c!.Department)
                                                    .OrderBy(i => i.ID);

        var totalItems = await query.CountAsync();
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

        var instructors = await query.Skip((page -1)*pageSize).Take(pageSize).ToListAsync();

        ViewBag.Instructors = instructors;
        ViewBag.Page = page;
        ViewBag.TotalPages = totalPages;

        return View();
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id==null)
        {
            return RedirectToAction(nameof(Index));
        }
        var instructor = await _context.Instructors.FindAsync(id.Value);
        if (instructor==null)
        {
            return NotFound();
        }
        return View(instructor);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Instructor model)
    {
        if (id!=model.ID)
        {
            return BadRequest();
        }

        model.FirstName ??= string.Empty;
        model.LastName ??= string.Empty;


        if (model.FirstName.Length > 50)
        {
            ModelState.AddModelError(nameof(model.FirstName), "First Name cannot exceed 50 characters");
        }

        if (model.LastName.Length > 50)
        {
            ModelState.AddModelError(nameof(model.LastName), "Last Name cannot exceed 50 characters");
        }

        var namePattern = new Regex(@"^[A-Z][A-Za-z]*$");
        if (!namePattern.IsMatch(model.FirstName))
        {
            ModelState.AddModelError(nameof(model.FirstName), "First name must start with upper case and remaining characters are letters");
        }
        if (!namePattern.IsMatch(model.LastName))
        {
            ModelState.AddModelError(nameof(model.LastName), "Last name must start with upper case and remaining characters are letters");
        }

        if (!ModelState.IsValid)
        {
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
            if (!await _context.Instructors.AnyAsync(i => i.ID == id))
            {
                return NotFound();
            }
            throw;
        }
    }

}
