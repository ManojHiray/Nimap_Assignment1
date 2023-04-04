using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ManojHiray_Nimap.Config
{
    public class StoreConnection
    {
        public static string getConnection()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);

            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:conn");
            return (constring);

           // return ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        }
    }
}
