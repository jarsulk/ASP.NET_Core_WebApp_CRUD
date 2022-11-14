using Microsoft.EntityFrameworkCore;
using WebAppRazor.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>(opts =>
{
	opts.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"));
	opts.EnableSensitiveDataLogging(true);
});
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.MapDefaultControllerRoute();
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
SeedData.SeedDatabase(context);

app.Run();
