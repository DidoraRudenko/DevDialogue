using Microsoft.EntityFrameworkCore;
using DevDialogue.Models;

namespace DevDialogue.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Question> Questions { get; set; }
}