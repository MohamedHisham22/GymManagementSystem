using GymManagementSystemDAL.Models.AuthModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Data.DbContexts
{
    public class GymAuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public GymAuthDbContext(DbContextOptions<GymAuthDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>(AU => 
            {
                AU.Property(AU => AU.FirstName)
                  .HasColumnType("varchar")
                  .HasMaxLength(30);
                AU.Property(AU => AU.LastName)
                  .HasColumnType("varchar")
                  .HasMaxLength(30);
            });

        }

    }
}
