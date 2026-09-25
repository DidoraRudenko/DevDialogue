namespace DevDialogue.Models;

public class Question
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public string ExpectedAnswer { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty; 
    public string Level { get; set; } = string.Empty; 
    public string Language { get; set; } = string.Empty; 
}