using BusinessLayer.Models;
using BusinessLayer.Models.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer;

/// <summary>
/// EF Data Context
/// </summary>
public class DataContext : DbContext
{
  /// <summary>
  /// Configuration
  /// </summary>
  protected readonly IConfiguration Configuration;

    /// <summary>
    /// DBset Address
    /// </summary>
    /// <value>Address set</value>
    public DbSet<Address>? Address { get; set; }

    /// <summary>
    /// DBset of Hotels
    /// </summary>
    /// <value>Hotels set</value>
    public DbSet<Hotels>? Hotels { get; set; }

    /// <summary>
    /// DBset of Users
    /// </summary>
    /// <value>Users set</value>
    public DbSet<User>? Users { get; set; }


    /// <summary>
    /// Data Context constructor
    /// </summary>
    /// <param name="configuration">Injected configuration</param>
    public DataContext(IConfiguration configuration)
  {
    Configuration = configuration;
  }

  /// <summary>
  /// Connection to database
  /// </summary>
  /// <param name="options">Injected options</param>
  protected override void OnConfiguring(DbContextOptionsBuilder options)
  {
    // connect to sql server with connection string from app settings
    options.UseSqlServer(Configuration.GetConnectionString("Database"));
  }
}