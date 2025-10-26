using API.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeExamV2.Models;
using PracticeExamV2.Utilities;


namespace API.Controllers;

[ApiController]
[Route("api/course")]


public class CourseController : ControllerBase
{
    private readonly ICoursesRepository _repo;

    public CourseController(ICoursesRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("{id:int}")]
    public ActionResult<object> Get(int id)
    {
        var targetItem = _repo.Get(id);
        if(targetItem==null)
        {
            return NotFound();
        }
        return Ok(targetItem);

    }

    [HttpGet]
    public ActionResult<IEnumerable<object>> GetAll()
    {
        var targetItems = _repo.GetAll();
        if (targetItems==null)
        {
            return NotFound();
        }
        return Ok(targetItems);

    }

    [HttpPut("{id:int}")]

    public IActionResult Update(int id, [FromBody] Course course)
    {
        if (course==null)
        {
            return NotFound();
        }
        
        var target = _repo.Get(id);
        if (target==null)
        {
            return NotFound();
        }

        target.Title = course.Title;
        target.Credits = course.Credits;
        target.DepartmentID= course.DepartmentID;

        try
        {
            var updatedCourse = _repo.Update(id, target);
            if (updatedCourse==0)
            {
                return StatusCode(500, new { message = "Update failed.  Breach in put conditions" });
            }

            return Ok(new
            {
                target.Title,
                target.Credits,
                target.DepartmentID
            });
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(new { message = "Validation failed at database layer.", detail = ex.InnerException?.Message ?? ex.Message });
        }




    }
}
