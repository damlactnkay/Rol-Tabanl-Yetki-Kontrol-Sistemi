using Application.Interfaces.Genel;
using Application.Interfaces.Ozel;
using Connect.Genel;
using Connect.Ozel;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// Dependency Injection kayýtlarý
builder.Services.AddScoped<IConnectionProivder, ConnectionProvider>();
builder.Services.AddScoped<IXConnectionProvider, XConnection>();
builder.Services.AddScoped<ISQLXConneciton, SQLXConnection>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IMakineRepository, MakineRepository>();
builder.Services.AddScoped<IVardiyaRepository, VardiyaRepository>();
builder.Services.AddScoped<IBolumRepository, BolumRepository>();
// Cookie Authentication ekle
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // giriþ yapýlmamýþsa buraya gider
        options.AccessDeniedPath = "/Account/AccessDenied"; // yetkisiz eriþim olursa
    });

var app = builder.Build();

// HTTP pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// **Authentication önce**
app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();

