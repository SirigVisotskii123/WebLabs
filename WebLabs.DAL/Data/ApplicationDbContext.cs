using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebLabs.DAL.Entities;
namespace WebLabs.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public
        ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        { 
        }
    }
}