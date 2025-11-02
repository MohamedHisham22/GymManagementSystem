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
    internal class MemberConfig : GymUserConfig<Member> , IEntityTypeConfiguration<Member>
    {
        public new void Configure(EntityTypeBuilder<Member> builder)
        {
            base.Configure(builder);

            builder.Property(M => M.CreatedAt)
                   .HasColumnName("JoinDate")
                   .HasDefaultValueSql("SysDateTime()");
        }
    }
}
