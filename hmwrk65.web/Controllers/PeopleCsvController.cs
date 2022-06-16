using CsvHelper;
using Faker;
using hmwrk65.data;
using hmwrk65.web.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hmwrk65.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleCsvController : ControllerBase
    {
        private string _connectionString;
        public PeopleCsvController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [Route("getpeople")]
        [HttpGet]
        public List<Person> GetPeople()
        {
            var repo = new PeopleRepository(_connectionString);
            return repo.GetAll();
        }

        [HttpGet]
        [Route("generate")]
        public IActionResult Generate(int amount)
        {
            var people = GetPeople(amount);
            var csv = GetCsv(people);
            byte[] bytes = Encoding.UTF8.GetBytes(csv);
            return File(bytes, "text/csv", $"{amount} People.csv");
        }

        [Route("upload")]
        [HttpPost]
        public void Upload(FileViewModel file)
        {
            int index = file.Base64.IndexOf(",") + 1;
            string base64 = file.Base64.Substring(index);
            byte[] bytes = Convert.FromBase64String(base64);
            var people = GetFromCsvBytes(bytes);
            var repo = new PeopleRepository(_connectionString);
            repo.Add(people);
        }

        [Route("delete")]
        [HttpPost]
        public void Delete()
        {
            var repo = new PeopleRepository(_connectionString);
            repo.DeleteAll();
        }

        static List<Person> GetPeople(int amount)
        {
            return Enumerable.Range(1, amount).Select(_ =>
            {
                return new Person
                {
                    FirstName = Name.First(),
                    LastName = Name.Last(),
                    Age = RandomNumber.Next(10, 60),
                    Address = Address.StreetAddress(),
                    Email = Internet.Email()
                };
            }).ToList();
        }

        static string GetCsv(List<Person> people)
        {
            var builder = new StringBuilder();
            var stringWriter = new StringWriter(builder);
            using var csv = new CsvWriter(stringWriter, CultureInfo.InvariantCulture);
            csv.WriteRecords(people);
            return builder.ToString();
        }

        static List<Person> GetFromCsvBytes(byte[] bytes)
        {
            using var memoryStream = new MemoryStream(bytes);
            var streamReader = new StreamReader(memoryStream);
            using var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            return reader.GetRecords<Person>().ToList();
        }


    }
}
