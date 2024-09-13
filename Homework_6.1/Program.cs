using Azure;

namespace Homework_6._1
{
    public class Program
    {
        private static DatabaseService databaseService;
        static void Main()
        {
            databaseService = new DatabaseService();
            databaseService.EnsurePopulated();

            //databaseService.GetStudentsEnrolledCourse(1);//1
            //databaseService.GetCoursesByInstructor(1);//2
            //databaseService.GetCoursesByInstructorWithStudents(1);//3
            //databaseService.GetCoursesWhereStudentsMoreThen(5);//4
            //databaseService.GetStudentsWhereAgeMoreThen(25);//5
            //databaseService.GetAvgAgeAllStudents();//6
            //databaseService.GetMaxAgeAllStudents();//7
            //databaseService.GetAllCoursesWhereStudentStudiesById(3);//8
            //databaseService.GetNameAllStudents();//9
            //databaseService.GetStudentsGroupByAge();//10
            //databaseService.GetSortedStudentsByLastName();//11
            //databaseService.GetStudentsAndCoursesInfo();//12
            //databaseService.GetStudentsNotEnrolledByIdCourse(2);//13
            //databaseService.GetStudentsEnrolledByTwoCourses(1, 3);//14
            databaseService.GetCountStudentsByCourses();//15
        }
    }
}
