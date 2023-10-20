using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
           builder.ToTable("Category");
           builder.HasKey(x => x.ID);
           builder.Property(b => b.ID).ValueGeneratedOnAdd();
           builder.Property(b => b.Name).HasMaxLength(1000).IsRequired();
         

        }
    }
}
