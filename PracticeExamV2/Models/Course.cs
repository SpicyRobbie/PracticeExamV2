using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PracticeExamV2.Models;
public class Course
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int CourseID { get; set; }

    [Required]
    [MaxLength(50)]
    [MinLength(3)]
    public string? Title { get; set; }

    [Required]
    [Range(0, 5, ErrorMessage = "Credits must be between 0 and 5")]

    public int Credits { get; set; }

    [Required]
    [ForeignKey(nameof(Department))]
    public int DepartmentID { get; set; }
    public virtual Department? Department { get; set; }


    public virtual List<CourseAssignment>? CourseAssignment { get; set; }
    public virtual List<Enrollment>? Enrollment { get; set; }

}
