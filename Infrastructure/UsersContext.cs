using CesgranSec.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CesgranSec.Infrastructure;
public class UsersContext : IdentityDbContext<ApplicationUser>
{
    private readonly IConfiguration _configuration;
    public UsersContext(DbContextOptions<UsersContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(_configuration["ConnectionStrings:SqlServer"]);

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CryptoFiles>().HasKey(x => x.Id);
        modelBuilder.Entity<CryptoFiles>().HasOne(x => x.ApplicationUser)
            .WithMany(y => y.CryptoFiles)
            .HasForeignKey(x => x.ResponsibleUserId);

        modelBuilder.Entity<ApplicationUser>().HasKey(x => x.Id);
        modelBuilder.Entity<ApplicationUser>().HasMany(x => x.CryptoFiles)
            .WithOne(y => y.ApplicationUser)
            .HasForeignKey(y => y.ResponsibleUserId);
    }
    public DbSet<CryptoFiles> CryptoFiles { get; set; }
}
