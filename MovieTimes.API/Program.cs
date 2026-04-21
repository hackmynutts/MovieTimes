using Microsoft.EntityFrameworkCore;
using MovieTimes.API.Data;
using MovieTimes.API.Repository;
using MovieTimes.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar la cadena de conexión para SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar servicios de la capa de negocio
builder.Services.AddScoped<IMovieServices, MovieServices>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Registrar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowUI", policy =>
    {
        policy.WithOrigins(
            "https://localhost:7038",
            "http://localhost:5280"
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); ? COMENTADO, causaba el conflicto
app.UseCors("AllowUI"); // ? CORS primero, siempre
app.UseAuthorization();
app.MapControllers();
app.Run();