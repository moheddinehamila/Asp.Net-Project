using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dari.Domain
{
  public  class Verification
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Count must be a natural number")]
        [StringLength(8, ErrorMessage = "Please Enter a Valid CIN Number.", MinimumLength = 8)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Cin")]

        public string cin { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string address { get; set; }
        [Required]
        [Display(Name = "City")]
        public string City { get; set; }
        [Required]
        [Display(Name = "State / Region")]
        public string Country { get; set; }
        [Required]
        public string image { get; set; }
        [Required]

        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }
        [Required]

        public string Email { get; set; }
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "must be a natural number")]
        [StringLength(8, ErrorMessage = "Please Enter a Valid Phone Number.", MinimumLength = 8)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public string userId { get; set; }
        public string fb { get; set; }
        public string tw { get; set; }
        public string gg { get; set; }
    //    public string status { get; set; }


    }
}
