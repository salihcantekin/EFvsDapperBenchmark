using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Domain.Entities
{
    [Table("efdapperbenchmark.teacher")]
    public class TeacherDP
    {
        [Computed]
        public int id { get; set; }

        public String first_name { get; set; }

        public String last_name { get; set; }

        public DateTime birth_date { get; set; }
    }
}
