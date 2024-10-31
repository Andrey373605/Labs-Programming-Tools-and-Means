using System;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Martinvovich_353503_Lab5
{
    public class ConfigReader
    {
        public string FileName { get; set; }

        public static ConfigReader LoadConfig()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            return configuration.Get<ConfigReader>();
        }
    }

}
