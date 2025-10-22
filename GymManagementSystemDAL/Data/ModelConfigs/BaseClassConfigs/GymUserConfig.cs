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
    internal class GymUserConfig<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(GU=>GU.Name)
                   .HasColumnType("varchar")
                   .HasMaxLength(50);

            builder.Property(GU => GU.Email)
                   .HasColumnType("varchar")
                   .HasMaxLength(100);

            builder.Property(GU => GU.Phone)
                   .HasColumnType("varchar")
                   .HasMaxLength(11);
            
            builder.Property(GU => GU.Gender)
                   .HasConversion(typeof(string))
                   .HasColumnType("varchar")
                   .HasMaxLength(10);

            builder.ToTable(GU => 
            { 
                GU.HasCheckConstraint("EmailCheck" , "[Email] like '_%@_%._%'");
                GU.HasCheckConstraint("PhoneCheck" , "[Phone] not like '%[^0-9]%' and [Phone] like '01%'");
            });

            builder.HasIndex(GU => GU.Email)
                   .IsUnique(true);

            builder.HasIndex(GU => GU.Phone)
                   .IsUnique(true);

            builder.OwnsOne(GU=>GU.Address , ABuilder => 
            { 
                ABuilder.Property(A=>A.Street)
                        .HasColumnName("Street")
                        .HasColumnType("varchar")
                        .HasMaxLength(30);

                ABuilder.Property(A => A.City)
                        .HasColumnName("City")
                        .HasColumnType("varchar")
                        .HasMaxLength(30);

                ABuilder.Property(A => A.BuildingNumber)
                        .HasColumnName("BuildingNumber");
            });
        }
    }
}
