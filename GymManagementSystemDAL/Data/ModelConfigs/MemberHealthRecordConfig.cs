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
    internal class MemberHealthRecordConfig : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            builder.ToTable("Members")
                    .HasKey(x => x.Id);

            builder.Property(HR => HR.BloodType)
                   .HasConversion(typeof(string))
                   .HasColumnType("varchar")
                   .HasMaxLength(10);


            builder.HasOne<Member>()
                    .WithOne(M => M.HealthRecord)
                    .HasForeignKey<HealthRecord>(HR => HR.Id);
        }
    }
}
