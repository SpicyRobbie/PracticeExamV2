using PracticeExamV2.Models;
using PracticeExamV2.Utilities;
using Microsoft.EntityFrameworkCore;

namespace PracticeExamV2.Data;

public class SeedDataBase
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<Context>();

        // -------- Instructors --------
        if (!await context.Instructors.AnyAsync())
        {
            await context.Instructors.AddRangeAsync
            (
                new Instructor { ID = 1, LastName = "Hogan", FirstName = "Hulk", EnrollmentDate = new DateTime(1985, 7, 1) },
                new Instructor { ID = 2, LastName = "Savage", FirstName = "Randy", EnrollmentDate = new DateTime(1990, 7, 1) },
                new Instructor { ID = 3, LastName = "Hart", FirstName = "Brett", EnrollmentDate = new DateTime(1994, 7, 1) },
                new Instructor { ID = 4, LastName = "Rogan", FirstName = "Joe", EnrollmentDate = new DateTime(2023, 7, 1) },
                new Instructor { ID = 5, LastName = "Messi", FirstName = "Lionel", EnrollmentDate = new DateTime(2006, 7, 1) }
            );
            await context.SaveChangesAsync();
        }

        // -------- Students --------
        if (!await context.Students.AnyAsync())
        {
            await context.Students.AddRangeAsync
            (
                new Student { ID = 1, LastName = "Cristiano", FirstMidName = "Ronaldo", EnrollmentDate = new DateTime(1985, 7, 1) },
                new Student { ID = 2, LastName = "Shevchenko", FirstMidName = "Andrei", EnrollmentDate = new DateTime(1990, 7, 1) },
                new Student { ID = 3, LastName = "Hart", FirstMidName = "Brett", EnrollmentDate = new DateTime(1994, 7, 1) },
                new Student { ID = 4, LastName = "Pirlo", FirstMidName = "Andrea", EnrollmentDate = new DateTime(2023, 7, 1) },
                new Student { ID = 5, LastName = "Batistuta", FirstMidName = "Gabriel", EnrollmentDate = new DateTime(2006, 7, 1) }
            );
            await context.SaveChangesAsync();
        }

        // -------- OfficeAssignments --------
        if (!await context.OfficeAssignments.AnyAsync())
        {
            await context.OfficeAssignments.AddRangeAsync
            (
                new OfficeAssignment { InstructorID = 1, Location = "New York" },
                new OfficeAssignment { InstructorID = 2, Location = "Jamaica" },
                new OfficeAssignment { InstructorID = 3, Location = "Sydney" },
                new OfficeAssignment { InstructorID = 4, Location = "San Francisco" },
                new OfficeAssignment { InstructorID = 5, Location = "Belgium" }
            );
            await context.SaveChangesAsync();
        }

        // -------- Departments --------
        if (!await context.Departments.AnyAsync())
        {
            await context.Departments.AddRangeAsync
            (
                new Department { DepartmentID = 1, Name = "Fitness", Budget = 500000, StartDate = new DateTime(2025, 7, 1), InstructorID = 1 },
                new Department { DepartmentID = 2, Name = "Boxing", Budget = 100000, StartDate = new DateTime(2014, 7, 1), InstructorID = 2 },
                new Department { DepartmentID = 3, Name = "Dancing", Budget = 100000, StartDate = new DateTime(2025, 7, 1), InstructorID = 3 },
                new Department { DepartmentID = 4, Name = "Accounting", Budget = 5100000, StartDate = new DateTime(2015, 7, 1), InstructorID = 4 },
                new Department { DepartmentID = 5, Name = "Break Dancing", Budget = 600000, StartDate = new DateTime(2025, 7, 1), InstructorID = 5 }
            );
            await context.SaveChangesAsync();
        }

        // ===== CourseIDs mapped to 3-digit values =====
        const int C1 = 101;
        const int C2 = 202;
        const int C3 = 303;
        const int C4 = 404;
        const int C5 = 505;

        // -------- Courses (now 3-digit CourseIDs) --------
        if (!await context.Courses.AnyAsync())
        {
            await context.Courses.AddRangeAsync
            (
                new Course { CourseID = C1, Title = "Fighting", Credits = 1, DepartmentID = 1 },
                new Course { CourseID = C2, Title = "Spinning", Credits = 3, DepartmentID = 2 },
                new Course { CourseID = C3, Title = "Fighting", Credits = 3, DepartmentID = 3 },
                new Course { CourseID = C4, Title = "Fighting", Credits = 1, DepartmentID = 4 },
                new Course { CourseID = C5, Title = "Fighting", Credits = 1, DepartmentID = 5 }
            );
            await context.SaveChangesAsync();
        }

        // -------- Enrollments (FKs -> new 3-digit CourseIDs) --------
        if (!await context.Enrollments.AnyAsync())
        {
            await context.Enrollments.AddRangeAsync
            (
                new Enrollment { EnrollmentID = 1, CourseID = C1, StudentID = 1, Grade = 1 },
                new Enrollment { EnrollmentID = 2, CourseID = C1, StudentID = 2, Grade = 2 },
                new Enrollment { EnrollmentID = 3, CourseID = C3, StudentID = 3, Grade = 3 },
                new Enrollment { EnrollmentID = 4, CourseID = C4, StudentID = 4, Grade = 4 },
                new Enrollment { EnrollmentID = 5, CourseID = C5, StudentID = 5, Grade = 5 }
            );
            await context.SaveChangesAsync();
        }

        // -------- CourseAssignment (FKs -> new 3-digit CourseIDs) --------
        if (!await context.CoursesAssignment.AnyAsync())
        {
            await context.CoursesAssignment.AddRangeAsync
            (
                new CourseAssignment { CourseID = C1, InstructorID = 1 },
                new CourseAssignment { CourseID = C2, InstructorID = 2 },
                new CourseAssignment { CourseID = C3, InstructorID = 3 },
                new CourseAssignment { CourseID = C4, InstructorID = 4 },
                new CourseAssignment { CourseID = C5, InstructorID = 5 }
            );
            await context.SaveChangesAsync();
        }
    }
}
