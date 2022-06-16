using System.Collections.Generic;
using System.Linq;

namespace hmwrk65.data
{
    public class PeopleRepository
    {
        private string _connectionString;
        public PeopleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Person> GetAll()
        {
            using var context = new PeopleDataContext(_connectionString);
            return context.People.ToList(); 
        }

        public void Add(List<Person> people)
        {
            using var context = new PeopleDataContext(_connectionString);
            context.People.AddRange(people);
            context.SaveChanges();
        }

        public void DeleteAll()
        {
            using var context = new PeopleDataContext(_connectionString);
            var people = context.People.ToList();
            context.People.RemoveRange(people);
            context.SaveChanges();
        }
    }
}
