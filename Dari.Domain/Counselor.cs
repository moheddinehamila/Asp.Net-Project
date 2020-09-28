using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dari.Domain
{
   public class Counselor
    {
        [Key]
        public int Id { get; set; }

        public string cin { get; set; }
       
        public string address { get; set; }
        
        public string City { get; set; }
       
        public string Country { get; set; }
        public string image { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }

        public string userId { get; set; }
        public string fb { get; set; }
        public string tw { get; set; }
        public string gg { get; set; }

    }
}
