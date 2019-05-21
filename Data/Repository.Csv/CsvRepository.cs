using System.Collections.Generic;
using DataClasses;
using Repository.Contracts;
using CsvHelper;
using System.IO;
using Repository.Csv.Mapping;
using System.Text;
using DataClasses.Models;
using System;
using System.Linq;

namespace Repository.Csv
{
    public class CsvRepository: IRepository
    {
        private readonly CsvConfiguration _configuration;
        private IEnumerable<Person> _personlist;

        public CsvRepository(CsvConfiguration configuration)
        {
            _configuration = configuration;
            ReadCsv();
        }

        public T Get<T>(int id) where T : AbstractModel
        {
            return _personlist.Where(x => x.Id == id).FirstOrDefault() as T;
        }

        public  IEnumerable<T> GetList<T>() where T : AbstractModel
        {
            return _personlist as IEnumerable<T>;
        }

        public T Update<T>(T model) where T : AbstractModel
        {

            using (var writer = new StreamWriter(_configuration.Path,true))
            {
                writer.Write(writer.NewLine);
                using (var csv = new CsvWriter(writer))
                {
                    csv.Configuration.HasHeaderRecord = false;
                    csv.Configuration.Delimiter = _configuration.Delimiter;
                    if(model.GetType() == typeof(Person))
                    {
                        var person = model as Person;
                        csv.WriteField(person.Lastname);
                        csv.WriteField(person.Name);
                        csv.WriteField(person.Zipcode + " " + person.City);
                        csv.WriteField((int)person.Color);
                    } 
                }
            }
            ReadCsv();
            return _personlist.OrderByDescending(x => x.Id).FirstOrDefault() as T;
        }

        private void ReadCsv()
        {
            using (var reader = new StreamReader(_configuration.Path))
            {
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.Delimiter = _configuration.Delimiter;
                    csv.Configuration.RegisterClassMap<PersonReadingMapping>();
                    csv.Configuration.Encoding = Encoding.Unicode;
                    csv.Configuration.HasHeaderRecord = false;
                    _personlist = csv.GetRecords<Person>().ToList();
                }
            }
        }
    }
}
