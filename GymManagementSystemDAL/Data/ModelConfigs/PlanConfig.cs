using GymManagementSystemDAL.Data.ModelConfigs.BaseClassConfigs;
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
    internal class PlanConfig : BaseConfig<Plan>,IEntityTypeConfiguration<Plan>
    {
        public new void Configure(EntityTypeBuilder<Plan> builder)
        {
            base.Configure(builder);

            builder.Property(P => P.Name)
                   .HasColumnType("varchar")
                   .HasMaxLength(50);

            builder.Property(P => P.Description)
                   .HasColumnType("varchar")
                   .HasMaxLength(200);

            builder.Property(P => P.Name)
                   .HasPrecision(10, 2);

            builder.ToTable(P =>
            {
                P.HasCheckConstraint("DurationDaysCheck", "[DurationDays] between 1 and 365");
            });
        }
    }
}
