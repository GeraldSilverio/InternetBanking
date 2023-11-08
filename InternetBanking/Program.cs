using InternetBanking.Core.Application;
using InternetBanking.Infraestructure.Persistence;
using InternetBanking.Infraestructure.Identity;
using InternetBanking.Infraestructure.Shared;
using WebApp.InternetBanking.Middlewares;
using Microsoft.AspNetCore.Identity;
using InternetBanking.Infraestructure.Identity.Entities;
using InternetBanking.Infraestructure.Identity.Seeds;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddApplicationLayer();
builder.Services.AddPersistenceInfraestructure(builder.Configuration);
builder.Services.AddSharedInfraestructure(builder.Configuration);
builder.Services.AddIdentityInfraestructure(builder.Configuration);
builder.Services.AddScoped<LoginAuthorize>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ValidateUserSession, ValidateUserSession>();
builder.Services.AddSession();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultRoles.SeedAsync(userManager, rolesManager);
        await DefaultAdminUser.SeedAsync(userManager, rolesManager);
        await DefaultClientUser.SeedAsync(userManager, rolesManager);
    }
    catch (Exception ex)
    {

        throw new Exception(ex.Message);
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();

/* 
Russel Brian 2021-1742
Cristopher Alexis 2022-1024
Gerald Silverio 2022-1068*/