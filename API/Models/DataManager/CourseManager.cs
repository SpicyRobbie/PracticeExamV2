using API.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using PracticeExamV2.Data;
using PracticeExamV2.Models;
using PracticeExamV2.Utilities;

namespace API.Models.DataManager;



public class CourseManager : ICoursesRepository
{
    public readonly Context _context;

    public CourseManager(Context context)
    {
        _context = context;
    }

    public Course? Get(int id) => _context.Courses?.Include(d=>d.Department)!
                                                   .Include(e => e.Enrollment)!
                                                   .ThenInclude(s => s.Student)
                                                   .FirstOrDefault(b => b.CourseID == id);


    public IEnumerable<Course> GetAll() => _context.Courses.Include(d => d.Department)!
                                                   .Include(e => e.Enrollment)!
                                                   .ThenInclude(s => s.Student)
                                                   .ToList();

    public int Update(int id, Course courses)
    {
        if (id!=courses.CourseID)
        {
            return 0;
        }

        var existing = _context.Courses.FirstOrDefault(c => c.CourseID == id);
        if (existing==null)
        {
            return 0;
        }
        existing!.Title = courses.Title;
        existing!.Credits = courses.Credits;
        existing!.DepartmentID = courses.DepartmentID;

        return _context.SaveChanges();
    }


}
