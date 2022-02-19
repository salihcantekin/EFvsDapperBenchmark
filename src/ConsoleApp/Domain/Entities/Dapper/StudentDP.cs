using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Domain.Entities.Dapper
{
    [Table("efdapperbenchmark.student")]
    public class StudentDP
    {
        [Computed]
        public int id { get; set; }

        public String first_name { get; set; }

        public String last_name { get; set; }

        public DateTime birth_date { get; set; }
    }
}
