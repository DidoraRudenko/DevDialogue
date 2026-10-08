using Microsoft.EntityFrameworkCore;
using DevDialogue.Data;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// 1. Додаємо підтримку контролерів
builder.Services.AddControllers();

// 2. Налаштовуємо CORS (дозволяємо фронтенду робити запити)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("SupabaseConnection")));

var app = builder.Build();

// Вмикаємо CORS у пайплайні
app.UseCors("AllowAll");
app.MapControllers();

// 3. Тестовий ендпоінт (Health Check) для перевірки працездатності
app.MapGet("/api/health/ping", () => Results.Ok(new { 
    status = "API is running", 
    timestamp = DateTime.UtcNow 
}));

app.MapGet("/api/health/db", async (AppDbContext db) =>
{
    try
    {
        var canConnect = await db.Database.CanConnectAsync();
        return canConnect
            ? Results.Ok(new { status = "DB connected" })
            : Results.StatusCode(500);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.Run();