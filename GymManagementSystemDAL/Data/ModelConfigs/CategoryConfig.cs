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
    internal class CategoryConfig : BaseConfig<Category>,IEntityTypeConfiguration<Category>
    {
        public new void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);
            builder.Property(C => C.CategoryName)
                   .HasColumnType("varchar")
                   .HasMaxLength(20);
            
        }
    }
}
