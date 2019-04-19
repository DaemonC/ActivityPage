using Microsoft.EntityFrameworkCore;

namespace DojoActivities.Models
{
    public class DojoActivityContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public DojoActivityContext (DbContextOptions<DojoActivityContext> options) : base (options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Activ> Activs { get; set; }
        public DbSet<Join> Joins { get; set; }

    }
}