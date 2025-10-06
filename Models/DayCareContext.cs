using DayCareProject.Models;

using Microsoft.EntityFrameworkCore;

namespace DayCareProject.Data
{
    public class DayCareContext : DbContext
    {
        public DayCareContext(DbContextOptions<DayCareContext> options) : base(options) { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Kid> Kids { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<ChildApplication> ChildApplications { get; set; }


        public DbSet<Facility> Facilities { get; set; }

    }
}
