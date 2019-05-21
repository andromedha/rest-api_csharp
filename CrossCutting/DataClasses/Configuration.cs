using System;
using System.Collections.Generic;
using System.Text;

namespace DataClasses
{
    public class Configuration
    {
        public CsvConfiguration CsvConfiguration { get; set; }

        public SqlConfiguration SqlConfiguration { get; set; }
    }
}
