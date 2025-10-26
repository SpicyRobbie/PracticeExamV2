using Microsoft.AspNetCore.Mvc;
using APIPortal.ViewModel;
using PracticeExamV2.Models;
using PracticeExamV2.Utilities;
using System.Threading.Tasks;


namespace APIPortal.Controllers;


public class CourseController : Controller
{
    private readonly HttpClient _client;
    private const string CourseBasePath = "/api/course";

    public CourseController(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient("API");
    }


    public async Task<IActionResult> Index()
    {
        var courses = await _client.GetFromJsonAsync<List<Course>>(CourseBasePath);
        return View(courses ??  new List<Course>());
    }

    [HttpGet]
    public async Task<IActionResult> Edit (int id)
    {
        var course = await _client.GetFromJsonAsync<Course>($"{CourseBasePath}/{id}");
        if (course == null)
        {
            return NotFound();
        }

        return View(course);
    }

    [HttpPost]
    public async Task<IActionResult> Edit (int id, [Bind("CourseID,Title,Credits,DepartmentID")] Course courses)
    {
        if(!ModelState.IsValid)
        {
            return View(courses);
        }
        var current = await _client.GetFromJsonAsync<Course>($"{CourseBasePath}/{id}");

        if(current == null)
        {
            return NotFound();
        }
        var payload = new
        {
            Title = courses.Title,
            Credits = courses.Credits,
            DepartmentID = courses.DepartmentID
        };

        var response = await _client.PutAsJsonAsync($"{CourseBasePath}/{id}", payload);

        if(!response.IsSuccessStatusCode)
        {
            var detail = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"API Error {(int)response.StatusCode}: {detail}");
            return View(detail);
        }
        return RedirectToAction(nameof(Index));
    }
}
