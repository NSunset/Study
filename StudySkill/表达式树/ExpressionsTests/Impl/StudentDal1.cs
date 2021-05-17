using System.Collections.Generic;

namespace ExpressionsTests
{
    public class StudentDal1 : IStudentDal
    {
        private readonly IList<Student> _studentList = new List<Student>
            {
                new Student
                {
                    Id = "12",
                    Name = "Traceless",
                }
            };

        public IEnumerable<Student> GetStudents()
        {
            return _studentList;
        }
    }
}
