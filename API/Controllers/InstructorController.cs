using API.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeExamV2.Models;
using PracticeExamV2.Utilities;


namespace API.Controllers;

[ApiController]
[Route("api/instructor")]


public class InstructorController : ControllerBase
{
    private readonly IInstructorRepository _repo;

    public InstructorController(IInstructorRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("{id:int}")]
    public ActionResult<object> Get(int id)
    {
        var targetItem = _repo.Get(id);
        if (targetItem==null)
        {
            return NotFound();
        }
        return Ok(targetItem);

    }

    [HttpGet]
    public ActionResult<IEnumerable<Instructor>> GetAll() => Ok(_repo.GetAll());


    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] Instructor instructor)
    {
        if (instructor == null)
            return BadRequest(new { message = "Request body is required." });

        if (id != instructor.ID)
            return BadRequest(new { message = "Route id and body id must match." });

        try
        {
            var rows = _repo.Update(id, instructor);
            if (rows == 0)
                return NotFound(new { message = "Instructor not found." });

            return Ok(new
            {
                instructor.LastName,
                instructor.FirstName,
                instructor.EnrollmentDate
            });
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(new { message = "Validation failed at database layer.", detail = ex.InnerException?.Message ?? ex.Message });
        }
    

}
}
