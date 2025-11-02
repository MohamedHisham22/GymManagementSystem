using GymManagementSystemDAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Data.ModelConfigs
{
    internal class MemberPlanConfig : IEntityTypeConfiguration<MemberPlan>
    {
        public void Configure(EntityTypeBuilder<MemberPlan> builder)
        {
            builder.ToTable("MemberShip").HasKey(MP => new
            {
                MP.MemberId,
                MP.PlanId,
                MP.CreatedAt,
            });

            builder.Ignore(MP => MP.Id);

            builder.HasOne(MP => MP.Member)
                   .WithMany(M => M.MemberShips)
                   .HasForeignKey(M => M.MemberId);

            builder.HasOne(MP => MP.Plan)
                  .WithMany(P => P.MemberShips)
                  .HasForeignKey(M => M.PlanId);

            builder.Property(MP => MP.CreatedAt)
                   .HasColumnName("StartDate")
                   .HasDefaultValueSql("SysDateTime()");

        }
    }
}

