using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Story.Data;
using synaxnet;
using synaxnet.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(config =>
{
    config.UseLazyLoadingProxies();

    var provider = builder.Configuration.GetValue<string>("DatabaseProvider");
    switch (provider)
    {
        case "PostgreSQL":
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            config.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSql")

                );
            break;
        case "MySql":
            config.UseMySql(
                builder.Configuration.GetConnectionString("MySql"),
                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySql"))

                );
            break;
        case "SqlServer":
        default:
            config.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")

                );
            break;
    }
});
builder.Services.AddIdentity<AppUser, AppRole>(config =>
{
    config.Password.RequiredLength = builder.Configuration.GetValue<int>("Password:RequiredLength");
    config.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Password:RequireLowercase");
    config.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Password:RequireUppercase");
    config.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Password:RequireNonAlphanumeric");
    config.Password.RequireDigit = builder.Configuration.GetValue<bool>("Password:RequireDigit");
    config.Password.RequiredUniqueChars = builder.Configuration.GetValue<int>("Password:RequiredUniqueChars");

    config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    config.Lockout.MaxFailedAccessAttempts = 5;

    config.SignIn.RequireConfirmedEmail = true;

  

})

    .AddEntityFrameworkStores<AppDbContext>();
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

app.UseSynaxnet();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );
app.MapControllerRoute(
    name: "Product",
    pattern: "{name}-p-{id}",
    defaults: new { controller = "Home", action = "Product" }
    );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();