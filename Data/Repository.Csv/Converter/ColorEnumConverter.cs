using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using DataClasses.Enums;

namespace Repository.Csv.Converter
{
    public class ColorEnumConverter : DefaultTypeConverter 
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
           if(Enum.TryParse(typeof(Colors),text,out var color))
            {
                return color;
            }
            throw new InvalidCastException($"Invalid value \"{text}\" passed to ColorEnumConverter by reading the csv file at line {row.Context.RawRow}.");
        }

        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if(value is Enum color)
            {
                return color.ToString();
            }
            throw new InvalidCastException($"Invalid value \"{value}\" passed to ColorEnumConverter by writing to a csv file.");
        }
    }
}
