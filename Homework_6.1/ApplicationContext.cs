using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_6._1
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Course> Courses { get; set; } = null;
        public DbSet<Student> Students { get; set; } = null;
        public DbSet<Enrollment> Enrollments { get; set; } = null;
        public DbSet<Instructor> Instructors { get; set; } = null;

        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>().
                HasOne(e => e.Student)
                .WithMany(e => e.Enrollments)
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(e => e.Enrollments)
                .HasForeignKey(e => e.CourseId);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.Instructors)
                .WithMany(e => e.Courses)
                .UsingEntity(j => j.ToTable("CourseInstructor"));

            Seed(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, FirstName = "Alice", LastName = "Johnson", DateOfBirth = new DateTime(1998, 5, 15) },
                new Student { Id = 2, FirstName = "Bob", LastName = "Smith", DateOfBirth = new DateTime(1997, 8, 22) },
                new Student { Id = 3, FirstName = "Charlie", LastName = "Brown", DateOfBirth = new DateTime(1999, 3, 18) },
                new Student { Id = 4, FirstName = "David", LastName = "Wilson", DateOfBirth = new DateTime(2000, 2, 5) },
                new Student { Id = 5, FirstName = "Emma", LastName = "Miller", DateOfBirth = new DateTime(1996, 11, 18) },
                new Student { Id = 6, FirstName = "Max", LastName = "Kelen", DateOfBirth = new DateTime(1999, 5, 22) },
                new Student { Id = 7, FirstName = "Bob", LastName = "Daves", DateOfBirth = new DateTime(2000, 2, 24) },
                new Student { Id = 8, FirstName = "Kate", LastName = "Sizi", DateOfBirth = new DateTime(1998, 4, 13) }
                );

            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Title = "Mathematics", Description = "Introduction to Mathematics" },
                new Course { Id = 2, Title = "History", Description = "World History" },
                new Course { Id = 3, Title = "Computer Science", Description = "Fundamentals of Computer Science" },
                new Course { Id = 4, Title = "English Literature", Description = "Literature and Composition" },
                new Course { Id = 5, Title = "Biology", Description = "Introduction to Biology" }
                );
            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment { Id = 1, StudentId = 1, CourseId = 1, EnrollmentDate = DateTime.Now.AddMonths(-1) },
                new Enrollment { Id = 2, StudentId = 2, CourseId = 2, EnrollmentDate = DateTime.Now.AddDays(-200) },
                new Enrollment { Id = 3, StudentId = 3, CourseId = 3, EnrollmentDate = DateTime.Now.AddDays(-125) },
                new Enrollment { Id = 4, StudentId = 4, CourseId = 4, EnrollmentDate = DateTime.Now.AddDays(-48) },
                new Enrollment { Id = 5, StudentId = 5, CourseId = 5, EnrollmentDate = DateTime.Now.AddYears(-2) },
                new Enrollment { Id = 6, StudentId = 6, CourseId = 5, EnrollmentDate = DateTime.Now.AddYears(-1) },
                new Enrollment { Id = 7, StudentId = 6, CourseId = 5, EnrollmentDate = DateTime.Now.AddDays(-38) },
                new Enrollment { Id = 8, StudentId = 7, CourseId = 1, EnrollmentDate = DateTime.Now.AddDays(-79) },
                new Enrollment { Id = 10, StudentId = 2, CourseId = 5, EnrollmentDate = DateTime.Now.AddDays(-83) },
                new Enrollment { Id = 11, StudentId = 2, CourseId = 4, EnrollmentDate = DateTime.Now.AddDays(-298) }
                );

            modelBuilder.Entity<Instructor>().HasData(
                new Instructor { Id = 1, FirstName = "John", LastName = "Doe" },
                new Instructor { Id = 2, FirstName = "Jane", LastName = "Smith" },
                new Instructor { Id = 3, FirstName = "Michael", LastName = "Johnson" }
                );
        }
    }
}
