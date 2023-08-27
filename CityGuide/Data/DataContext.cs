using CityGuide.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CityGuide.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options) { }
        public DbSet<Value> Values{ get; set; }
        public DbSet<City> Cities{ get; set; }
        public DbSet<Photo> Photos{ get; set; }
        public DbSet<User> Users{ get; set; }
    }
}
