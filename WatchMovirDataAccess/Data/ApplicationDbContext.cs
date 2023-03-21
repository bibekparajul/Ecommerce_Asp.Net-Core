using Microsoft.EntityFrameworkCore;
using WatchMovie.Models;


namespace WatchMovieWeb.DataAccess
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }  
    }
}
