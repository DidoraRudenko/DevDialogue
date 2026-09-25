using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevDialogue.DTOs;
using DevDialogue.Data;

namespace DevDialogue.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SimulationsController : ControllerBase
{
    private readonly AppDbContext _context;

    // Саме так працює Dependency Injection в ASP.NET Core! 
    // Ми просимо систему надати нам доступ до бази даних при створенні контролера.
    public SimulationsController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/simulations
    [HttpPost]
    public IActionResult CreateSimulation([FromBody] SimulationRequest request)
    {
        // Таблицю для збереження сесій ми ще не створювали, 
        // тому тут поки залишаємо генерацію фейкового ID.
        var mockSessionId = Guid.NewGuid();
        
        return Created("", new 
        { 
            sessionId = mockSessionId, 
            totalQuestions = 5,
            status = "Created"
        });
    }

    // GET: api/simulations/{sessionId}/next-question
    [HttpGet("{sessionId}/next-question")]
    public async Task<IActionResult> GetNextQuestion(Guid sessionId)
    {
        // Звертаємося до реальної бази даних і беремо одне випадкове питання
        var question = await _context.Questions
            .OrderBy(q => EF.Functions.Random()) 
            .FirstOrDefaultAsync();

        if (question == null)
        {
            return NotFound(new { message = "У базі даних поки немає питань. Додайте їх через Supabase!" });
        }

        return Ok(new 
        { 
            attemptId = Guid.NewGuid(), 
            questionContent = question.Content, 
            questionNumber = 1,
            totalQuestions = 5
        });
    }
}