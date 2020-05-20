using System;
using Microsoft.EntityFrameworkCore;
using StreetJams.Entities;

namespace StreetJams.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Song> Songs { get; set; }
    }
}
