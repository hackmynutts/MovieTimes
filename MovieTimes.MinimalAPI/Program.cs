using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

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

// CORS debe ir primero, antes de cualquier otro middleware
app.UseCors("AllowUI");

// app.UseHttpsRedirection(); ← COMENTADO, causaba el conflicto HTTP/HTTPS
app.UseAuthorization();
app.MapControllers();

// ── ENDPOINT 1: Lista de películas ──────────────────────────
app.MapGet("/api/Movies/top-rated", async ([FromServices] IHttpClientFactory httpFactory, int page = 1) =>
{
    var http = httpFactory.CreateClient();
    http.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI5NWQzNmE5YzY2MzdmODE2OWUzOWM4ODg3NGM2NmQ1MSIsIm5iZiI6MTc3NTkzNjg1Mi4xMiwic3ViIjoiNjlkYWE1NTQ2YjAxNzRkYjkyMWQ5Mjk0Iiwic2NvcGVzIjpbImFwaV9yZWFkIl0sInZlcnNpb24iOjF9.pSYjK2wx4vRet7Ex81P5UqJWD2F5zjr2gcqLK69T2Vs");

    var response = await http.GetStringAsync(
        $"https://api.themoviedb.org/3/movie/top_rated?page={page}"
    );

    using var doc = JsonDocument.Parse(response);
    var results = doc.RootElement.GetProperty("results");
    var movies = results.EnumerateArray().Select(movie => new
    {
        id = movie.GetProperty("id").GetInt32(),
        title = movie.GetProperty("title").GetString(),
        overview = movie.GetProperty("overview").GetString(),
        posterUrl = movie.GetProperty("poster_path").GetString()
    }).ToList();

    return Results.Ok(movies);
});

// ── ENDPOINT 2: Detalle de película por ID ──────────────────
app.MapGet("/api/Movies/{id}", async ([FromServices] IHttpClientFactory httpFactory, int id) =>
{
    var http = httpFactory.CreateClient();
    http.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI5NWQzNmE5YzY2MzdmODE2OWUzOWM4ODg3NGM2NmQ1MSIsIm5iZiI6MTc3NTkzNjg1Mi4xMiwic3ViIjoiNjlkYWE1NTQ2YjAxNzRkYjkyMWQ5Mjk0Iiwic2NvcGVzIjpbImFwaV9yZWFkIl0sInZlcnNpb24iOjF9.pSYjK2wx4vRet7Ex81P5UqJWD2F5zjr2gcqLK69T2Vs");

    var response = await http.GetStringAsync(
        $"https://api.themoviedb.org/3/movie/{id}"
    );

    using var doc = JsonDocument.Parse(response);
    var movie = doc.RootElement;
    var movieData = new
    {
        id = movie.GetProperty("id").GetInt32(),
        title = movie.GetProperty("title").GetString(),
        overview = movie.GetProperty("overview").GetString(),
        releaseDate = movie.GetProperty("release_date").GetString(),
        rating = movie.GetProperty("vote_average").GetDouble(),
        posterUrl = movie.GetProperty("poster_path").GetString()
    };

    return Results.Ok(movieData);
});

app.Run();