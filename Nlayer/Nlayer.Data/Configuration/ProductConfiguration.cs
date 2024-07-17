using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nlayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //primary key olduğu yazıldı
            builder.HasKey(p => p.Id);
            //primary key birbir artması için gereken kod
            builder.Property(p => p.Id).UseIdentityColumn();

            //name property zorunlu olduğu yazıldı
            builder.Property(p => p.Name).IsRequired();
            //name property en fazla 200 karakter olduğu yazıldı
            builder.Property(p => p.Name).HasMaxLength(200);
            //stock property zorunlu olduğu yazıldı
            builder.Property(p=>p.Stock).IsRequired();
            //Price property zorunlu olduğu yazıldı
            builder.Property(p=>p.Price).IsRequired();
            //price property decimal tipinde virgülden sonra 2 karakter toplamda 18 karakter
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            //Category tablosu ilişiki olduğu için ilişkiler verildi
            builder.HasOne(p=>p.Category).WithMany(c=>c.Products).HasForeignKey(p=>p.CategoryId);
          
        }
    }
}
