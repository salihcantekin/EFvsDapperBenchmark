using BenchmarkDotNet.Attributes;
using ConsoleApp.Domain.Entities;
using ConsoleApp.Persistence.EF.Context;
using Dapper;
using Dommel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp.Tests
{
    [SimpleJob(
        BenchmarkDotNet.Engines.RunStrategy.ColdStart,
        BenchmarkDotNet.Jobs.RuntimeMoniker.Net60,
        launchCount: 1,
        targetCount: 100,
        id: "Update Test")]
    [MemoryDiagnoser]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public class UpdateTest
    {
        private List<Student> studentList;
        private SqlConnection connection;
        private ApplicationDbContext context;
        private int rowCount;

        private readonly string rawSqlDP = @"UPDATE STUDENT SET first_name = @FirstName WHERE Id = @Id";
        private readonly string rawSqlEF = @"UPDATE STUDENT SET first_name = {1} WHERE Id = {0}";

        private Student GetRandomStudent()
        {
            var student = studentList.OrderBy(i => Guid.NewGuid()).First();
            studentList.Remove(student);
            return student;
        }
        private int GetRandomId() => new Random().Next(1, rowCount);

        [GlobalSetup]
        public async Task Init()
        {
            Program.InitDapper();
            var dbContextOptions = Program.InitEf();

            connection = new SqlConnection(Constants.ConnectionStringDapper);
            context = new ApplicationDbContext(dbContextOptions);
            rowCount = await context.Students.CountAsync();
            studentList = await context.Students.OrderBy(i => Guid.NewGuid()).Take(1000).ToListAsync();
        }


        [Benchmark(Description = "DP Single Update")]
        public async Task UpdateSingleDP()
        {
            var user = GetRandomStudent();
            user.FirstName = user.FirstName.ToUpper();
            await connection.UpdateAsync(user);
        }

        [Benchmark(Description = "EF Single Update")]
        public async Task UpdateSingleEF()
        {
            var user = GetRandomStudent();

            user.FirstName = user.FirstName.ToUpper();
            context.Update(user);
            await context.SaveChangesAsync();
        }

        [Benchmark(Description = "DP Single Update Raw")]
        public async Task UpdateSingleDPRaw()
        {
            await connection.ExecuteAsync(rawSqlDP, new { FirstName = "XXX", Id = GetRandomId() });
        }

        [Benchmark(Description = "EF Single Update Raw")]
        public async Task UpdateSingleEFRaw()
        {
            await context.Database.ExecuteSqlRawAsync(rawSqlEF, GetRandomId(), "XXX");
        }
    }
}
