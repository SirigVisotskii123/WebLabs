using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using WebLabs.DAL.Entities;
namespace WebLabs.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {

        }
        public DbSet<Dish> Dish { get; set; }
		public DbSet<DishGroup> DishGroup { get; set; }
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        { 

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-WebLabsV05-Data-Ident-App;Trusted_Connection=True;MultipleActiveResultSets=true");
            //base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
           

            base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Dish>().ToTable("Dish");
			modelBuilder.Entity<DishGroup>().ToTable("DishGroup");
		}
		//protected override void OnModelCreating(ModelBuilder modelBuilder)
		//{
		//	modelBuilder.Entity<Dish>().ToTable("Dish");
		//	modelBuilder.Entity<DishGroup>().ToTable("DishGroup");
		//}
	}
}