using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ProductAttachmentsConfiguration : IEntityTypeConfiguration<ProductAttachments>
    {
        public void Configure(EntityTypeBuilder<ProductAttachments> builder)
        {
            builder.ToTable("ProductAttachments");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).ValueGeneratedOnAdd();
            builder.Property(x => x.Image).IsRequired();
            builder.HasOne(x => x.Product).WithMany(x => x.ProductAttachments)
                .HasForeignKey(x => x.ProductID).OnDelete(DeleteBehavior.Cascade).IsRequired();
          
        }
    }
}
