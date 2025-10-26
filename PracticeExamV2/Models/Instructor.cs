using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PracticeExamV2.Models;

public class Instructor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int ID { get; set; }

    [Required]
    [MaxLength(50)]
    [RegularExpression(@"^[A-Z][a-z]*$", ErrorMessage = "Start with a capital letter; remaining letters must be lowercase a–z.")]
    public string LastName { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    [RegularExpression(@"^[A-Z][a-z]*$", ErrorMessage = "Start with a capital letter; remaining letters must be lowercase a–z.")]
    public string FirstName { get; set; } = null!;

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? EnrollmentDate { get; set; }

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}".Trim();

    public virtual OfficeAssignment? OfficeAssignment { get; set; }

    public virtual List<CourseAssignment>? CourseAssignment { get; set; }

    public virtual List<Department>? Departments { get; set; }
}
