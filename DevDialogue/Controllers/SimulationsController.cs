using Microsoft.AspNetCore.Mvc;
using DevDialogue.DTOs;

namespace DevDialogue.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SimulationsController : ControllerBase
{
    // POST: api/simulations
    [HttpPost]
    public IActionResult CreateSimulation([FromBody] SimulationRequest request)
    {
        // Фронтенд надсилає налаштування, ми повертаємо фейковий ID сесії
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
    public IActionResult GetNextQuestion(Guid sessionId)
    {
        // Повертаємо випадкове фейкове питання
        return Ok(new 
        { 
            attemptId = Guid.NewGuid(), 
            questionContent = "Як працює механізм Dependency Injection в ASP.NET Core?", 
            questionNumber = 1,
            totalQuestions = 5
        });
    }
}