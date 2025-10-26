using PracticeExamV2.Models;
using PracticeExamV2.Utilities;
using Microsoft.EntityFrameworkCore;

namespace PracticeExamV2.Data;

public class Context: DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) 
    { }

    public DbSet<Course> Courses { get; set; } 
    public DbSet<CourseAssignment> CoursesAssignment { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Course
        modelBuilder.Entity<Course>().ToTable(b =>
        {
            //THIS IS AN ERROR BE CAREFUL!!!
            b.HasCheckConstraint("CH_Course_CourseID", "LEN([CourseID]) BETWEEN 3 AND 50");
            b.HasCheckConstraint("CH_Course_Credits", "[Credits] >= 0 AND [Credits] <= 5");

        });

        //CourseAssignment
        modelBuilder.Entity<CourseAssignment>(b =>
        {
            b.HasKey(x => new { x.CourseID, x.InstructorID });
        });

        //Department
        modelBuilder.Entity<Department>().ToTable(b =>
        {
            b.HasCheckConstraint("CH_Department_Name", "LEN([Name]) BETWEEN 3 AND 50");
        });

        //Enrollment
        modelBuilder.Entity<Enrollment>().ToTable(b =>
        {
            b.HasCheckConstraint("CK_Enrollment_Grade", $"[Grade] IS NULL OR [Grade] IN ({(int)GradeCode.A}, " +
                                                        $"{(int)GradeCode.B}, " +
                                                        $"{(int)GradeCode.C}, " +
                                                        $"{(int)GradeCode.D}, " +
                                                        $"{(int)GradeCode.E}, " +
                                                        $"{(int)GradeCode.F})");

        });

        //Instructor

        modelBuilder.Entity<Instructor>().ToTable(b =>
        {
            b.HasCheckConstraint("CH_Instructor_LastName", "LEN([LastName]) <= 50");
            b.HasCheckConstraint("CH_Instructor_FirstName", "LEN([FirstName]) <= 50");
        });

        //Office Assignment

        modelBuilder.Entity<OfficeAssignment>().ToTable(b =>
        {
            b.HasCheckConstraint("CH_OfficeAssignment_Location", "LEN([Location]) <= 50");

        });

        modelBuilder.Entity<Student>().ToTable(b =>
        {
            b.HasCheckConstraint("CH_Student_LastName", "LEN([LastName]) <= 50");
            b.HasCheckConstraint("CH_Student_FirstMidName", "LEN([FirstMidName]) <= 50");
        });

        //0 to One relationships
        modelBuilder.Entity<OfficeAssignment>().HasOne(i => i.Instructor).WithOne(o => o.OfficeAssignment)
                                               .HasForeignKey<OfficeAssignment>(o => o.InstructorID);

        //1 to Many relationships
        modelBuilder.Entity<Department>().HasOne(d => d.Instructor).WithMany(i => i.Departments).HasForeignKey(i => i.InstructorID);
        modelBuilder.Entity<CourseAssignment>().HasOne(d=>d.Instructor).WithMany(i=>i.CourseAssignment).HasForeignKey(i => i.InstructorID);
        modelBuilder.Entity<CourseAssignment>().HasOne(d => d.Course).WithMany(i => i.CourseAssignment).HasForeignKey(i => i.CourseID);
        modelBuilder.Entity<Course>().HasOne(d => d.Department).WithMany(i => i.Courses).HasForeignKey(d => d.DepartmentID);
        modelBuilder.Entity<Enrollment>().HasOne(d=>d.Course).WithMany(i=>i.Enrollment).HasForeignKey(d=>d.CourseID);
        modelBuilder.Entity<Enrollment>().HasOne(d=>d.Student).WithMany(i=>i.Enrollment).HasForeignKey(d=>d.StudentID);
    }
}
