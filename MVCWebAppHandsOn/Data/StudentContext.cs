using Microsoft.EntityFrameworkCore;
using MVCWebAppHandsOn.Models;

namespace MVCWebAppHandsOn.Data
{
    public class StudentContext: DbContext
    {
        public StudentContext (DbContextOptions<StudentContext> options)
            : base(options) { }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Student");
        }
    }
}
