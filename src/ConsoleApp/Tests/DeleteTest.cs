using BenchmarkDotNet.Attributes;
using ConsoleApp.Domain.Entities;
using ConsoleApp.Persistence.EF.Context;
using Dapper;
using Dommel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp.Tests
{
    [SimpleJob(
        BenchmarkDotNet.Engines.RunStrategy.ColdStart,
        BenchmarkDotNet.Jobs.RuntimeMoniker.Net60,
        launchCount: 2,
        targetCount: 50,
        id: "Delete Test")]
    [MemoryDiagnoser]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public class DeleteTest
    {
        private List<Student> studentList;
        private SqlConnection connection;
        private ApplicationDbContext context;
        private int rowCount;

        private readonly string rawSqlDP = @"DELETE FROM STUDENT WHERE Id = @Id";
        private readonly string rawSqlEF = @"DELETE FROM STUDENT WHERE Id = {0}";

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


        [Benchmark(Description = "DP Single Delete")]
        public async Task DeleteSingleDP()
        {
            var student = GetRandomStudent();
            await connection.DeleteAsync(student);
        }

        [Benchmark(Description = "EF Single Delete")]
        public async Task DeleteSingleEF()
        {
            var student = GetRandomStudent();

            context.Students.Remove(student);
            await context.SaveChangesAsync();
        }


        [Benchmark(Description = "DP Single Delete Raw")]
        public async Task DeleteSingleDPRaw()
        {
            await connection.ExecuteAsync(rawSqlDP, new { Id = GetRandomId() });
        }

        [Benchmark(Description = "EF Single Delete Raw")]
        public async Task DeleteSingleEFRaw()
        {
            await context.Database.ExecuteSqlRawAsync(rawSqlEF, GetRandomId());
        }
    }
}
