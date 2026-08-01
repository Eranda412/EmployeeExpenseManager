using BaseAPI.FirstDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

// 1. Define a unique string for your policy name
//var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<FirstDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();


// 2. Enable the middleware in the exact correct order
app.UseRouting();

app.UseCors("AllowAll"); // Must be after UseRouting() but before UseAuthorization()




var connection = String.Empty;
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();

    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
    connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");



    app.UseSwagger();

    app.UseSwaggerUI();
}

app.MapGet("/users", async (FirstDbContext db) =>
{
    return await db.Users.ToListAsync();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


