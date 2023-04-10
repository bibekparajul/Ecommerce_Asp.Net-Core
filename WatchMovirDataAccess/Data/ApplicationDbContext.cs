using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WatchMovie.Models;


namespace WatchMovieWeb.DataAccess
{
    public class ApplicationDbContext :IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        //ultimate level ma DbSet ma kaam garney ho
        public DbSet<Category> Categories { get; set; } 
        
        public DbSet<CoverType> CoverTypes { get; set; }    
        public DbSet<Product> Products { get; set; }    

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
