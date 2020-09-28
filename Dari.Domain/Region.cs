using System.ComponentModel.DataAnnotations;

namespace Dari.Domain
{
    public class Region
    {
        [Key]
       
        public string RegionCode { get; set; }

        [Required]
        [MaxLength(3)]
        public string Iso3 { get; set; }

        [Required]
        public string RegionNameEnglish { get; set; }

        public virtual Country Country { get; set; }
    }
}