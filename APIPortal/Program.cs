using Microsoft.EntityFrameworkCore;
using PracticeExamV2.Data;

var builder = WebApplication.CreateBuilder(args);

// Named HttpClient for calling your API
builder.Services.AddHttpClient("API", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["API:BaseURL"]!);
});

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Context")));


builder.Services.AddControllersWithViews().ConfigureApplicationPartManager(apm =>
{
    var thisAssembly = typeof(Program).Assembly.GetName().Name;
    var extras = apm.ApplicationParts.Where(p => !string.Equals(p.Name, thisAssembly, StringComparison.OrdinalIgnoreCase)).ToList();
    foreach (var part in extras)
        apm.ApplicationParts.Remove(part);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization(); 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
