using System;
using System.Collections.Generic;
using System.Text;

namespace DataClasses
{
    public class SqlConfiguration
    {
        public string Server { get; set; }

        public string Database { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public bool TrustedConnection { get; set; }
    }
}
