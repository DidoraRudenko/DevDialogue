var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

// Вмикаємо CORS у пайплайні
app.UseCors("AllowAll");
app.MapControllers();

// 3. Тестовий ендпоінт (Health Check) для перевірки працездатності
app.MapGet("/api/health/ping", () => Results.Ok(new { 
    status = "API is running", 
    timestamp = DateTime.UtcNow 
}));

app.Run();