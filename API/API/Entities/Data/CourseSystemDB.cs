
using Microsoft.EntityFrameworkCore;

namespace API.Entities;

    public class CourseSystemDB : DbContext
    {
        public CourseSystemDB(DbContextOptions<CourseSystemDB> options) : base(options) 
        {
        }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Teacher> Teachers { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<CourseGroup> CourseGroup { get; set; } = null!;
        public DbSet<Class> Classes { get; set; } = null!;
        public DbSet<Subject> Subjects { get; set; } = null!;
        public DbSet<Faculty> Faculties { get; set; } = null!;
        public DbSet<StudentClass> StudentClasses { get; set; } = null!;
        public DbSet<GradeType> GradeTypes {  get; set; } = null!;
        public DbSet<SubjectClass> SubjectClasses { get; set; } = null!;
        public DbSet<SubjectGradeType> SubjectGradeTypes { get; set; } = null!;
        public DbSet<TeachSchedule> TeachSchedules { get; set; } = null!;
        public DbSet<SchoolHolidaySchedule> SchoolHolidaySchedules { get; set; } = null!;
        public DbSet<GradeColumn> GradeColumns { get; set; } = null!;
        public DbSet<TuitionFeePayment> TuitionFeePayments { get; set; } = null!;
        public DbSet<TuitionType> TuitionTypes { get; set; } = null!;
        public DbSet<Grade> Grades { get; set; } = null!;
        public DbSet<EmployeeSalary> EmployeeSalaries { get; set; } = null!;
        public DbSet<Permission> Permissions { get; set; } = null!;
        public DbSet<RolePermission> RolePermissions { get; set; } = null!;
     protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Teacher>().ToTable("Teachers");
    modelBuilder.Entity<Student>().ToTable("Students");
    modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
    modelBuilder.Entity<Teacher>().HasIndex(t => t.PhoneNumber).IsUnique();
    modelBuilder.Entity<Student>().HasIndex(s => s.PhoneNumber).IsUnique();
    modelBuilder.Entity<Teacher>().HasIndex(t => t.IdentityCard).IsUnique();
    // Mỗi SubjectClass sẽ có một Subject (One-to-Many), nhưng Subject không được xóa khi SubjectClass bị xóa
    modelBuilder.Entity<SubjectClass>()
                .HasOne(sc => sc.Subject) 
                .WithMany() 
                .HasForeignKey(sc => sc.SubjectId)                    
                .OnDelete(DeleteBehavior.NoAction);
    // Mỗi TeachSchedule sẽ có một Subject (One-to-Many)
    modelBuilder.Entity<TeachSchedule>()
                .HasOne(ts => ts.Subject) 
                .WithMany()  
                .HasForeignKey(ts => ts.SubjectId);
    // Mỗi TeachSchedule sẽ liên kết với Subject qua TeachSchedules (One-to-Many)
    // Subject không được xóa nếu TeachSchedule bị xóa
    modelBuilder.Entity<TeachSchedule>()
                .HasOne(ts => ts.Subject)               
                .WithMany(s => s.TeachSchedules)        
                .HasForeignKey(ts => ts.SubjectId)             
                .OnDelete(DeleteBehavior.NoAction);
    // Mỗi TeachSchedule liên kết với một Teacher (One-to-Many)
    // Teacher không được xóa nếu TeachSchedule bị xóa
    modelBuilder.Entity<TeachSchedule>()
                .HasOne(ts => ts.Teacher)
                .WithMany()
                .HasForeignKey(ts => ts.UserId)
                .OnDelete(DeleteBehavior.NoAction);
    // Mỗi SubjectClass sẽ có một Subject (One-to-Many)
    // Subject không được xóa nếu SubjectClass bị xóa
    modelBuilder.Entity<SubjectClass>()
                .HasOne(ts => ts.Subject)               
                .WithMany(s => s.SubjectClasses)        
                .HasForeignKey(ts => ts.SubjectId)
                .OnDelete(DeleteBehavior.NoAction);
}

    }
