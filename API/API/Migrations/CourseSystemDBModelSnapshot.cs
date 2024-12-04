﻿// <auto-generated />
using System;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(CourseSystemDB))]
    partial class CourseSystemDBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("API.Entities.Class", b =>
                {
                    b.Property<string>("ClassId")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Avatar")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CourseGroupId")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FacultyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("MaxNumberStudent")
                        .HasColumnType("int");

                    b.Property<int>("NumberStudent")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<double>("Tuition")
                        .HasColumnType("float");

                    b.HasKey("ClassId");

                    b.HasIndex("CourseGroupId");

                    b.HasIndex("FacultyId");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("API.Entities.CourseGroup", b =>
                {
                    b.Property<string>("CourseGroupId")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("CourseGroupName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("EndStudy")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartStudy")
                        .HasColumnType("datetime2");

                    b.HasKey("CourseGroupId");

                    b.ToTable("CourseGroup");
                });

            modelBuilder.Entity("API.Entities.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentId"));

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("API.Entities.EmployeeSalary", b =>
                {
                    b.Property<int>("EmployeeSalaryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeSalaryId"));

                    b.Property<double?>("Allowance")
                        .HasColumnType("float");

                    b.Property<string>("AllowanceName")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsClose")
                        .HasColumnType("bit");

                    b.Property<string>("Note")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<double>("Salary")
                        .HasColumnType("float");

                    b.Property<double>("SalaryReal")
                        .HasColumnType("float");

                    b.Property<string>("TeacherUserId")
                        .HasColumnType("nvarchar(13)");

                    b.Property<double>("TotalSalary")
                        .HasColumnType("float");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeSalaryId");

                    b.HasIndex("TeacherUserId");

                    b.ToTable("EmployeeSalaries");
                });

            modelBuilder.Entity("API.Entities.Faculty", b =>
                {
                    b.Property<string>("FacultyId")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FacultyName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("FacultyId");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("API.Entities.Grade", b =>
                {
                    b.Property<int>("GradeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GradeId"));

                    b.Property<int>("GradeTypeId")
                        .HasColumnType("int");

                    b.Property<string>("SubjectId")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(13)");

                    b.HasKey("GradeId");

                    b.HasIndex("GradeTypeId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("API.Entities.GradeColumn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GradeId")
                        .HasColumnType("int");

                    b.Property<double>("Score")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("GradeId");

                    b.ToTable("GradeColumns");
                });

            modelBuilder.Entity("API.Entities.GradeType", b =>
                {
                    b.Property<int>("GradeTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GradeTypeId"));

                    b.Property<int>("Coefficient")
                        .HasColumnType("int");

                    b.Property<string>("GradeTypeName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("GradeTypeId");

                    b.ToTable("GradeTypes");
                });

            modelBuilder.Entity("API.Entities.Permission", b =>
                {
                    b.Property<int>("PermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PermissionId"));

                    b.Property<string>("PermissionName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("PermissionId");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("API.Entities.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("API.Entities.RolePermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("API.Entities.SchoolHolidaySchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NameHoliday")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("SchoolHolidaySchedules");
                });

            modelBuilder.Entity("API.Entities.StudentClass", b =>
                {
                    b.Property<int>("StudentClassId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentClassId"));

                    b.Property<string>("ClassId")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(13)");

                    b.HasKey("StudentClassId");

                    b.HasIndex("ClassId");

                    b.HasIndex("UserId");

                    b.ToTable("StudentClasses");
                });

            modelBuilder.Entity("API.Entities.Subject", b =>
                {
                    b.Property<string>("SubjectId")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("FacultyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("SubjectId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("FacultyId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("API.Entities.SubjectClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClassId")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<bool>("IsClose")
                        .HasColumnType("bit");

                    b.Property<string>("SubjectId")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("SubjectId");

                    b.ToTable("SubjectClasses");
                });

            modelBuilder.Entity("API.Entities.SubjectGradeType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CourseGroupId")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("GradeColumn")
                        .HasColumnType("int");

                    b.Property<int>("GradeTypeId")
                        .HasColumnType("int");

                    b.Property<int>("MandatoryColumnGrade")
                        .HasColumnType("int");

                    b.Property<string>("SubjectId")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("CourseGroupId");

                    b.HasIndex("GradeTypeId");

                    b.HasIndex("SubjectId");

                    b.ToTable("SubjectGradeTypes");
                });

            modelBuilder.Entity("API.Entities.TeachSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClassId")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ClassRoom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("StudyDay")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("StudyTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("StudyTimeEnd")
                        .HasColumnType("time");

                    b.Property<string>("SubjectId")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("TeacherUserId")
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(13)");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TeacherUserId");

                    b.HasIndex("UserId");

                    b.ToTable("TeachSchedules");
                });

            modelBuilder.Entity("API.Entities.TuitionFeePayment", b =>
                {
                    b.Property<int>("TuitionFeePaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TuitionFeePaymentId"));

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<double>("Discount")
                        .HasColumnType("float");

                    b.Property<double>("EffectiveChargeRate")
                        .HasColumnType("float");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentClassId")
                        .HasColumnType("int");

                    b.Property<double>("Surcharge")
                        .HasColumnType("float");

                    b.Property<double>("Tuition")
                        .HasColumnType("float");

                    b.Property<int>("TuitionTypeId")
                        .HasColumnType("int");

                    b.HasKey("TuitionFeePaymentId");

                    b.HasIndex("StudentClassId");

                    b.HasIndex("TuitionTypeId");

                    b.ToTable("TuitionFeePayments");
                });

            modelBuilder.Entity("API.Entities.TuitionType", b =>
                {
                    b.Property<int>("TuitionTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TuitionTypeId"));

                    b.Property<string>("TuitionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TuitionTypeId");

                    b.ToTable("TuitionTypes");
                });

            modelBuilder.Entity("API.Entities.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Avatar")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("VerificationCode")
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("API.Entities.Student", b =>
                {
                    b.HasBaseType("API.Entities.User");

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("Parents")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.HasIndex("PhoneNumber")
                        .IsUnique()
                        .HasFilter("[PhoneNumber] IS NOT NULL");

                    b.ToTable("Students", (string)null);
                });

            modelBuilder.Entity("API.Entities.Teacher", b =>
                {
                    b.HasBaseType("API.Entities.User");

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdentityCard")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("PartTimeSubject")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("SubjectId")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("TaxCode")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasIndex("IdentityCard")
                        .IsUnique()
                        .HasFilter("[IdentityCard] IS NOT NULL");

                    b.HasIndex("PhoneNumber")
                        .IsUnique()
                        .HasFilter("[PhoneNumber] IS NOT NULL");

                    b.HasIndex("SubjectId");

                    b.ToTable("Teachers", (string)null);
                });

            modelBuilder.Entity("API.Entities.Class", b =>
                {
                    b.HasOne("API.Entities.CourseGroup", "CourseGroup")
                        .WithMany()
                        .HasForeignKey("CourseGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Faculty", "Faculty")
                        .WithMany("Class")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseGroup");

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("API.Entities.EmployeeSalary", b =>
                {
                    b.HasOne("API.Entities.Teacher", "Teacher")
                        .WithMany("EmployeeSalarys")
                        .HasForeignKey("TeacherUserId");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("API.Entities.Grade", b =>
                {
                    b.HasOne("API.Entities.GradeType", "GradeType")
                        .WithMany("Grade")
                        .HasForeignKey("GradeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GradeType");

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("API.Entities.GradeColumn", b =>
                {
                    b.HasOne("API.Entities.Grade", "Grade")
                        .WithMany("GradeColumn")
                        .HasForeignKey("GradeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Grade");
                });

            modelBuilder.Entity("API.Entities.RolePermission", b =>
                {
                    b.HasOne("API.Entities.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("API.Entities.StudentClass", b =>
                {
                    b.HasOne("API.Entities.Class", "Class")
                        .WithMany("StudentClasses")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Student", "Student")
                        .WithMany("StudentClasses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("API.Entities.Subject", b =>
                {
                    b.HasOne("API.Entities.Department", "Department")
                        .WithMany("Subjects")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Faculty", "Faculty")
                        .WithMany("Subjects")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("API.Entities.SubjectClass", b =>
                {
                    b.HasOne("API.Entities.Class", "Class")
                        .WithMany("SubjectClasses")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Department", "Department")
                        .WithMany("SubjectClasses")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Subject", "Subject")
                        .WithMany("SubjectClasses")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Department");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("API.Entities.SubjectGradeType", b =>
                {
                    b.HasOne("API.Entities.CourseGroup", "CourseGroup")
                        .WithMany("SubjectGradeTypes")
                        .HasForeignKey("CourseGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.GradeType", "GradeType")
                        .WithMany("SubjectGradeType")
                        .HasForeignKey("GradeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Subject", "Subject")
                        .WithMany("SubjectGradeTypes")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseGroup");

                    b.Navigation("GradeType");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("API.Entities.TeachSchedule", b =>
                {
                    b.HasOne("API.Entities.Class", "Class")
                        .WithMany("TeachSchedules")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Subject", "Subject")
                        .WithMany("TeachSchedules")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("API.Entities.Teacher", null)
                        .WithMany("TeachSchedules")
                        .HasForeignKey("TeacherUserId");

                    b.HasOne("API.Entities.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Subject");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("API.Entities.TuitionFeePayment", b =>
                {
                    b.HasOne("API.Entities.StudentClass", "StudentClass")
                        .WithMany("TuitionFeePayments")
                        .HasForeignKey("StudentClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.TuitionType", "TuitionType")
                        .WithMany("TuitionFeePayments")
                        .HasForeignKey("TuitionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StudentClass");

                    b.Navigation("TuitionType");
                });

            modelBuilder.Entity("API.Entities.User", b =>
                {
                    b.HasOne("API.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("API.Entities.Student", b =>
                {
                    b.HasOne("API.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("API.Entities.Student", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("API.Entities.Teacher", b =>
                {
                    b.HasOne("API.Entities.Subject", "Subject")
                        .WithMany("Teachers")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("API.Entities.Teacher", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("API.Entities.Class", b =>
                {
                    b.Navigation("StudentClasses");

                    b.Navigation("SubjectClasses");

                    b.Navigation("TeachSchedules");
                });

            modelBuilder.Entity("API.Entities.CourseGroup", b =>
                {
                    b.Navigation("SubjectGradeTypes");
                });

            modelBuilder.Entity("API.Entities.Department", b =>
                {
                    b.Navigation("SubjectClasses");

                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("API.Entities.Faculty", b =>
                {
                    b.Navigation("Class");

                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("API.Entities.Grade", b =>
                {
                    b.Navigation("GradeColumn");
                });

            modelBuilder.Entity("API.Entities.GradeType", b =>
                {
                    b.Navigation("Grade");

                    b.Navigation("SubjectGradeType");
                });

            modelBuilder.Entity("API.Entities.Permission", b =>
                {
                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("API.Entities.Role", b =>
                {
                    b.Navigation("RolePermissions");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("API.Entities.StudentClass", b =>
                {
                    b.Navigation("TuitionFeePayments");
                });

            modelBuilder.Entity("API.Entities.Subject", b =>
                {
                    b.Navigation("SubjectClasses");

                    b.Navigation("SubjectGradeTypes");

                    b.Navigation("TeachSchedules");

                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("API.Entities.TuitionType", b =>
                {
                    b.Navigation("TuitionFeePayments");
                });

            modelBuilder.Entity("API.Entities.Student", b =>
                {
                    b.Navigation("StudentClasses");
                });

            modelBuilder.Entity("API.Entities.Teacher", b =>
                {
                    b.Navigation("EmployeeSalarys");

                    b.Navigation("TeachSchedules");
                });
#pragma warning restore 612, 618
        }
    }
}