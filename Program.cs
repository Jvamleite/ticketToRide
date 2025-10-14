using TicketToRide.Domain.Interfaces;
using TicketToRide.Application.Repositories;
using TicketToRide.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://127.0.0.1:3000", "http://localhost:5257", "http://127.0.0.1:5257")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add dependency injection
builder.Services.AddScoped<IPartidaRepository, PartidaRepositoryMemory>();
builder.Services.AddScoped<PartidaService>();
builder.Services.AddScoped<JogadorService>();
builder.Services.AddScoped<TurnoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection(); // Disabled for local development

// Use CORS
app.UseCors("AllowFrontend");

// Map controllers
app.MapControllers();

// Serve static files (frontend)
app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();
