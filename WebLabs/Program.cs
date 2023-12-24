using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebLabs.DAL.Entities;
using WebLabs.DAL.Data;
using WebLabs.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using WebLabs.Models;
using Serilog.Core;
using WebLabs.Extentions;
using System.Text.Json.Serialization;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        //builder.Logging.ClearProviders();
        //builder.Logging.AddConsole();

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        //    .AddEntityFrameworkStores<ApplicationDbContext>();
        //builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = false;
            options.SignIn.RequireConfirmedAccount = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireDigit = false;
        })
        .AddRoles<IdentityRole>()
        // .AddDefaultUI()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
        builder.Services.AddIdentityCore<ApplicationUser>();
        //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        //    .AddEntityFrameworkStores<ApplicationDbContext>();
        //builder.Services.AddControllersWithViews();



        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = $"/Identity/Account/Login";
            options.LogoutPath = $"/Identity/Account/Logout";
        });

        //builder.Services.AddScoped<SignInManager<IdentityUser>, SignInManager<IdentityUser>>();

        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(opt =>
        {
            opt.Cookie.HttpOnly = true;
            opt.Cookie.IsEssential = true;
        });
        builder.Services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        //builder.Services.AddScoped;
        builder.Services.AddScoped(sp => CartService.GetCart(sp));
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddSingleton<ICart, Cart>();
        builder.Services.AddControllersWithViews();

        //ILoggerFactory logger = LoggerFactory.Create(builder => builder.AddConsole());
        ////ILoggerFactory logger=new L;
        //logger.AddFile("Logs/log-{Date}.txt");


        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //Host.CreateDefaultBuilder(args)
        //.ConfigureWebHostDefaults(webBuilder =>
        //{
        //webBuilder.UseStartup<Startup>();
        //})
        //.ConfigureLogging(lp =>
        //{
        //	lp.ClearProviders();
        //	lp.AddFilter("Microsoft", LogLevel.None);
        //}

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("PolicyName", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseSession();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();
        await DbInitializer.SetupDb(app);


        //ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        //loggerFactory.AddFile("Logs/log-{Date}.txt");
        //ILogger logger = loggerFactory.CreateLogger<Program>();

        //app.Run(async (context) =>
        //{
        //	logger.LogInformation($"Requested Path: {context.Request.Path}");
        //	await context.Response.WriteAsync("Hello World!");
        //});

        var loggerFactory = app.Services.GetService<ILoggerFactory>();
        loggerFactory.AddFile(builder.Configuration["Logging:LogFilePath"].ToString());

        app.UseFileLogging();


        app.UseCors("PolicyName");
       


        app.Run();
    }
}

