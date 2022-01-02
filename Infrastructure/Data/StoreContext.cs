using System.Linq;
using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    // we need to add Entity Framework as a package into our application
    //goto Nuget Package Manager Ctrl+Shift+P and Open Nuget Gallery
    //Use dotnet –info and check the host version of Asp.net (actually use the target framework) and use the same for installing Entity Framework

    //we are abstracting our database away from our code, we do not directly query our database, we use DbContext methods to query database
    public class StoreContext : DbContext
    {
        public StoreContext( DbContextOptions<StoreContext> options) : base(options)
        {

        }
        //Pluralized name of the entity , its the convention , so products would be the name of the table created
        public DbSet<Product> Products{get;set;}
        public DbSet<ProductBrand> ProductBrands{get;set;}
        public DbSet<ProductType> ProductTypes{get;set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            if(Database.ProviderName=="Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach(var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties =entityType.ClrType.GetProperties().Where(p=>p.PropertyType==typeof(decimal));
                    foreach (var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name)
                        .HasConversion<double>();
                    }
                }
            }
        }
    }
}