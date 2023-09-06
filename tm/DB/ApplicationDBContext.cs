using Microsoft.EntityFrameworkCore;
using TaskManagement.Models;

namespace TaskManagement.DB
{
    public class ApplicationDBContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tasks>()
           .HasMany(t => t.Comments)
           .WithOne(c => c.Task)
           .HasForeignKey(c => c.TaskId)
           .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Login>()
                .HasMany(p => p.Tasks)
                .WithOne(c => c.Login)
                .HasForeignKey(c => c.LoginId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Login>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Login)
                .HasForeignKey(c => c.LoginId)
                .OnDelete(DeleteBehavior.NoAction);
            

            modelBuilder.Entity<Project>()
            .HasMany(p => p.Tasks)
            .WithOne(t => t.Project)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.NoAction);
            /* 
             *
            */

        }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<Login> Login { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Models.Tasks> Tasks{ get; set; }

        public DbSet<Comment> Comments { get; set; }
        internal string Find(int assignedTo)
        {
            throw new NotImplementedException();
        }
    }
}
