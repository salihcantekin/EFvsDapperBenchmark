﻿using BenchmarkDotNet.Attributes;
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
    [HtmlExporter]
    [JsonExporterAttribute.FullCompressed]
    [MarkdownExporterAttribute.GitHub]
    [SimpleJob(
        BenchmarkDotNet.Engines.RunStrategy.ColdStart,
        BenchmarkDotNet.Jobs.RuntimeMoniker.Net60,
        launchCount: 1,
        targetCount: 100,
        id: "Insert Test")]
    [MemoryDiagnoser]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public class InsertTest
    {
        private readonly string rawSqlDP = @"INSERT INTO student (first_name, last_name, birth_date) 
                                            VALUES (@FirstName, @LastName, @BirthDate)";

        private readonly string rawSqlEF = @"INSERT INTO student (first_name, last_name, birth_date) 
                                            VALUES ({0}, {1}, {2})";

        private SqlConnection connection;
        private ApplicationDbContext context;

        [GlobalSetup]
        public void Init()
        {
            Program.InitDapper();
            var dbContextOptions = Program.InitEf();

            connection = new SqlConnection(Constants.ConnectionStringDapper);
            context = new ApplicationDbContext(dbContextOptions);
        }

        [Benchmark(Description = "EF Single Insert")]
        public async Task InsertByParamsEF()
        {
            var student = StudentDataProvider.GetStudentEF();

            await context.AddAsync(student);
            await context.SaveChangesAsync();
        }


        [Benchmark(Description = "DP Single Insert")]
        public async Task InsertByParamsDapper()
        {
            var student = StudentDataProvider.GetStudentDP();

            await connection.InsertAsync(student);
        }


        #region Single Insert With RawSql

        [Benchmark(Description = "EF Single Raw")]
        public async Task INSERT_EF_Single_Student_RawSQL()
        {
            var student = StudentDataProvider.GetStudentEF();

            await context.Database.ExecuteSqlRawAsync(rawSqlEF, student.FirstName, student.LastName, student.BirthDate);
        }

        [Benchmark(Description = "DP Single Raw")]
        public async Task INSERT_Dapper_Single_Student_RawSQL()
        {
            var student = StudentDataProvider.GetStudentDP();

            await connection.ExecuteAsync(rawSqlDP, student);
        }

        #endregion
    }
}
