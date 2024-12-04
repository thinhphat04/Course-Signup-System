using System.Text;
using API.Entities;

using API.Middlewares;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();



builder.Services.AddLogging();



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Thiết lập các tùy chọn xác thực JWT
            ValidateIssuer = true, // Kiểm tra Issuer (người phát hành token)
            ValidateAudience = true, // Kiểm tra Audience (đối tượng nhận token)
            ValidateLifetime = true, // Kiểm tra thời hạn token
            ValidateIssuerSigningKey = true, // Kiểm tra khóa ký token
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // Issuer hợp lệ, lấy từ cấu hình
            ValidAudience = builder.Configuration["Jwt:Audience"], // Audience hợp lệ, lấy từ cấu hình
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // Khóa ký token, lấy từ cấu hình
        };
    });

builder.Services.AddDbContext<CourseSystemDB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"))); // Lấy chuỗi kết nối từ appsettings.json

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();
app.UseAuthentication();


var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.UseRouting();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}