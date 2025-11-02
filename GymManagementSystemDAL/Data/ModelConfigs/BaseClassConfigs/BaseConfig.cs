using GymManagementSystemDAL.Models.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Data.ModelConfigs.BaseClassConfigs
{
    internal class BaseConfig<T> : IEntityTypeConfiguration<T> where T : Base
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(B => B.CreatedAt)
                   .HasDefaultValueSql("SysDateTime()");
        }
    }
}
