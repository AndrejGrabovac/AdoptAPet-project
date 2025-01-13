using AdoptAPet.Models;
using Microsoft.EntityFrameworkCore;

namespace AdoptAPet.Data;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Shelter> Shelters { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Breed> Breeds { get; set; }
    public DbSet<AdoptionRequest> AdoptionRequests { get; set; }
    
    public DbSet<RefreshToken> RefreshToken { get; set; }
    
    public DbSet<Role> Roles { get; set; }
    
    public DataContext(DbContextOptions options) : base(options)
    {
        
    }
    /*
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer("DefaultConnection");
        //options.UseSqlServer("Server=localhost,1433;Database=PetAdoptionDB;User Id=sa;Password=dockerStrongPwd123; TrustServerCertificate=True");
    }
    */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin" },
            new Role { Id = 2, Name = "User" }
        );

        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);

        modelBuilder.Entity<User>()
            .Property(u => u.RoleId)
            .HasDefaultValue(2);
    }
}