using Microsoft.EntityFrameworkCore;
using WatchMovieWeb.Models;

namespace WatchMovieWeb.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }  
    }
}
