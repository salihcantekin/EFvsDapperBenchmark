using BenchmarkDotNet.Attributes;
using ConsoleApp.Domain.Entities.Dapper;
using ConsoleApp.Persistence.EF.Context;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp.Tests
{
    [SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.NetCoreApp31, id: "Select Test Core3.1", targetCount: 10, warmupCount: 50)]
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public class SelectTest
    {
        private int dpRowCount = 0, efRowCount = 0;
        private String dpFirstName, efFirstName;


        private int getDPId() => new Random().Next(1, dpRowCount);
        private int getEFId() => new Random().Next(1, efRowCount);


        [GlobalSetup]
        public async Task Init()
        {
            if (dpRowCount == 0 || efRowCount == 0)
            {
                using var connection = new SqlConnection(Constants.ConnectionStringDapper);
                dpRowCount = await connection.ExecuteScalarAsync<int>("SELECT COUNT(0) FROM STUDENT");

                using var context = new ApplicationDbContext();
                efRowCount = await context.Students.CountAsync();
            }

            if (String.IsNullOrEmpty(dpFirstName) || String.IsNullOrEmpty(efFirstName))
            {
                using var context = new ApplicationDbContext();
                efFirstName = (await context.Students.FromSqlRaw("SELECT * FROM student ORDER BY RANDOM()").FirstOrDefaultAsync()).FirstName;

                using var connection = new SqlConnection(Constants.ConnectionStringDapper);
                dpFirstName = await connection.QueryFirstOrDefaultAsync<String>("SELECT first_name FROM student ORDER BY RANDOM() LIMIT 1");
            }
        }

        #region Find Single

        [Benchmark(Description = "EF Find Single")]
        public async Task EF_Select_Student_By_Id_Linq()
        {
            using var context = new ApplicationDbContext();
            int id = getEFId();
            var student = await context.Students.FindAsync(id);
            student = null;
        }

        [Benchmark(Description = "DP Find Single")]
        public async Task DP_Select_Student_By_Id_Linq()
        {
            using var connection = new SqlConnection(Constants.ConnectionStringDapper);
            int id = getDPId();
            var student = await connection.GetAsync<StudentDP>(id);
            student = null;
        }

        #endregion

        #region SingleOrDefault RawSql

        [Benchmark(Description = "EF SingleOrDefault RawSql")]
        public async Task EF_Select_Student_By_Id_RawSqwl()
        {
            using var context = new ApplicationDbContext();
            int id = getEFId();
            var student = await context.Students.FromSqlRaw("SELECT * FROM student WHERE id = {0}", id).SingleOrDefaultAsync();
            student = null;
        }

        [Benchmark(Description = "DP SingleOrDefault RawSql")]
        public async Task DP_Select_Student_By_Id()
        {
            using var connection = new SqlConnection(Constants.ConnectionStringDapper);
            int id = getDPId();
            var student = await connection.QuerySingleOrDefaultAsync<StudentDP>("SELECT * FROM student WHERE id = @pid", new { pid = id });
            student = null;
        }

        #endregion

        #region Filtered By FirstName

        [Benchmark(Description = "EF Filter By FirstName LinQ")]
        public async Task EF_FilterBy_FirstName_LinQ()
        {
            using var context = new ApplicationDbContext();
            var list = await context.Students.Where(i => i.FirstName == efFirstName).ToListAsync();
            list = null;
        }

        [Benchmark(Description = "EF Filter By FirstName RawSql")]
        public async Task EF_FilterBy_FirstName_RawSql()
        {
            using var context = new ApplicationDbContext();
            var list = await context.Students.FromSqlRaw("SELECT * FROM student WHERE first_name = {0}", efFirstName).ToListAsync();
            list = null;
        }

        [Benchmark(Description = "DP Filter By FirstName RawSql")]
        public async Task DP_FilterBy_FirstName_RawSql()
        {
            using var connection = new SqlConnection(Constants.ConnectionStringDapper);
            var student = await connection.QueryAsync<StudentDP>("SELECT * FROM student WHERE first_name = @dpFirstName", new { dpFirstName });
            student = null;
        }

        #endregion

        #region Get All

        [Benchmark(Description = "DP Get ALL")]
        public async Task DP_Select_Student_ALL()
        {
            using var connection = new SqlConnection(Constants.ConnectionStringDapper);
            var student = await connection.GetAllAsync<StudentDP>();
            student = null;
        }

        [Benchmark(Description = "EF Get ALL")]
        public async Task EF_Select_Student_ALL()
        {
            using var context = new ApplicationDbContext();
            var list = await context.Students.ToListAsync();
            list = null;
        }

        #endregion
    }
}
