using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using _252936_MVC.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<_252936_MVCContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("_252936_MVCContext")));

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.Run();
