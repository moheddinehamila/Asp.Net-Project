using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dari.Domain
{

    public class Recommendation
    {
        [Key]
        public int RecommendationId { get; set; }
        public String Title { get; set; }
        public int Budget { get; set; }
        public TypeChoix Type { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Adress { get; set; }
        public int Price { get; set; }
        public string DescriptionA { get; set; }
        public string DescriptionR { get; set; }
        [MaxLength(100)]
        public String UserId { get; set; }





    }
}
