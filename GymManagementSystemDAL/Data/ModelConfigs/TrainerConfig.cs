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
    internal class TrainerConfig : GymUserConfig<Trainer> , IEntityTypeConfiguration<Trainer>
    {
        public new void Configure(EntityTypeBuilder<Trainer> builder)
        {
            base.Configure(builder);

            builder.Property(T => T.CreatedAt)
                   .HasColumnName("HireDate")
                   .HasDefaultValueSql("SysDateTime()");
        }
    }
}
