using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExperisMovieProject.Models;

namespace ExperisMovieProject.Operations
{
    internal class CsvOperations
    {
        private static CsvConfiguration Configure()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                MissingFieldFound = null

            };
            return config;
        }
        public static List<Users> ParseCsvToUsers()
        {
            CsvConfiguration config = Configure();
            using (var reader = new StreamReader(@"C:\Users\lknud\source\repos\ExperisMovieProject\ExperisMovieProject\Resources\Users.txt"))

            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<Users>();
                return records.ToList();
            }
        }
        public static List<Products> ParseCsvToProducts()
        {
            CsvConfiguration config = Configure();
            using (var reader = new StreamReader(@"C:\Users\lknud\source\repos\ExperisMovieProject\ExperisMovieProject\Resources\Products.txt"))

            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<Products>();
                return records.ToList();
            }
        }
        public static List<CurrentUserSession> ParseCsvToUserSession()
        {
            CsvConfiguration config = Configure();
            using (var reader = new StreamReader(@"C:\Users\lknud\source\repos\ExperisMovieProject\ExperisMovieProject\Resources\CurrentUserSession.txt"))

            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<CurrentUserSession>();
                return records.ToList();
            }
        }

    }
}
