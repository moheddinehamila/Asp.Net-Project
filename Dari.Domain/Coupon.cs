using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dari.Domain
{
   public class Coupon
    {
        public int CouponId { get; set; }
        public string Couponvalue { get; set; }
        public int Pourcentage { get; set; }
        public Boolean Used { get; set; }
    }
}
