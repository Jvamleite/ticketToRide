using TicketToRide.API.Filters;
using TicketToRide.Application.Services;
using TicketToRide.Application.Services.Interfaces;
using TicketToRide.Configuration;
using TicketToRide.Domain.Interfaces;
using TicketToRide.Infracstruture.Repositories;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<DomainExceptionFilter>();
});
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
builder.Services.AddSingleton<IPartidaRepository, PartidaRepositoryMemory>();
builder.Services.AddEventHandling();
builder.Services.AddMapper();
builder.Services.AddScoped<IPartidaService, PartidaService>();
builder.Services.AddScoped<IJogadorService, JogadorService>();
builder.Services.AddScoped<ITurnoService, TurnoService>();

WebApplication app = builder.Build();

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