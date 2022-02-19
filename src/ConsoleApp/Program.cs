using BenchmarkDotNet.Running;
using ConsoleApp.Domain.Entities.Dapper;
using ConsoleApp.Persistence.EF.Context;
using ConsoleApp.Tests;
using Dapper.Contrib.Extensions;
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
            using IHost host = CreateHostBuilder(args).Build();

            //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            initDapper();
            initEf();

            var test = new InsertTest();
            //test.Init();
            //await test.INSERT_Dapper_Single_Student_RawSQL();
            //await test.INSERT_EF_Single_Student_RawSQL();


            //await test.DP_Select_Student_By_Id();
            //await test.EF_Select_Student_By_Id_RawSqwl();
            //await test.DP_FilterBy_FirstName_RawSql();
            //await test.EF_FilterBy_FirstName_RawSql();
#if DEBUG

            //await test.INSERT_EF_Single_Student();
            //await test.INSERT_Dapper_Single_Student();
            //await test.INSERT_EF_Single_Student_RawSQL();
            //await test.INSERT_Dapper_Single_Student_RawSQL();
            //await test.INSERT_EF_100_Students();
            //await test.INSERT_Dapper_100_Students();
            //await test.INSERT_EF_1000_Students();
            //await test.INSERT_Dapper_1000_Students();
            //await test.INSERT_Dapper_1000_Students();
#endif

            BenchmarkRunner.Run<InsertTest>();
            //BenchmarkRunner.Run<SelectTest>();

            await host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services.AddDbContext();
                    //services.AddSingleton<StudentDataProvider>();
                });
        }

        private static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseSqlServer(Constants.ConnectionStringDapper);
            });

            return services;
        }

        private static void initDapper()
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            //SqlMapperExtensions.TableNameMapper = entityType =>
            //{
            //    return entityType == typeof(StudentDP) ? "student" : throw new Exception($"Not supported entity type {entityType}");
            //};

            //FluentMapper.Initialize(config =>
            //{
            //    config.AddMap(new StudentMap());
            //    config.AddMap(new TeacherMap());
            //    config.AddMap(new CourseMap());
            //    //config.ForDommel();
            //});
        }

        private static void initEf()
        {
            var context = new ApplicationDbContext();
            context.Database.Migrate();
            context.Database.OpenConnection();
            //context.Students.FirstOrDefaultAsync().GetAwaiter().GetResult(); // so allow to configure first
            context.Dispose();
        }
    }
}