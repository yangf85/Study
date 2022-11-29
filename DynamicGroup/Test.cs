using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static DynamicGroup.Program;

namespace DynamicGroup
{
    [BenchmarkDotNet.Attributes.MemoryDiagnoser]
    public class Test
    {
        private CommonComparer<Student> _comp;
        private Func<Student, dynamic> _func;
        private Student[] _students;

        public Test()
        {
            //var students = new List<Student>()
            //{
            //    new Student(0,"王重阳","男",71),
            //    new Student(1,"黄药师","男",68),
            //    new Student(2,"欧阳锋","男",68),
            //    new Student(3,"一灯","男",68),
            //    new Student(4,"洪七公","男",68),
            //    new Student(5,"郭靖","男",38),
            //    new Student(6,"黄蓉","女",38),
            //    new Student(7,"杨康","男",38),
            //    new Student(8,"穆念慈","女",38),
            //    new Student(9,"欧阳克","男",48),
            //    new Student(10,"杨过","男",18),
            //    new Student(11,"小龙女","女",18),
            //    new Student(12,"李莫愁","女",38),
            //    new Student(13,"林朝英","女",68),
            //    new Student(14,"金轮法王","男",68),
            //};

            _students = Create();
            _comp = new CommonComparer<Student>(new string[2] { "Gender", "Age" });
            _func = ExpressionGroup.Builder<Student>(new string[2] { "Gender", "Age" }).Compile();
        }

        public Student[] Create()
        {
            var names = new string[] { "王重阳", "黄药师", "洪七公", "穆念慈", "黄蓉", "欧阳克", "李莫愁" };
            var genders = new string[] { "男", "女" };
            var ages = new int[3] { 18, 38, 68 };

            var list = new List<Student>();

            var random = new Random();
            for (int i = 0; i < 10000; i++)
            {
                var name = names[random.Next(0, 6)];
                var gender = genders[random.Next(0, 1)];
                var age = ages[random.Next(0, 2)];
                var stu = new Student(i, name, gender, age);
                list.Add(stu);
            }
            return list.ToArray();
        }

        [Benchmark]
        public void Group1()
        {
            var groups = _students.GroupBy(s => new { s.Gender, s.Age });
        }

        [Benchmark]
        public void Group2()
        {
            var groups = _students.GroupBy(s => s, _comp);
        }

        [Benchmark]
        public void Group3()
        {
            var groups = _students.GroupBy(_func);
        }
    }
}