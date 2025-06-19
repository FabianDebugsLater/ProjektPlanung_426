namespace Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Standort> Standorte { get; set; }
    public DbSet<Projekt> Projekte { get; set; }
}