using Microsoft.EntityFrameworkCore;
using DevDialogue.Models;

namespace DevDialogue.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Question> Questions => Set<Question>();
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
    public DbSet<SimulationSession> SimulationSessions => Set<SimulationSession>();
    public DbSet<SimulationAttempt> SimulationAttempts => Set<SimulationAttempt>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Question>(e =>
        {
            e.ToTable("questions", t => t.ExcludeFromMigrations());
            e.Property(q => q.Id).HasColumnName("id");
            e.Property(q => q.Content).HasColumnName("question_text");
            e.Property(q => q.ExpectedAnswer).HasColumnName("answer_text");
            e.Property(q => q.Category).HasColumnName("category");
            e.Property(q => q.DifficultyLevel).HasColumnName("difficulty_level");
            e.Property(q => q.Role).HasColumnName("role");
            e.Property(q => q.Level).HasColumnName("level");
            e.Property(q => q.Language).HasColumnName("language");
            e.Property(q => q.Framework).HasColumnName("framework");
        });

        modelBuilder.Entity<UserProfile>()
            .Property(u => u.Id).ValueGeneratedNever();

        modelBuilder.Entity<SimulationSession>()
            .Property(s => s.Status).HasConversion<string>();

        modelBuilder.Entity<SimulationAttempt>()
            .HasIndex(a => new { a.SimulationSessionId, a.IsAnswered });
    }
}