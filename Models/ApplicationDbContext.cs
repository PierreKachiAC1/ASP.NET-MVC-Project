using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FinalProject.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasOne(a => a.Session)
                      .WithMany(s => s.Attendances)
                      .HasForeignKey(a => a.SessionId);

                entity.HasOne(a => a.User)
                      .WithMany(u => u.Attendances)
                      .HasForeignKey(a => a.UserId);

                //entity.HasOne(a => a.)
                //      .WithMany()
                //      .HasForeignKey(a => a.MarkedBy)
                //      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable(name: "User");

                entity.Property(u => u.Username)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(u => u.Password)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(u => u.FullName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.Role)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(u => u.IsActive)
                      .IsRequired();
            });
            modelBuilder.Entity<Session>(entity =>
            {
                entity.Property(s => s.Active).HasDefaultValue(true);


            });
        }
    }
}
