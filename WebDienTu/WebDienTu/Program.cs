using Microsoft.EntityFrameworkCore;
using WebDienTu.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 👉 Cấu hình DbContext
var connectionString = builder.Configuration.GetConnectionString("DienTuStoreConnection");
builder.Services.AddDbContext<DienTuStoreContext>(x => x.UseSqlServer(connectionString));

// 👉 Thêm cấu hình Session
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor(); // 👈 để dùng trong PartialView NavAdmin.cshtml

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // 👈 Kích hoạt session

app.UseAuthorization();

// 👉 Cấu hình cho Area trước
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// 👉 Cấu hình mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
