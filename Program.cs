using Microsoft.EntityFrameworkCore;
using ReadMovie.Data;
using ReadMovie.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var pg = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ReadMovieDb>(o => o.UseNpgsql(pg));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UsarEndpoints();

app.Run();
