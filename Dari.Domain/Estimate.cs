using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dari.Domain
{
   public class Estimate
    {
        [Key]
        public int EstimateId { get; set; }
        public string RegionCode { get; set; }
        public TypeChoix Type { get; set; }
        public int MetrePrice { get; set; }



    }
}
