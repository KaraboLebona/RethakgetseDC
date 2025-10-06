using DayCareProject.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Configure DbContext (replace "DbConn" with your connection string name in appsettings.json)
//builder.Services.AddDbContext<DayCareContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConn")));
builder.Services.AddDbContext<DayCareContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


// Enable session for login/authentication
builder.Services.AddDistributedMemoryCache(); // Required for session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Optional: Needed if you want to access HttpContext in services/controllers
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable session middleware
app.UseSession();

app.UseAuthorization();

// Set default route to Admin Login page
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=About}/{id?}");

// Optional: Dashboard route (not strictly needed since default routing handles it)
app.MapControllerRoute(
    name: "dashboard",
    pattern: "{controller=Admin}/{action=Dashboard}/{id?}");

app.Run();
