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
    }
}