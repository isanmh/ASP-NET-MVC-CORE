using Microsoft.EntityFrameworkCore;
using MvcApp.Data;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MovieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection") ?? throw new InvalidOperationException("Connection string 'MovieContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Untuk koneksi database
builder.Services.AddDbContext<EmployeeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Custom Routing 
app.MapGet("/hello", () => "Hello World");
app.MapGet("/hai", () => "Hai Dunia");
// app.MapGet("/salam/{name}", (string name) => $"Hai, nama saya {name}");

// localhost:5000/salam?nama=budi&umur=20?Input=""
app.MapGet("/welcome", async context =>
{
    var nama = context.Request.Query["nama"];
    var umur = context.Request.Query["umur"];
    var res = $"Hai, nama saya {nama}, umur saya {umur}";
    await context.Response.WriteAsync(res);
});

app.Run();
