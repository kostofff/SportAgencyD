using BusinessLayer.Entities;
using DataLayer;
using DataLayer.AdsInterfaces;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Contexts;
using SportAgencyDApplication.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<SportAgencyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlOptions => sqlOptions.EnableRetryOnFailure()
    ));

builder.Services.AddScoped<UserIdentityRepository>();
builder.Services.AddScoped<UserIdentityContext>();

builder.Services.AddScoped<AthleteAdRepository>();
builder.Services.AddScoped<AthleteAdContext>();

builder.Services.AddScoped<ClubAdRepository>();
builder.Services.AddScoped<ClubAdContext>();

builder.Services.AddScoped<IApplicationService, ApplicationService>();

//builder.Services.AddScoped<AthletesApplicationController>();

builder.Services.AddScoped<UserManager<User>>();
builder.Services.AddScoped<SignInManager<User>>();
builder.Services.AddSingleton<IEmailSender, EmailSender>();




builder.Services.AddScoped<IUserStore<User>, UserStore<User, IdentityRole, SportAgencyDbContext>>();
builder.Services.AddScoped<IRoleStore<IdentityRole>, RoleStore<IdentityRole, SportAgencyDbContext>>();
builder.Services.AddScoped<SignInManager<User>>();


builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<SportAgencyDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
    options.ReturnUrlParameter = "ReturnUrl"; // Добави това за коректно пренасочване
});


var serviceProvider = builder.Services.BuildServiceProvider();
var logger = serviceProvider.GetService<ILogger<Program>>();

foreach (var service in builder.Services)
{
    logger.LogInformation($"Service: {service.ServiceType.FullName}, Lifetime: {service.Lifetime}, Implementation: {service.ImplementationType?.FullName}");
}






// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<SportAgencyDbContext>();
//    Console.WriteLine(dbContext.Database.CanConnect() ? "DB Connection Successful" : "DB Connection Failed");
//}


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

app.UseAuthentication();
app.UseAuthorization();

//app.UseStatusCodePages(async context =>
//{
//    await context.HttpContext.Response.WriteAsync(
//        $"Error: {context.HttpContext.Response.StatusCode}");
//});
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapDefaultControllerRoute();

app.Run();
