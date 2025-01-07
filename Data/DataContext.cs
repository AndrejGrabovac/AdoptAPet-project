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
}