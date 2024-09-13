using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_6._1
{
    public class DatabaseService
    {
       
        DbContextOptions<ApplicationContext> options;

        public void EnsurePopulated()
        {

            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            string connectionString = config.GetConnectionString("DefaultConnection");


            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            options = optionsBuilder.UseSqlServer(connectionString).Options;

            using (ApplicationContext db = new ApplicationContext(options))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }

        }

        //1.Получить список студентов, зачисленных на определенный курс.
        public void GetStudentsEnrolledCourse(int idCourse) 
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var studentsEnrolledCourse = db.Enrollments.Where(e => e.CourseId == idCourse).Select(e => e.Student).ToList();
            }
        }

        //2.Получить список курсов, на которых учит определенный преподаватель.
        public void GetCoursesByInstructor(int idInstructor) 
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var coursesByInstructor = db.Courses.Where(c => c.Instructors.Any(i => i.Id == idInstructor)).ToList();
            }
        }

        //3.Получить список курсов, на которых учит определенный преподаватель,
        //вместе с именами студентов, зачисленных на каждый курс.
        public void GetCoursesByInstructorWithStudents(int idInstructor) 
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var coursesByInstructorWithStudents = db.Courses.Where(c => c.Instructors.Any(i => i.Id == idInstructor))
                    .Select(c => new
                    {
                        Course = c,
                        Students = c.Enrollments.Select(e => e.Student).ToList()
                    }).ToList();
            }
        }

        //4.Получить список курсов, на которые зачислено более 5 студентов.
        public void GetCoursesWhereStudentsMoreThen(int count)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var coursesWhereStudentsMoreThen = db.Courses.Where(c => c.Enrollments.Count() > count).ToList();
            }
        }

        //5.Получить список студентов, старше 25 лет.
        public void GetStudentsWhereAgeMoreThen(int age)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var studentsWhereAgeMoreThen = db.Students.Where(s => DateTime.Now.Year - s.DateOfBirth.Year > age).ToList();
            }
        }

        //6. Получить средний возраст всех студентов.
        public void GetAvgAgeAllStudents()
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var avgAgeAllStudents = db.Students.Average(s => DateTime.Now.Year - s.DateOfBirth.Year);
            }
        }

        //7. Получить самого молодого студента.
        public void GetMinAgeAllStudents()
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var minAgeAllStudents = db.Students.OrderBy(s => s.DateOfBirth).FirstOrDefault();
            }
        }

        //8. Получить количество курсов, на которых учится студент с определенным Id.
        public void GetAllCoursesWhereStudentStudiesById(int idStudent)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var allCoursesWhereStudentStudiesById = db.Enrollments.Count(s => s.StudentId == idStudent);
            }
        }

        //9. Получить список имен всех студентов.
        public void GetNameAllStudents()
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var nameAllStudents = db.Students.Select(s => s.FirstName).ToList();
            }
        }

        //10. Сгруппировать студентов по возрасту.
        public void GetStudentsGroupByAge()
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var studentsGroupByAge = db.Students.GroupBy(s => DateTime.Now.Year - s.DateOfBirth.Year).ToList();
            }
        }

        //11. Получить список студентов, отсортированных по фамилии в алфавитном порядке.
        public void GetSortedStudentsByLastName()
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var sortedStudentsByLastName = db.Students.OrderBy(s => s.LastName).ToList();
            }
        }

        //12. Получить список студентов вместе с информацией о зачислениях на курсы.
        public void GetStudentsAndCoursesInfo()
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var studentsAndCoursesInfo = db.Students.Join(db.Enrollments,
                    student => student.Id,
                    enrollment => enrollment.StudentId,
                    (student, enrollment) => new
                    {
                        Student = student,
                        Enrollment = enrollment
                    }).ToList();
            }
        }

        //13. Получить список студентов, не зачисленных на определенный курс.
        public void GetStudentsNotEnrolledByIdCourse(int id)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var studentsNotEnrolledByIdCourse = db.Students.Where(s => !db.Enrollments.
                Any(e => e.StudentId == s.Id && e.CourseId == id)).ToList();
            }
        }

        //14. Получить список студентов, зачисленных одновременно на два определенных курса.
        public void GetStudentsEnrolledByTwoCourses(int idCourse1, int idCourse2)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var studentsEnrolledByTwoCourses = db.Students.Where(s => db.Enrollments
                .Any(e => e.StudentId == s.Id && e.CourseId == idCourse1)&&
                db.Enrollments.Any( e => e.StudentId == s.Id && e.CourseId == idCourse2)).ToList();
            }
        }

        //15. Получить количество студентов на каждом курсе.
        public void GetCountStudentsByCourses()
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var countStudentsByCourses = db.Enrollments.GroupBy(e => e.CourseId)
                    .Select(g => new
                    {
                        CourseId = g.Key,
                        StudentCount = g.Count()
                    }).ToList();
            }
        }

    }
}
