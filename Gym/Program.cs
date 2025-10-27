using AutoMapper;
using GymManagementSystemCore.MappingProfiles;
using GymManagementSystemCore.Services.Classes;
using GymManagementSystemCore.Services.Interfaces;
using GymManagementSystemDAL.Data.DataSeed;
using GymManagementSystemDAL.Data.DbContexts;
using GymManagementSystemDAL.Repositories.Classes;
using GymManagementSystemDAL.Repositories.Interfaces;
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




            var app = builder.Build();


            //Data Seeding
            using var dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<GymDbContext>();
            var pendingMigrations = dbContext.Database.GetPendingMigrations();
            if(pendingMigrations?.Any() ?? false)  
            {
                dbContext.Database.Migrate();
            }
            GymDbContextDataSeeder.SeedData(dbContext);


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

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
