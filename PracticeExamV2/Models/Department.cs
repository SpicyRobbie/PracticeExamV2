using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeExamV2.Models;
public class Department
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int DepartmentID { get; set; }


    [MaxLength(50)]
    [MinLength(3)]
    public string? Name { get; set; }

    [Column(TypeName="money")]
    public decimal? Budget {  get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? StartDate { get; set; }

    [ForeignKey(nameof(InstructorID))]
    public int? InstructorID { get; set; }
    public virtual Instructor Instructor { get; set; } = null!;

    //map 0 to none 
    

    //map 1 to many
    public virtual List<Course>? Courses { get; set; }
}
