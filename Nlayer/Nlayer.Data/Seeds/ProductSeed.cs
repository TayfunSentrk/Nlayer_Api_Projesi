using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nlayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Data.Seeds
{
    public class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(new Product()
            {
                Id = 1,
                Name = "Kalem1",
                CategoryId = 1,
                Price = 100,
                Stock = 20,
                CreatedDate = DateTime.Now,



            }, new Product()
            {
                Id = 2,
                Name = "Kalem2",
                CategoryId = 1,
                Price = 200,
                Stock = 20,
                CreatedDate = DateTime.Now,
            },
            new Product()
            {
                Id = 3,
                Name = "Kalem3",
                CategoryId = 1,
                Price = 5000,
                Stock = 20,
                CreatedDate = DateTime.Now,
            },
            new Product()
        {
                      Id = 4,
                 Name = "kitap1",
                 CategoryId = 2,
                 Price = 5000,
                 Stock = 20,
                 CreatedDate = DateTime.Now,
         },
               new Product()
               {
                   Id = 5,
                   Name = "kitap2",
                   CategoryId = 2,
                   Price = 5000,
                   Stock = 20,
                   CreatedDate = DateTime.Now,
               }

            );
        }
    }
}
