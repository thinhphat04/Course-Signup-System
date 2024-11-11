using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class CourseSystemContext : DbContext
    {
        public CourseSystemContext(DbContextOptions<CourseSystemContext> options) : base(options)
        {
        }

        // Định nghĩa các bảng (DbSet)
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<GradeType> GradeTypes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<TeacherAssignment> TeacherAssignments { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<FinanceReport> FinanceReports { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // User - Student (1:1)
    modelBuilder.Entity<Student>()
        .HasOne(s => s.User)
        .WithOne(u => u.Student)
        .HasForeignKey<Student>(s => s.UserId);

    // User - Teacher (1:1)
    modelBuilder.Entity<Teacher>()
        .HasOne(t => t.User)
        .WithOne(u => u.Teacher)
        .HasForeignKey<Teacher>(t => t.UserId);

    // Course - Class (1:N)
    modelBuilder.Entity<Class>()
        .HasOne(c => c.Course)
        .WithMany(c => c.Classes)
        .HasForeignKey(c => c.CourseId);

    // Teacher - Class (1:N)
    modelBuilder.Entity<Class>()
        .HasOne(c => c.Teacher)
        .WithMany(t => t.Classes)
        .HasForeignKey(c => c.TeacherId)
        .OnDelete(DeleteBehavior.Restrict); // Thêm để tránh cascade path

    // Student - Enrollment (1:N)
    modelBuilder.Entity<Enrollment>()
        .HasOne(e => e.Student)
        .WithMany(s => s.Enrollments)
        .HasForeignKey(e => e.StudentId)
        .OnDelete(DeleteBehavior.Restrict); // Thêm để tránh cascade path

    // Class - Enrollment (1:N)
    modelBuilder.Entity<Enrollment>()
        .HasOne(e => e.Class)
        .WithMany(c => c.Enrollments)
        .HasForeignKey(e => e.ClassId)
        .OnDelete(DeleteBehavior.Restrict); // Thêm để tránh cascade path

    // Enrollment - Attendance (1:N)
    modelBuilder.Entity<Attendance>()
        .HasOne(a => a.Enrollment)
        .WithMany(e => e.Attendances)
        .HasForeignKey(a => a.EnrollmentId);

    // Enrollment - Grade (1:N)
    modelBuilder.Entity<Grade>()
        .HasOne(g => g.Enrollment)
        .WithMany(e => e.Grades)
        .HasForeignKey(g => g.EnrollmentId);

    // GradeType - Grade (1:N)
    modelBuilder.Entity<Grade>()
        .HasOne(g => g.GradeType)
        .WithMany(gt => gt.Grades)
        .HasForeignKey(g => g.GradeTypeId);

    // Department - Subject (1:N)
    modelBuilder.Entity<Subject>()
        .HasOne(s => s.Department)
        .WithMany(d => d.Subjects)
        .HasForeignKey(s => s.DepartmentId);

    // Teacher - TeacherAssignment (1:N)
    modelBuilder.Entity<TeacherAssignment>()
        .HasOne(ta => ta.Teacher)
        .WithMany(t => t.TeacherAssignments)
        .HasForeignKey(ta => ta.TeacherId);

    // Class - TeacherAssignment (1:N)
    modelBuilder.Entity<TeacherAssignment>()
        .HasOne(ta => ta.Class)
        .WithMany(c => c.TeacherAssignments)
        .HasForeignKey(ta => ta.ClassId);

    // Teacher - Salary (1:N)
    modelBuilder.Entity<Salary>()
        .HasOne(s => s.Teacher)
        .WithMany(t => t.Salaries)
        .HasForeignKey(s => s.TeacherId);

    // Thiết lập khóa chính cho TeacherAssignment nếu dùng composite key
    modelBuilder.Entity<TeacherAssignment>()
        .HasKey(ta => new { ta.TeacherId, ta.ClassId });

    // Đảm bảo FinanceReport có khóa chính hoặc không có khóa (tùy chọn bên dưới)

    // Tùy chọn 1: Nếu FinanceReport cần khóa chính
    modelBuilder.Entity<FinanceReport>()
        .HasKey(f => f.ReportId);

    // Tùy chọn 2: Nếu FinanceReport là bảng không có khóa chính (keyless)
    // modelBuilder.Entity<FinanceReport>().HasNoKey();
    
    // Thiết lập độ chính xác và tỷ lệ cho Fee trong Course
    modelBuilder.Entity<Course>()
        .Property(c => c.Fee)
        .HasPrecision(18, 2); // 18 là tổng số chữ số, 2 là số chữ số sau dấu thập phân

    // Thiết lập độ chính xác và tỷ lệ cho Amount trong Salary
    modelBuilder.Entity<Salary>()
        .Property(s => s.Amount)
        .HasPrecision(18, 2); // 18 là tổng số chữ số, 2 là số chữ số sau dấu thập phân
}

    }
}
