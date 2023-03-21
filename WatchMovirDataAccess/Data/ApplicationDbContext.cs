﻿using Microsoft.EntityFrameworkCore;
using WatchMovie.Models;


namespace WatchMovieWeb.DataAccess
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        //ultimate level ma DbSet ma kaam garney ho
        public DbSet<Category> Categories { get; set; }  
    }
}