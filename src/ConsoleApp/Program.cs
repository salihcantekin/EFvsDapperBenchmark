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
        static void Main(string[] args)
        {
            //BenchmarkRunner.Run<InsertTest>();
            //BenchmarkRunner.Run<InsertManyTest>();
            //BenchmarkRunner.Run<SelectTest>();
            //BenchmarkRunner.Run<SearchTest>();
            //BenchmarkRunner.Run<FunctionsTest>();
            //BenchmarkRunner.Run<UpdateTest>();
            //BenchmarkRunner.Run<DeleteTest>();


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