using API.Models.DataManager;
using API.Models.Repositories;
using Microsoft.Extensions.Options;
using PracticeExamV2.Data;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(options =>
{
         options.UseSqlServer(builder.Configuration.GetConnectionString("Context"));
});

	// Add services to the container.
builder.Services.AddScoped<ICoursesRepository, CourseManager>();
builder.Services.AddScoped<IInstructorRepository, InstructorManager>();


builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options =>
{
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
         options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
 });


// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();



// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
