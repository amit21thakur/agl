using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Serilog;


namespace PersonPets.API.Common
{
    public static class LoggerConfig
    {
        public static void RegisterLogger()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            string appSettingsEnvFile = $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json";

            if (File.Exists(appSettingsEnvFile))
                configurationBuilder = configurationBuilder.AddJsonFile(appSettingsEnvFile);

            IConfigurationRoot configuration = configurationBuilder.Build();

            LoggerConfiguration logConfig = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(configuration);

            Log.Logger = logConfig.CreateLogger();
        }
    }
}
