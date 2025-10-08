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
    internal class SessionConfig : BaseConfig<Session> , IEntityTypeConfiguration<Session>
    {
        public new void Configure(EntityTypeBuilder<Session> builder)
        {
            base.Configure(builder);

            builder.ToTable(S => 
            {
                S.HasCheckConstraint("CapacityCheck", "[Capacity] between 1 and 25");
                S.HasCheckConstraint("EndDateCheck", "[EndDate] > [StartDate]");
            });

            builder.HasOne(S => S.Category)
                .WithMany(C => C.Sessions)
                .HasForeignKey(C => C.CategoryID);

            builder.HasOne(S => S.Trainer)
                .WithMany(T => T.Sessions)
                .HasForeignKey(T => T.TrainerID);


        }
    }
}
