using CRUDProductCatalog.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUDProductCatalog
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Specialist> Specialists { get; set; }
    }
}