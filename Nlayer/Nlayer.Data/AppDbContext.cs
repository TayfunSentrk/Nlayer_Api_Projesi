using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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





        // SaveChangesAsync metodu, temel sınıfın SaveChangesAsync metodunu geçersiz kılar
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // İzlenen tüm varlıkları dolaşır
            foreach (var item in ChangeTracker.Entries())
            {
                // Varlığın BaseEntity türünde olup olmadığını kontrol eder
                if (item.Entity is BaseEntity entityReference)
                {
                    // Varlığın durumunu belirler (Added veya Modified)
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                // Varlık eklenmişse, CreatedDate'i mevcut tarih ve saat olarak ayarlar
                                Entry(entityReference).Property(x => x.UpdatedDate).IsModified = false;
                                entityReference.CreatedDate = DateTime.Now;
                                break;
                            }

                        case EntityState.Modified:
                            {
                                // Varlık değiştirilmişse, CreatedDate'in değiştirilmesini engeller
                                Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;
                                // UpdatedDate'i mevcut tarih ve saat olarak ayarlar
                                entityReference.UpdatedDate = DateTime.Now;
                                break;
                            }
                    }
                }
            }
            // Temel sınıfın SaveChangesAsync metodunu çağırır
            return base.SaveChangesAsync(cancellationToken);
        }

        // SaveChanges metodu, temel sınıfın SaveChanges metodunu geçersiz kılar
        public override int SaveChanges()
        {
            // İzlenen tüm varlıkları dolaşır
            foreach (var item in ChangeTracker.Entries())
            {
                // Varlığın BaseEntity türünde olup olmadığını kontrol eder
                if (item.Entity is BaseEntity entityReference)
                {
                    // Varlığın durumunu belirler (Added veya Modified)
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                // Varlık eklenmişse, CreatedDate'i mevcut tarih ve saat olarak ayarlar
                                Entry(entityReference).Property(x => x.UpdatedDate).IsModified = false;
                                entityReference.CreatedDate = DateTime.Now;
                                break;
                            }

                        case EntityState.Modified:
                            {
                                // Varlık değiştirilmişse, CreatedDate'in değiştirilmesini engeller
                                Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;
                                // UpdatedDate'i mevcut tarih ve saat olarak ayarlar
                                entityReference.UpdatedDate = DateTime.Now;
                                break;
                            }
                    }
                }
            }
            // Temel sınıfın SaveChanges metodunu çağırır
            return base.SaveChanges();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Data assembly içindeki tüm configration dosyalarını okur çalışmış olduğu dosyaları alır
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
         
            base.OnModelCreating(modelBuilder);
        }


    }
}
