using Microsoft.EntityFrameworkCore;
using Nlayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Data
{
    public class AppDbContext:DbContext
    {

        // Veritabanından oluşucak tabloların isimleri verildi

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }



        // veritabanı yolunu program cs'te vermek için bunu yaptım
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Data assembly içindeki tüm configration dosyalarını okur çalışmış olduğu dosyaları alır
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
         
            base.OnModelCreating(modelBuilder);
        }


    }
}
