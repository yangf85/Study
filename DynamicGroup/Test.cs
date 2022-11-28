﻿using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DynamicGroup.Program;

namespace DynamicGroup
{
    [BenchmarkDotNet.Attributes.MemoryDiagnoser]
    public class Test
    {
        private Student[] _students;

        public Test()
        {
            var students = new List<Student>()
            {
                new Student(0,"王重阳","男",71),
                new Student(1,"黄药师","男",68),
                new Student(2,"欧阳锋","男",68),
                new Student(3,"一灯","男",68),
                new Student(4,"洪七公","男",68),
                new Student(5,"郭靖","男",38),
                new Student(6,"黄蓉","女",38),
                new Student(7,"杨康","男",38),
                new Student(8,"穆念慈","女",38),
                new Student(9,"欧阳克","男",48),
                new Student(10,"杨过","男",18),
                new Student(11,"小龙女","女",18),
                new Student(12,"金轮法王","女",38),
                new Student(13,"金轮法王","女",68),
                new Student(14,"金轮法王","男",68),
            };

            _students = students.ToArray();
        }

        [Benchmark]
        public void Group1()
        {
            var groups = _students.GroupBy(s => (s.Gender, s.Age));
        }

        [Benchmark]
        public void Group2()
        {
            var comp = new CommonComparer<Student>(new string[2] { "Gender", "Age" });
            var groups = _students.GroupBy(s => s, comp);
        }
    }
}