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
        launchCount: 1,
        targetCount: 50,
        id: "Select Test")]
    [MemoryDiagnoser]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public class SelectTest
    {
        private int rowCount;
        private string firstName;

        private int GetRandomId() => new Random().Next(1, rowCount);

        private SqlConnection connection;
        private ApplicationDbContext context;

        [GlobalSetup]
        public async Task Init()
        {
            Program.InitDapper();
            var dbContextOptions = Program.InitEf();

            context = new ApplicationDbContext(dbContextOptions);
            connection = new SqlConnection(Constants.ConnectionStringDapper);

            rowCount = await context.Students.CountAsync();
            firstName = await context.Students.OrderBy(i => Guid.NewGuid()).Select(i => i.FirstName).FirstAsync();
        }

        #region Find Single

        [Benchmark(Description = "EF Find")]
        public async Task EF_Select_Student_By_Id_Linq()
        {
            int id = GetRandomId();
            await context.Students.FindAsync(id);
        }

        [Benchmark(Description = "DP Find")]
        public async Task DP_Select_Student_By_Id_Linq()
        {
            int id = GetRandomId();
            await connection.GetAsync<Student>(id);
        }

        #endregion

        #region SingleOrDefault RawSql

        [Benchmark(Description = "EF SingleOrDefault RawSql")]
        public async Task EF_Select_Student_By_Id_RawSqwl()
        {
            int id = GetRandomId();
            await context.Students.FromSqlRaw("SELECT * from student WHERE id = {0}", id).SingleOrDefaultAsync();
        }

        [Benchmark(Description = "DP SingleOrDefault RawSql")]
        public async Task DP_Select_Student_By_Id()
        {
            int id = GetRandomId();
            await connection.QuerySingleOrDefaultAsync<Student>("SELECT * from student WHERE id = @pid", new { pid = id });
        }

        #endregion

        #region Filtered By FirstName

        [Benchmark(Description = "EF Filter By FirstName LinQ")]
        public async Task EF_FilterBy_FirstName_LinQ()
        {
            await context.Students.Where(i => i.FirstName == firstName).ToListAsync();
        }


        [Benchmark(Description = "DP Filter By FirstName LinQ")]
        public async Task DP_FilterBy_FirstName_LinQ()
        {
            (await connection.SelectAsync<Student>(i => i.FirstName == firstName)).ToList();
        }


        [Benchmark(Description = "EF Filter By FirstName RawSql")]
        public async Task EF_FilterBy_FirstName_RawSql()
        {
            await context.Students.FromSqlRaw("SELECT * from student WHERE first_name = {0}", firstName).ToListAsync();
        }

        [Benchmark(Description = "DP Filter By FirstName RawSql")]
        public async Task DP_FilterBy_FirstName_RawSql()
        {
            (await connection.QueryAsync<Student>("SELECT * from student WHERE first_name = @FirstName", new { FirstName = firstName })).ToList();
        }

        #endregion

        #region Get All

        [Benchmark(Description = "EF Get ALL")]
        public async Task EF_Select_Student_ALL()
        {
            await context.Students.ToListAsync();
        }


        [Benchmark(Description = "DP Get ALL")]
        public async Task DP_Select_Student_ALL()
        {
            (await connection.GetAllAsync<Student>()).ToList();
        }

        #endregion
    }
}
