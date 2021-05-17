using NUnit.Framework;
using System;

namespace ExpressionsTests
{
    public class FactoryTest
    {
        [Test]
        public void Run()
        {
            Console.WriteLine($"开始运行{nameof(FactoryTest)}");

            var builder = new ObjectContainerBuilder();
            builder.Register<StudentBll, IStudentBll>();
            builder.Register<StudentDal1, IStudentDal>();

            ICustomContainer container = builder.Build();
            // 使用 StudentDal1
            //IStudentBll studentBll = new StudentBll(new StudentDal1());
            IStudentBll studentBll = container.Resolve<IStudentBll>();
            var students = studentBll.GetStudents();
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
            // 使用 StudentDal2
            studentBll = new StudentBll(new StudentDal2());
            students = studentBll.GetStudents();
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
            Console.WriteLine($"结束运行{nameof(FactoryTest)}");
        }


    }
}
