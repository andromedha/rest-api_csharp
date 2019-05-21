using DataClasses;
using PersonManagement.Contracts;
using Repository.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace PersonManagement
{
    public class PersonManager : IPersonManager
    {
        private readonly IRepository _repository;

        public PersonManager(IRepository repository)
        {
            _repository = repository;
        }

        public Person AddPerson(Person model)
        {
            return _repository.Update<Person>(model);
        }

        public Person GetPerson(int id)
        {
            var result = _repository.Get<Person>(id);
            return result ?? throw new DataNotFoundException();
        }

        public IEnumerable<Person> GetPersons()
        {
            return _repository.GetList<Person>();
        }

        public IEnumerable<Person> GetPersonsWithColor(string color)
        {
            return _repository.GetList<Person>().Where(x => x.Color.ToString() == color);
        }
    }
}
