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
        targetCount: 50,
        id: "Search Test")]
    [MemoryDiagnoser]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public class SearchTest
    {
        private SqlConnection connection;
        private ApplicationDbContext context;

        private DateTime startDateTime = new DateTime(2000, 1, 1);
        private DateTime endDateTime = new DateTime(2022, 1, 1);

        [GlobalSetup]
        public void Init()
        {
            Program.InitDapper();
            var dbContextOptions = Program.InitEf();

            connection = new SqlConnection(Constants.ConnectionStringDapper);
            context = new ApplicationDbContext(dbContextOptions);
        }

        #region Equal Search

        [Benchmark(Description = "DP Count Equal [Mehmet]")]
        public async Task EqualDP()
        {
            await connection.CountAsync<Student>(i => i.FirstName == "Mehmet");
        }

        [Benchmark(Description = "EF Count Equal [Mehmet]")]
        public async Task EqualEF()
        {
            await context.Students.CountAsync(i => i.FirstName == "Mehmet");
        }

        #endregion


        #region StartsWith Search

        [Benchmark(Description = "DP Count StartsWith [A]")]
        public async Task StartsWithDP()
        {
            await connection.CountAsync<Student>(i => i.FirstName.StartsWith('A'));
        }

        [Benchmark(Description = "EF Count StartsWith [A]")]
        public async Task StartsWithEF()
        {
            await context.Students.CountAsync(i => i.FirstName.StartsWith("A"));
        }

        [Benchmark(Description = "EF Count StartsWith [A] F")]
        public async Task StartsWithEFFunctions()
        {
            await context.Students.CountAsync(i => EF.Functions.Like(i.FirstName, "A%"));
        }

        #endregion


        #region Contains Search

        [Benchmark(Description = "DP Count Contains [A]")]
        public async Task SearchDP()
        {
            await connection.CountAsync<Student>(i => i.FirstName.Contains('A'));
        }

        [Benchmark(Description = "EF Count Contains [A]")]
        public async Task SearchEF()
        {
            await context.Students.CountAsync(i => i.FirstName.Contains("A"));
        }

        [Benchmark(Description = "EF Count Contains [A] F")]
        public async Task SearchEFFunctions()
        {
            await context.Students.CountAsync(i => EF.Functions.Like(i.FirstName, "%A%"));
        }

        #endregion

        #region DateTime Search

        [Benchmark(Description = "DP Count Between Date")]
        public async Task SearchDateTimeDP()
        {
            await connection.CountAsync<Student>(i => i.BirthDate >= startDateTime && i.BirthDate <= endDateTime);
        }

        [Benchmark(Description = "EF Count Between Date")]
        public async Task SearchDateTimeEF()
        {
            await context.Students.CountAsync(i => i.BirthDate >= startDateTime && i.BirthDate <= endDateTime);
        }

        #endregion


    }
}
