using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Models;
using Repository;
using System.Data.Entity;

public class Program
{
    public static int Main(string[] args)
    {
        WebApplicationBuilder builder =
             WebApplication.CreateBuilder();

        builder.Services.AddDbContext<Models.MyDbContext>(i =>
        {
            i.UseLazyLoadingProxies().UseSqlServer(
                builder.Configuration.GetConnectionString("MyDB"));
        });

        builder.Services.AddIdentity<User, IdentityRole>(i => {
            i.Lockout.MaxFailedAccessAttempts = 2;
            i.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
            i.User.RequireUniqueEmail = true;
            i.SignIn.RequireConfirmedPhoneNumber = false;
            i.SignIn.RequireConfirmedEmail = false;
            i.SignIn.RequireConfirmedAccount = false;
            i.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
        })
            .AddEntityFrameworkStores<Models.MyDbContext>()
            .AddDefaultTokenProviders();
        builder.Services.Configure<IdentityOptions>(i =>
        {
            i.Password.RequireNonAlphanumeric = false;
            i.Password.RequireUppercase = false;

        });
        builder.Services.ConfigureApplicationCookie(i =>
        {
            i.LoginPath = "/Account/SignIn";

        });
        builder.Services.AddScoped(typeof(UnitOfWork));
        builder.Services.AddScoped(typeof(ProductManager));
        builder.Services.AddScoped(typeof(CategoryManager));
        builder.Services.AddScoped(typeof(AccountManager));
        builder.Services.AddScoped(typeof(RoleManager));
        builder.Services.AddScoped<IUserClaimsPrincipalFactory<User>, UserClaimsFactory>();
        builder.Services.AddControllers();
        var webApp = builder.Build();
        webApp.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory() + "/Content"),
            RequestPath = ""
        });
        webApp.UseRouting();
        webApp.UseAuthentication();
        webApp.UseAuthorization();
        webApp.MapControllerRoute("Default", "{Controller=Product}/{Action=Index}/{id?}");
        webApp.Run();


        return 0;
    }
}