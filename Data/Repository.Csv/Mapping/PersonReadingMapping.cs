using CsvHelper.Configuration;
using DataClasses;
using Repository.Csv.Converter;
using System;
using System.Linq;

namespace Repository.Csv.Mapping
{
    public sealed class PersonReadingMapping : ClassMap<Person>
    {
        public PersonReadingMapping()
        {
            Map(m => m.Name).Index(0);
            Map(m => m.Lastname).Index(1);
            Map(m => m.Zipcode).ConvertUsing(row => row.GetField(2).Trim().Split(' ').First());
            Map(m => m.City).ConvertUsing(row => string.Concat(row.GetField(2).Trim().Split(' ').Skip(1)));
            Map(m => m.Color).Index(3).TypeConverter<ColorEnumConverter>();
            Map(m => m.Id).ConvertUsing(row => row.Context.RawRow);
        }
    }
}
