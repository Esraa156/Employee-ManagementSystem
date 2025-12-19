using Application.Services;
using ApplicationLayer;
using ApplicationLayer.Interfaces;
using ApplicationLayer.Mapping;
using ApplicationLayer.Services;
using DomainLayer.Entities;
using Infrastructure.Data;
using InfrastructureLayer.Data;
using InfrastructureLayer.JWT;
using InfrastructureLayer.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddDbContext<AppDbContext>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddScoped<IJobTitleRepository, JobTitleRepository>();

// Services
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<JobTitleService>();
builder.Services.AddScoped<EmployeeService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});
// Example of overriding the default configuration
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // If this line exists, it means JSON property names must match C# property names (PascalCase) exactly.
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        // Configuration to allow camelCase C# properties to bind from camelCase JSON
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });
builder.Services.AddAutoMapper(typeof(EmployeeProfile));

var app = builder.Build();
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
