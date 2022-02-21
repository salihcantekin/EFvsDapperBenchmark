using BenchmarkDotNet.Attributes;
using ConsoleApp.DataProviders;
using ConsoleApp.Domain.Entities;
using ConsoleApp.Persistence.EF.Context;
using Dommel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Tests
{
    [SimpleJob(
        BenchmarkDotNet.Engines.RunStrategy.ColdStart,
        BenchmarkDotNet.Jobs.RuntimeMoniker.Net60,
        launchCount: 5,
        targetCount: 10,
        id: "Insert Many Test")]
    [MemoryDiagnoser]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public class FunctionsTest
    {
        private SqlConnection connection;
        private ApplicationDbContext context;

        [GlobalSetup]
        public void Init()
        {
            Program.InitDapper();
            var dbContextOptions = Program.InitEf();

            context = new ApplicationDbContext(dbContextOptions);
            connection = new SqlConnection(Constants.ConnectionStringDapper);

            // let it call modelcreating method
            context.Students.Count();
        }

        [Benchmark(Description = "DP Count")]
        public async Task CountDP()
        {
            await connection.CountAsync<Student>();
        }

        [Benchmark(Description = "EF Count")]
        public async Task CountEF()
        {
            await context.Students.CountAsync();
        }




        [Benchmark(Description = "DP Paged 1,50")]
        public async Task PagedDP()
        {
            (await connection.GetPagedAsync<Student>(1, 50)).ToList();
        }

        [Benchmark(Description = "EF Paged 1,50")]
        public async Task PagedEF()
        {
            await context.Students.Take(50).ToListAsync();
        }




        [Benchmark(Description = "DP Paged 3,75")]
        public async Task Pagedv2DP()
        {
            (await connection.GetPagedAsync<Student>(3, 75)).ToList();
        }

        [Benchmark(Description = "EF Paged 3,75")]
        public async Task Pagedv2EF()
        {
            await context.Students.Skip(75 * 2).Take(75).ToListAsync();
        }
    }
}

