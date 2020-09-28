using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dari.Domain
{
   public class Order
    {
        public string UserId { get; set; } 
        public int OrderId { get; set; }
        public int BillId { get; set; }
        public int Qte { get; set; }
        public int? FurnitureId { get; set; }
        public double TotalPrice { get; set; }
        public double FinalPrice { get; set; }

        [ForeignKey("FurnitureId")]
        public virtual Furniture furniture { get; set; }
    }
}
