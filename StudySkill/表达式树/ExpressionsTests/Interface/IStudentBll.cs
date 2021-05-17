using System.Collections.Generic;

namespace ExpressionsTests
{
    public interface IStudentBll
    {
        IEnumerable<Student> GetStudents();
    }
}