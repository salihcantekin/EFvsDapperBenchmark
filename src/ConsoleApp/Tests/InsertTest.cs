using BenchmarkDotNet.Attributes;
using ConsoleApp.DataProviders;
using ConsoleApp.Persistence.EF.Context;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ConsoleApp.Tests
{
    [SimpleJob(
        BenchmarkDotNet.Engines.RunStrategy.ColdStart,
        BenchmarkDotNet.Jobs.RuntimeMoniker.Net60,
        targetCount: 50,
        id: "Insert Test")]
    //[SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.NetCoreApp50, id: "Insert Test", targetCount: 10, warmupCount: 5)]
    [MemoryDiagnoser]
    //[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    //[MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public class InsertTest
    {
        private readonly string rawSqlDP = @"INSERT INTO efdapperbenchmark.student (first_name, last_name, birth_date) 
                                            VALUES (@first_name, @last_name, @birth_date)";

        private readonly string rawSqlEF = @"INSERT INTO efdapperbenchmark.student (first_name, last_name, birth_date) 
                                            VALUES ({0}, {1}, {2})";

        [Params(1, 10, 100)]
        public int insertRowCount { get; set; }


        private ApplicationDbContext _context;
        private SqlConnection _connection;

        [GlobalSetup]
        public void Init()
        {
            _context = new ApplicationDbContext();
            _connection = new SqlConnection(Constants.ConnectionStringDapper);
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            //var context = new ApplicationDbContext();
            //context.Students.RemoveRange(context.Students);
            //context.SaveChangesAsync();
        }

        [Benchmark(Description = "EF Insert")]
        public async Task InsertByParamsEF()
        {
            var students = StudentDataProvider.GetStudentsEF(insertRowCount);

            using var context = new ApplicationDbContext();
            await context.AddRangeAsync(students);
            await context.SaveChangesAsync();
        }

        [Benchmark(Description = "EF Insert Ex Context")]
        public async Task InsertByParamsExistingDbContextEF()
        {
            var students = StudentDataProvider.GetStudentsEF(insertRowCount);

            await _context.AddRangeAsync(students);
            await _context.SaveChangesAsync();
        }

        [Benchmark(Description = "Dapper Insert")]
        public async Task InsertByParamsDapper()
        {
            var student = StudentDataProvider.GetStudentsDP(insertRowCount);

            using var connection = new SqlConnection(Constants.ConnectionStringDapper);
            await connection.InsertAsync(student);
        }

        [Benchmark(Description = "Dapper Ex Insert")]
        public async Task InsertByParamsExistingConnDapper()
        {
            var student = StudentDataProvider.GetStudentsDP(insertRowCount);

            await _connection.InsertAsync(student);
        }


        #region Single Insert With RawSql

        [Benchmark(Description = "EF Single RAWSQL")]
        public async Task INSERT_EF_Single_Student_RawSQL()
        {
            var student = StudentDataProvider.GetStudentEF();

            using var context = new ApplicationDbContext();
            await context.Database.ExecuteSqlRawAsync(rawSqlEF, student.FirstName, student.LastName, student.BirthDate);
        }

        [Benchmark(Description = "DP Single RAWSQL")]
        public async Task INSERT_Dapper_Single_Student_RawSQL()
        {
            var student = StudentDataProvider.GetStudentDP();

            using var connection = new SqlConnection(Constants.ConnectionStringDapper);
            await connection.ExecuteAsync(rawSqlDP, student);
        }

        #endregion
    }
}
