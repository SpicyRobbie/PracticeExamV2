using API.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using PracticeExamV2.Data;
using PracticeExamV2.Models;
using PracticeExamV2.Utilities;


namespace API.Models.DataManager;


public class InstructorManager: IInstructorRepository
{
    public readonly Context _context;

    public InstructorManager(Context context)
    {
        _context = context;
    }

    public Instructor? Get(int id) => _context.Instructors.Include(i => i.CourseAssignment!)
                                                    .ThenInclude(ca => ca.Course)
                                                    .ThenInclude(c => c!.Department)
                                                    .FirstOrDefault(b => b.ID == id);


    public IEnumerable<Instructor> GetAll() => _context.Instructors.Include(i => i.CourseAssignment!)
                                                    .ThenInclude(ca => ca.Course)
                                                    .ThenInclude(c => c!.Department)
                                                    .ToList();

    public int Update(int id, Instructor instructors)
    {
        if (id!=instructors.ID)
        {
            return 0;
        }

        var existing = _context.Instructors.FirstOrDefault(c => c.ID == id);
        if (existing==null)
        {
            return 0;
        }
        existing!.LastName = instructors.LastName;
        existing!.FirstName = instructors.FirstName;
        existing!.EnrollmentDate = instructors.EnrollmentDate;

        return _context.SaveChanges();
    }
}
