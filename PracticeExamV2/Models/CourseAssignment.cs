using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeExamV2.Models;


public class CourseAssignment
{
    [ForeignKey(nameof(CourseID))]
    public int CourseID { get; set; }

    public virtual Course? Course { get; set; }

    [ForeignKey(nameof(InstructorID))]

    public int InstructorID { get; set; }

    public virtual Instructor? Instructor { get; set; }

}
