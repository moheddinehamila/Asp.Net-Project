using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dari.Domain
{
    public  class Basket
    {
        public string UserId { get; set; }  
        public int BasketId { get; set; }
        public int Qte { get; set; }
         public int? FurnitureId { get; set; }
        public double TotalPrice { get; set; } 

        [ForeignKey("FurnitureId")]
        public virtual Furniture furniture { get; set; }
    }
}
