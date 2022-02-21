using BenchmarkDotNet.Attributes;
using ConsoleApp.DataProviders;
using ConsoleApp.Persistence.EF.Context;
using Dapper;
using Dommel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
    public class InsertManyTest
    {
        [Params(10, 100, 1000)]
        public int insertRowCount { get; set; }


        private SqlConnection connection;
        private ApplicationDbContext context;

        [GlobalSetup]
        public void Init()
        {
            Program.InitDapper();
            var dbContextOptions = Program.InitEf();

            connection = new SqlConnection(Constants.ConnectionStringDapper);
            context = new ApplicationDbContext(dbContextOptions);
            
            // let it call modelcreating method
            context.Students.Count();
        }

        [Benchmark(Description = "EF Insert Many")]
        public async Task InsertEF()
        {
            var students = StudentDataProvider.GetStudentsEF(insertRowCount);

            await context.AddRangeAsync(students);
            await context.SaveChangesAsync();
        }


        [Benchmark(Description = "DP Insert Many")]
        public async Task InsertDP()
        {
            var student = StudentDataProvider.GetStudentsDP(insertRowCount);
            await connection.InsertAllAsync(student);
        }
    }
}
