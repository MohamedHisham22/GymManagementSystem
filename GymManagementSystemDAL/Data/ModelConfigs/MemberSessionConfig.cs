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
    internal class MemberSessionConfig : IEntityTypeConfiguration<MemberSession>
    {
        public void Configure(EntityTypeBuilder<MemberSession> builder)
        {
            builder.ToTable("Booking").HasKey(MS=> new 
            {
                MS.MemberId,
                MS.SessionId,
            });

            builder.Ignore(MS => MS.Id);

            builder.HasOne(MS => MS.Member)
                   .WithMany(M => M.Bookings)
                   .HasForeignKey(M => M.MemberId);

            builder.HasOne(MS => MS.session)
                  .WithMany(S => S.Bookings)
                  .HasForeignKey(M => M.SessionId);

            builder.Property(MS => MS.CreatedAt)
                   .HasColumnName("BookingDate")
                   .HasDefaultValueSql("SysDateTime()");

        }
    }
}
