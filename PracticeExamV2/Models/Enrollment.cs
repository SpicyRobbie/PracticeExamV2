using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PracticeExamV2.Utilities;


namespace PracticeExamV2.Models;


public class Enrollment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int EnrollmentID { get; set; }


    [Required]
    [ForeignKey(nameof(CourseID))]
    public int CourseID { get; set; }
    public virtual Course Course { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(StudentID))]
    public int StudentID { get; set; }
    public virtual Student Student { get; set; } = null!;

    [Range(0, 6, ErrorMessage = "Must be a number between 0 and 6")]
    public int? Grade { get; set; }
    public GradeCode GradeCode { get; set; }


}
