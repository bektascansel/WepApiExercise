using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NETCORE_7_WepApiProject.Models;

namespace NETCORE_7_WepApiProject.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options):base(options) { }

        // public DbSet<Character> Characters { get; set; }
        public DbSet<Character> Characters => Set<Character>();



    }
}
