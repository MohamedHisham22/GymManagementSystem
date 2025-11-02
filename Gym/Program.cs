using AutoMapper;
using GymManagementSystemCore.MappingProfiles;
using GymManagementSystemCore.Services.AttachmentService;
using GymManagementSystemCore.Services.AuthService;
using GymManagementSystemCore.Services.Classes;
using GymManagementSystemCore.Services.Interfaces;
using GymManagementSystemDAL.Data.DataSeed;
using GymManagementSystemDAL.Data.DbContexts;
using GymManagementSystemDAL.Models.AuthModels;
using GymManagementSystemDAL.Repositories.Classes;
using GymManagementSystemDAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gym
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<GymDbContext>(options => 
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<GymAuthDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<GymAuthDbContext>();
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login"; //Used when a user is not authenticated(not logged in) and tries to access a page need authentication(e.g., a page decorated with [Authorize]).
                options.AccessDeniedPath = "/Account/AccessDenied"; //Used when a user is authenticated (logged in) but tried to access a paged that needs a specific role and he is not authorized (doesn’t have the required role or policy which requires [Authorize(Roles="RoleName")]).
            });
            builder.Services.AddAutoMapper(X => X.AddMaps(typeof(MemberMappingProfile).Assembly));
            builder.Services.AddScoped<IHealthRecordRepo, HealthRecordRepo>();
            builder.Services.AddScoped<ISessionRepo, SessionRepo>();
            builder.Services.AddScoped<IMembershipRepo, MembershipRepo>();
            builder.Services.AddScoped<IMemberSessionRepo, MemberSessionRepo>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IHomeServices , HomeServices>();
            builder.Services.AddScoped<IMemberServices, MemberServices>();
            builder.Services.AddScoped<ITrainerServices, TrainerServices>();
            builder.Services.AddScoped<IPlanServices, PlanServices>();
            builder.Services.AddScoped<ISessionServices, SessionServices>();
            builder.Services.AddScoped<IMemberShipServices, MemberShipServices>();
            builder.Services.AddScoped<IMemberSessionsServices, MemberSessionsServices>();
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();
            builder.Services.AddScoped<IAccountService, AccountService>();




            var app = builder.Build();





            //Data Seeding
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<GymDbContext>();
            var pendingMigrations = dbContext.Database.GetPendingMigrations();
            if(pendingMigrations?.Any() ?? false)  
            {
                dbContext.Database.Migrate();
            }
            GymDbContextDataSeeder.SeedData(dbContext);

            var authDbContext = scope.ServiceProvider.GetRequiredService<GymAuthDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var pendingAuthMigrations = authDbContext.Database.GetPendingMigrations();
            if (pendingAuthMigrations?.Any() ?? false)
            {
                authDbContext.Database.Migrate();
            }
            GymAuthDbContextDataSeeder.SeedData(roleManager, userManager);





            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
