using System.Collections.Generic;

namespace ExpressionsTests
{
    public class StudentDal2 : IStudentDal
    {
        private readonly IList<Student> _studentList = new List<Student>
            {
                new Student
                {
                    Id = "11",
                    Name = "月落"
                }
            };

        public IEnumerable<Student> GetStudents()
        {
            return _studentList;
        }
    }
}
