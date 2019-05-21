using DataClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PersonManagement.Contracts;
using Repository.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace PersonManagement.Unittests
{
    [TestClass]
    public class PersonManagerTests
    {

        private Person CreatePerson()
        {
            return new Person
            {
                Id = 5,
                Name = "Bernd",
                Lastname = "Baum",
                Zipcode = "45678",
                City = "Ferncity",
                Color = DataClasses.Enums.Colors.rot
            };
        }

        private IEnumerable<Person> CreatePersons()
        {
            return new List<Person>
            {
                new Person
            {
                Id = 5,
                Name = "Bernd",
                Lastname = "Baum",
                Zipcode = "45678",
                City = "Ferncity",
                Color = DataClasses.Enums.Colors.rot
            },
                new Person
            {
                Id = 10,
                Name = "Katja",
                Lastname = "Katze",
                Zipcode = "9999",
                City = "Nahstadt",
                Color = DataClasses.Enums.Colors.grün
            }
            };
        }

        [TestMethod]
        [ExpectedException(typeof(DataNotFoundException))]
        public void GetPerson_CallWrongId_DataNotFoundException()
        {
            var person = CreatePerson();
            var mock = new Mock<IRepository>();
            mock.Setup(service => service.Get<Person>(5)).Returns(person);

            var personmanager = new PersonManager(mock.Object);
            personmanager.GetPerson(400);

        }

        [TestMethod]
        public void GetPerson_CallCorrectId_ReturnPerson()
        {
            var person = CreatePerson();
            var mock = new Mock<IRepository>();
            mock.Setup(service => service.Get<Person>(5)).Returns(person);

            var personmanager = new PersonManager(mock.Object);
            Assert.AreEqual(person, personmanager.GetPerson(5));

        }

        [TestMethod]
        public void GetPersons_Call_ReturnListPersons()
        {
            var persons = CreatePersons();
            var mock = new Mock<IRepository>();
            mock.Setup(service => service.GetList<Person>()).Returns(persons);

            var personmanager = new PersonManager(mock.Object);
            Assert.AreEqual(persons, personmanager.GetPersons());

        }


        [TestMethod]
        public void GetPersonsWithColor_CallExistingColor_ReturnListPersons()
        {
            var persons = CreatePersons();
            var mock = new Mock<IRepository>();
            mock.Setup(service => service.GetList<Person>()).Returns(persons);

            var personmanager = new PersonManager(mock.Object);
            CollectionAssert.AreEqual(persons.Where(x => x.Color == DataClasses.Enums.Colors.rot).ToList(), personmanager.GetPersonsWithColor("rot").ToList());
        }

        [TestMethod]
        public void GetPersonsWithColor_CallExisatingColor_ReturnListPersons()
        {
            var persons = CreatePersons();
            var mock = new Mock<IRepository>();
            mock.Setup(service => service.GetList<Person>()).Returns(persons);

            var personmanager = new PersonManager(mock.Object);
            CollectionAssert.AreEqual( new List<Person>(), personmanager.GetPersonsWithColor("blau").ToList());
        }
    }
}
