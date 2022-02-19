using Bogus;
using ConsoleApp.Domain.Entities;
using ConsoleApp.Domain.Entities.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp.DataProviders
{
    public class StudentDataProvider
    {
        public static ICollection<StudentDP> GetStudentsDP(int count = 1000)
        {
            var studentFaker = new Faker<Domain.Entities.Dapper.StudentDP>("tr")
                //.RuleFor(i => i.Id, i => studentId++)
                .RuleFor(i => i.first_name, i => i.Person.FirstName)
                .RuleFor(i => i.last_name, i => i.Person.LastName)
                .RuleFor(i => i.birth_date, i => i.Person.DateOfBirth);

            return studentFaker.Generate(count);
        }

        public static Domain.Entities.Dapper.StudentDP GetStudentDP()
        {
            return GetStudentsDP(1).First();
        }



        public static ICollection<StudentEF> GetStudentsEF(int count = 1000)
        {
            var studentFaker = new Faker<StudentEF>("tr")
                //.RuleFor(i => i.Id, i => studentId++)
                .RuleFor(i => i.FirstName, i => i.Person.FirstName)
                .RuleFor(i => i.LastName, i => i.Person.LastName)
                .RuleFor(i => i.BirthDate, i => i.Person.DateOfBirth);

            return studentFaker.Generate(count);
        }

        public static StudentEF GetStudentEF()
        {
            return GetStudentsEF(1).First();
        }
    }
}
