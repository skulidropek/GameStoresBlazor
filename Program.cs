using GameStoresBlazor.Models.User;
using GameStoresBlazor.Services.DataBase;
using GameStoresBlazor.Services.Transactions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddBootstrapBlazor();
builder.Services.AddServerSideBlazor();

builder.Services.AddControllersWithViews();

builder.Logging.AddFilter("Microsoft.AspNetCore.Authentication", LogLevel.Debug);

builder.Services.AddAuthorization();
builder.Services.AddAuthentication()
    .AddSteam(options =>
    {
        options.ApplicationKey = "";
        options.CallbackPath = new PathString("/signin-steam");

        options.Events = new AspNet.Security.OpenId.OpenIdAuthenticationEvents
        {
            OnAuthenticated = async context =>
            {
                // ��������� �������������� ������������ Steam
                var steamId = context.Identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (steamId == null)
                {
                    // ������ ��������� ������
                    throw new Exception("������ ��� �������������� ����� Steam");
                }

                //context.Identity.Name

                var normalizeSteamId = Regex.Replace(steamId, @"\D", "");

                // ������ ������ ��� �������� ������������ � ����� �������
                var userManager = context.HttpContext.RequestServices.GetService<UserManager<SteamIdentityUserModel>>();
                var signInManager = context.HttpContext.RequestServices.GetService<SignInManager<SteamIdentityUserModel>>();

                var user = await userManager.FindByLoginAsync("Steam", normalizeSteamId);
                if (user == null)
                {
                    user = new SteamIdentityUserModel { SteamId = normalizeSteamId, Name = context.Identity.Name };
                    var createResult = await userManager.CreateAsync(user);
                    if (!createResult.Succeeded)
                    {
                        throw new Exception("�� ������� ������� ������������");
                    }

                    var addLoginResult = await userManager.AddLoginAsync(user, new UserLoginInfo("Steam", normalizeSteamId, "Steam"));
                    if (!addLoginResult.Succeeded)
                    {
                        throw new Exception("�� ������� �������� ���� Steam");
                    }
                }
                await signInManager.SignInAsync(user, isPersistent: false);
            },
        };

    });

builder.Services.AddDbContext<DataBaseContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("UsersDbConnection"), npgsqlOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorCodesToAdd: null);
    });
}, ServiceLifetime.Transient);

builder.Services.AddScoped<TransactionService>();

//builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<DataBaseContext>();
//builder.Services.AddIdentity<SteamIdentityUserModel, IdentityRole>()
//    .AddEntityFrameworkStores<DataBaseContext>()
//    .AddDefaultTokenProviders();
builder.Services.AddDefaultIdentity<SteamIdentityUserModel>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;

    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;

})
.AddRoles<IdentityRole>()
//.AddDefaultTokenProviders()
.AddEntityFrameworkStores<DataBaseContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromHours(36);

    options.LoginPath = "/auth/steam"; // Укажите здесь ваш путь к странице входа
    options.AccessDeniedPath = "/auth/steam"; // Укажите здесь ваш путь к странице "Доступ запрещен"
    options.SlidingExpiration = true;
});

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.Zero;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute(
       name: "default",
       pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapRazorPages();

app.Run();
