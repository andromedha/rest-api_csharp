using DataClasses.Enums;
using DataClasses.Models;

namespace DataClasses
{
    public class Person : AbstractModel
    {
        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Zipcode { get; set; }

        public string City { get; set; }

        public Colors Color { get; set; }
    }
}
