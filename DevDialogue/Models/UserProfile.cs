using DevDialogue.Models;

public class UserProfile
{
    public Guid Id { get; set; }   // = sub із Supabase JWT, задається вручну
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public enum SimulationStatus { InProgress, Completed }

public class SimulationSession
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserProfile User { get; set; } = null!;
    public string TargetRole { get; set; } = "";
    public string TargetLevel { get; set; } = "";
    public string TargetLanguage { get; set; } = "";
    public string? TargetFramework { get; set; }
    public SimulationStatus Status { get; set; } = SimulationStatus.InProgress;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<SimulationAttempt> Attempts { get; set; } = new();
}

public class SimulationAttempt
{
    public Guid Id { get; set; }
    public Guid SimulationSessionId { get; set; }
    public SimulationSession Session { get; set; } = null!;
    public int QuestionId { get; set; }
    public Question Question { get; set; } = null!;
    public string? UserAnswer { get; set; }
    public int? AiScore { get; set; }
    public string? AiFeedback { get; set; }
    public bool IsAnswered { get; set; }
    public DateTime AskedAt { get; set; } = DateTime.UtcNow;
    public DateTime? AnsweredAt { get; set; }
}