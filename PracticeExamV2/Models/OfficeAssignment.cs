using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PracticeExamV2.Models;


public class OfficeAssignment
{
    [Key]
    [ForeignKey(nameof(InstructorID))]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int InstructorID { get; set; }

    public virtual Instructor Instructor { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string Location { get; set; } = null!;
}
