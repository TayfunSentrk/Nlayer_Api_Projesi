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
    public class ProductFeatureConfiguration : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            //primary key olduğu yazıldı
            builder.HasKey(pf => pf.Id);
            //primary key birbir artması için gereken kod
            builder.Property(pf => pf.Id).UseIdentityColumn();
            //İlişkisel yapıları kontrol etmek için
            builder.HasOne(pf => pf.Product).WithOne(p => p.ProductFeature).HasForeignKey<ProductFeature>(pf => pf.ProductId);
        }
    }
}
