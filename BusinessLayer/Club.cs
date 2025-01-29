using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Club:User
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string League { get; set; }
        public string Website { get; set; }

        public Club(string name,string country,string city,string league,string website) 
        { 
         Name = name;
         Country = country;
         City = city;
         League = league;
         Website = website;
        }
    }
}
