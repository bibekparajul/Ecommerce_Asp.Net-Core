using Microsoft.EntityFrameworkCore;
using WacthMovie.DataAccess.Repository;
using WacthMovie.DataAccess.Repository.IRepository;
using WatchMovieWeb.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//the below code is used to integrate the apppdbcontext and the connectionstring ....

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

//unit of work in action 
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();  //
 

//this below code is used when we add the new razor runtime compilation package..which
//in this case is the  new nav bar from the solar themeee and nav inside the _layout.cshtml
builder.Services.AddRazorPages();


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
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
