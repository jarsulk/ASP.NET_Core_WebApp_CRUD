using Microsoft.EntityFrameworkCore;
using WebAppRazor.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>(opts =>
{
	opts.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"));
	opts.EnableSensitiveDataLogging(true);
});
builder.Services.AddRazorPages();

var app = builder.Build();
app.UseStaticFiles();
app.MapControllers();
app.MapRazorPages();

var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
SeedData.SeedDatabase(context);

app.Run();
