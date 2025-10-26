using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace PracticeExamV2.Models;


public class Student
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
    public string FirstMidName { get; set; } = null!;

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? EnrollmentDate { get; set; }

    [NotMapped]
    public string FullName => $"{FirstMidName} {LastName}".Trim();

    public virtual List<Enrollment>? Enrollment { get; set; }
}
