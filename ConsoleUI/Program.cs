﻿using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Build()).Enrich.FromLogContext()
                .WriteTo.Console().CreateLogger();

            Log.Logger.Information("Application Starting");

            var host = Host.CreateDefaultBuilder().ConfigureServices(((context, collection) =>
                {
                    collection.AddTransient<ITestService, TestService>();
                }))
                .UseSerilog().Build();

            var svc = ActivatorUtilities.CreateInstance<TestService>(host.Services);
            svc.Run();
        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
        }
    }
}
