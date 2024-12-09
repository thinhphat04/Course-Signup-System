using AutoMapper;
using API.Common.Mapping;
using API.Entities;
using API.Repositories;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCors",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyMethod() // Allow any HTTP method
                .AllowAnyHeader()
                .AllowCredentials();
        });
});


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Jwt auth header",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme bearer {token}",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
// Đăng ký dịch vụ Controller để hỗ trợ API
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

});
//Sử dụng ReferenceHandler.Preserve
// builder.Services.AddControllers().AddJsonOptions(options =>
// {
//     options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
//     options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
// });
 builder.Services.AddAutoMapper(typeof(MapperProfile));
            builder.Services.AddScoped<IUserService, UserRepository>();
            builder.Services.AddScoped<IHashPasword, HashPasswordRepository>();
            builder.Services.AddScoped<IRoleService, RoleRepository>();
            builder.Services.AddScoped<IStudentService, StudentRepository>();
            builder.Services.AddScoped<ITeacherService, TeacherRepository>();
            builder.Services.AddScoped<IGenerateService,GenerateRepository>();
            builder.Services.AddScoped<IAuthService,AuthRepository>();
            builder.Services.AddScoped<ITuitionFeePaymentService, TuitionFeePaymentRepository>(); // **Add this line**
            builder.Services.AddScoped<IClassService, ClassRepository>();
            builder.Services.AddScoped<IFacultyService, FacultyRepository>();
            builder.Services.AddScoped<IStudentClassService,StudentClassRepository>();
            builder.Services.AddScoped<ISubjectService, SubjectRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentRepository>();
            builder.Services.AddScoped<IFileStorageService, FileStorageRepository>();
            builder.Services.AddScoped<ISubjectClassService,SubjectClassRepository>();
            builder.Services.AddScoped<ITeacherScheduleService, TeacherScheduleRepository>();
            builder.Services.AddScoped<IGradeTypeService, GradeTypeRepository>();
            builder.Services.AddScoped<ITuitionTypeService, TuitionTypeRepository>();
            builder.Services.AddScoped<ICourseGroupService, CourseGroupRepository>(); // **Add this line**
            builder.Services.AddScoped<IGradeService, GradeRepository>();
            builder.Services.AddScoped<ISubjectGradeTypeService,SubjectGradeTypeRepository>();
            builder.Services.AddScoped<IGradeColumnService, GradeColumnRepository>();
            builder.Services.AddScoped<ISchoolHolidayScheduleService, SchoolHolidayScheduleRepository>();
            builder.Services.AddScoped<IEmployeeSalaryService,EmployeeSalaryRepository>();
            builder.Services.AddScoped<IPermissionService, PermissionRepository>();


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


app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseHttpsRedirection();
app.UseCors("MyCors");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

