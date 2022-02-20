using BenchmarkDotNet.Running;
using ConsoleApp.Persistence.Dapper.Mapping;
using ConsoleApp.Persistence.EF.Context;
using ConsoleApp.Tests;
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            //var test = new DeleteTest();
            //await test.Init();

            //await test.DeleteByIdSingleDP();
            //await test.DeleteByIdSingleEF();
            //await test.DeleteSingleDPRaw();
            //await test.DeleteSingleEFRaw();


            //BenchmarkRunner.Run<InsertTest>();
            //BenchmarkRunner.Run<InsertManyTest>();
            //BenchmarkRunner.Run<UpdateTest>();
            //BenchmarkRunner.Run<SelectTest>();


            var benchs = BenchmarkConverter.TypeToBenchmarks(typeof(DeleteTest));

            BenchmarkRunner.Run(benchs);

            Console.ReadLine();
        }

        public static void InitDapper()
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            FluentMapper.Initialize(config =>
            {
                config.AddMap(new StudentMap());
                config.ForDommel();
            });

            //SqlMapperExtensions.TableNameMapper = entityType =>
            //{
            //    return entityType == typeof(StudentDP) ? "student" : throw new Exception($"Not supported entity type {entityType}");
            //};

        }

        public static DbContextOptions InitEf()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseSqlServer(Constants.ConnectionStringEF);
            builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            return builder.Options;
        }
    }
}