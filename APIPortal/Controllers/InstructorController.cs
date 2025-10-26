using Microsoft.AspNetCore.Mvc;
using APIPortal.ViewModel;
using PracticeExamV2.Models;
using PracticeExamV2.Utilities;
using System.Threading.Tasks;


namespace APIPortal.Controllers;


public class InstructorController : Controller
{
    private readonly HttpClient _client;
    private const string InstructorBasePath = "/api/instructor";

    public InstructorController(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient("API");
    }


    public async Task<IActionResult> Index()
    {
        var instructors = await _client.GetFromJsonAsync<List<Instructor>>(InstructorBasePath);
        return View(instructors ??  new List<Instructor>());
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var instructor = await _client.GetFromJsonAsync<Instructor>($"{InstructorBasePath}/{id}");
        if (instructor == null)
        {
            return NotFound();
        }

        return View(instructor);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstName,EnrollmentDate")] Instructor instructors)
    {
        if (!ModelState.IsValid)
        {
            return View(instructors);
        }
        var current = await _client.GetFromJsonAsync<Instructor>($"{InstructorBasePath}/{id}");

        if (current == null)
        {
            return NotFound();
        }
        var payload = new
        {
            LastName = instructors.LastName,
            FirstName = instructors.FirstName,
            EnrollmentDate = instructors.EnrollmentDate
        };

        var response = await _client.PutAsJsonAsync($"{InstructorBasePath}/{id}", payload);

        if (!response.IsSuccessStatusCode)
        {
            var detail = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"API Error {(int)response.StatusCode}: {detail}");
            return View(instructors);
        }
        return RedirectToAction(nameof(Index));
    }
}
