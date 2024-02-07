using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using SkillSheetManager.Controllers;
using SkillSheetManager.DAL;
using SkillSheetManager.DAO;
using SkillSheetManager.Utility;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllersWithViews().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

    builder.Services.AddDbContext<UsersDetailsDBContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(option =>
        {
            option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            option.LoginPath = "/Auth/Index";
            option.AccessDeniedPath = "/Auth/Index";
        });

    builder.Services.AddSession(option =>
    {
        option.IdleTimeout = TimeSpan.FromMinutes(5);
        option.Cookie.HttpOnly = true;
        option.Cookie.IsEssential = true;
    });

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // DAO services.
    builder.Services.AddScoped<UserDAO>();
    builder.Services.AddScoped<UserDetailsDAO>();

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

    app.UseSession();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Auth}/{action=Index}/{id?}");

    app.Run();
}
catch(Exception objException)
{
    string type = objException.GetType().Name;
    if (type.Equals(Constants.STOP_HOST_EXCEPTION, StringComparison.Ordinal)) //To check the stop host exception.
    {
        logger.Error(objException, Constants.MSG_STOP_HOST_EXCEPTION);
        throw;
    }
}
finally
{
    LogManager.Shutdown();
}