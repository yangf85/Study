using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DynamicGroup
{
    public class Student// : IEquatable<Student>
    {
        public int Age { get; set; }

        public string Gender { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public Student(int id, string name, string gender, int age)
        {
            Id = id;
            Name = name;
            Gender = gender;
            Age = age;
        }

        //public bool Equals(Student other)
        //{
        //    if (other == null)
        //    {
        //        return false;
        //    }

        //    if (object.ReferenceEquals(this, other))
        //    {
        //        return true;
        //    }

        //    return (Gender, Age) == (other.Gender, other.Age);
        //}

        //public override bool Equals(object obj)
        //{
        //    if (obj is Student stu)
        //    {
        //        return Equals(stu);
        //    }
        //    return false;
        //}

        //public override int GetHashCode()
        //{
        //    return (Gender, Age).GetHashCode();
        //}

        public override string ToString()
        {
            return $"Id:{Id} Gender:{Gender} Age:{Age} Name:{Name}";
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
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
            //    new Student(12,"金轮法王","女",38),
            //    new Student(13,"金轮法王","女",68),
            //    new Student(14,"金轮法王","男",68),
            //};

            //var comp = new CommonComparer<Student>(new string[1] { "Name" });
            //var groups = students.GroupBy(s => s, comp);

            //foreach (var group in groups)
            //{
            //    Console.WriteLine($"分组依据：{group.Key.Gender}{group.Key.Age}==========");
            //    foreach (var s in group)
            //    {
            //        Console.WriteLine(s.ToString());
            //    }
            //}

            //var numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 1, 2, 4 };
            //var groups2 = numbers.GroupBy(n => n);

            //foreach (var group in groups2)
            //{
            //    Console.WriteLine($"分组依据 ==========");
            //    foreach (var s in group)
            //    {
            //        Console.WriteLine(s);
            //    }
            //}

            BenchmarkRunner.Run<Test>();
            Console.ReadKey();
        }

        public class CommonComparer<T> : IEqualityComparer<T>
        {
            private PropertyInfo[] _propertyInfos;

            public CommonComparer(IEnumerable<string> propertyNames)
            {
                var properties = typeof(T).GetProperties();
                var list = new List<PropertyInfo>();
                foreach (var p in properties)
                {
                    if (propertyNames.Contains(p.Name))
                    {
                        list.Add(p);
                    }
                }
                _propertyInfos = list.ToArray();
            }

            public bool Equals(T x, T y)
            {
                foreach (var p in _propertyInfos)
                {
                    dynamic value1 = x.GetType().GetProperty(p.Name).GetValue(x);
                    dynamic value2 = x.GetType().GetProperty(p.Name).GetValue(y);
                    if (value1 != value2)
                    {
                        return false;
                    }
                }
                return true;
            }

            public int GetHashCode(T obj)
            {
                return 0;
            }
        }

        public class StudentComparer : IEqualityComparer<Student>
        {
            public bool Equals(Student x, Student y)
            {
                return (x.Gender, x.Age) == (y.Gender, y.Age);
            }

            public int GetHashCode(Student obj)
            {
                return 0;
            }
        }
    }
}