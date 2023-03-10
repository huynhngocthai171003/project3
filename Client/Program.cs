using Client.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ePRJContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("AAl")));
builder.Services.AddSession(
    op =>
    {
        op.IdleTimeout = new TimeSpan(24, 0, 0);
        op.Cookie.HttpOnly = true;
        op.Cookie.IsEssential = true;
    });

builder.Services.AddSession(cfg =>
{
    cfg.Cookie.Name = "fptaptech";
    cfg.IdleTimeout = new TimeSpan(0, 30, 0);
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
