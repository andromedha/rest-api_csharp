using DataClasses;
using System.Collections.Generic;

namespace PersonManagement.Contracts
{
    public interface IPersonManager
    {
        IEnumerable<Person> GetPersons();

        Person GetPerson(int id);

        IEnumerable<Person> GetPersonsWithColor(string color);

        Person AddPerson(Person model);
    }
}
