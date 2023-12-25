using Microsoft.EntityFrameworkCore;
using NetMantab.Data;
using NetMantab.Models;
using NetMantab.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext") ?? throw new InvalidOperationException("Connection string 'AppDbContext' not found.")));

builder.Services.AddAutoMapper(typeof(UserMapperProfile), typeof(TodoMapperProfile));

// Dependencies
builder.Services.AddScoped<AppADOConnection>((d) => new AppADOConnection(builder.Configuration.GetConnectionString("AppDbContext")));
builder.Services.AddScoped<TodoService>();
builder.Services.AddScoped<UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
