using System.Collections.Generic;

namespace ExpressionsTests
{
    public interface IStudentDal
    {
        IEnumerable<Student> GetStudents();
    }
}
