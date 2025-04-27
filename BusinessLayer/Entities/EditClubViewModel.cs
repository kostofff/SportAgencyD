using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Entities
{
    public class EditClubViewModel
    {
        public string Id { get; set; }
        public string ClubName { get; set; }
        public Country Country { get; set; }
        public string City { get; set; }
        public string League { get; set; }
        public string Website { get; set; }
    }

}
