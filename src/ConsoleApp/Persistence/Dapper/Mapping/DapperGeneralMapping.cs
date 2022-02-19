using ConsoleApp.Domain.Entities;
using Dapper.FluentMap.Dommel.Mapping;
using Dapper.FluentMap.Mapping;
using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Persistence.Dapper.Mapping
{
    public class StudentMap : ClassMapper<StudentEF>
    {
        public StudentMap()
        {
            Table("student");
            Map(i => i.Id).Column("id").Key(KeyType.Identity);
            Map(i => i.FirstName).Column("first_name");
            Map(i => i.LastName).Column("last_name");
            Map(i => i.BirthDate).Column("birth_date");
            AutoMap();
        }
    }

    public class TeacherMap : ClassMapper<Teacher>
    {
        public TeacherMap()
        {
            Table("teacher");
            Map(i => i.Id).Column("id").Key(KeyType.Identity);
            Map(i => i.FirstName).Column("first_name");
            Map(i => i.LastName).Column("last_name");
            Map(i => i.BirthDate).Column("birth_date");
            AutoMap();
        }
    }

    public class CourseMap : ClassMapper<Course>
    {
        public CourseMap()
        {
            Table("course");
            Map(i => i.Id).Column("id").Key(KeyType.Identity);
            Map(i => i.Name).Column("name");
            Map(i => i.IsActive).Column("is_active");
            AutoMap();
        }
    }
}
