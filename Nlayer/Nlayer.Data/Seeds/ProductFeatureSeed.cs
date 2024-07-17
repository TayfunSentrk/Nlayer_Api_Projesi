using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nlayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Data.Seeds
{
    public class ProductFeatureSeed : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.HasData(new ProductFeature()
            {
                Id = 1,
                Color = "kirmizi",
                Height = 200,
                Weight = 100,
                ProductId = 1,
            },
            new ProductFeature()
            {
                Id = 2,
                Color = "mavi",
                Height = 200,
                Weight = 300,
                ProductId = 2,
            });
        }
    }
}
