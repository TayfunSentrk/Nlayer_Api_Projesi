using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nlayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            //primary key olduğu yazıldı
            builder.HasKey(c => c.Id);
            //primary key sütunun bir bir artması için gereken kod
            builder.Property(c=>c.Id).UseIdentityColumn();
            //name property  zorunlu olduğu yazıldı
            builder.Property(c => c.Name).IsRequired();
            //name property en fazla 50 karakter olduğu yazıldı
            builder.Property(c => c.Name).HasMaxLength(50);
            //tablo ismi belirtildi
            builder.ToTable("Categories");
        }
    }
}
