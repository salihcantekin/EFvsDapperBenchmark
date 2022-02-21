using Bogus;
using ConsoleApp.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp.DataProviders
{
    public class StudentDataProvider
    {
        public static ICollection<Student> GetStudentsDP(int count = 1000)
        {
            var studentFaker = new Faker<Student>("tr")
                .RuleFor(i => i.FirstName, i => i.Person.FirstName)
                .RuleFor(i => i.LastName, i => i.Person.LastName)
                .RuleFor(i => i.BirthDate, i => i.Person.DateOfBirth);

            return studentFaker.Generate(count);
        }

        public static Student GetStudentDP()
        {
            return GetStudentsDP(1).First();
        }



        public static ICollection<Student> GetStudentsEF(int count = 1000)
        {
            var studentFaker = new Faker<Student>("tr")
                .RuleFor(i => i.FirstName, i => i.Person.FirstName)
                .RuleFor(i => i.LastName, i => i.Person.LastName)
                .RuleFor(i => i.BirthDate, i => i.Person.DateOfBirth);

            var result = studentFaker.Generate(count);
            result.ForEach(i => i.Id = null);

            return result;
        }

        public static Student GetStudentEF()
        {
            return GetStudentsEF(1).First();
        }
    }
}
