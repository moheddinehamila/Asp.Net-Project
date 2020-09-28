using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dari.Domain
{
   public class AnnonceVente
    {
        [Key]
        
        public int VenteId { get; set; }


        
        public String UserId { get; set; }
       
        public string Name { get; set; }
       
        public string Image { get; set; }
        
        public string Adress { get; set; }
        
        public int Price { get; set; }
        [DataType(DataType.MultilineText)]
        
        public string Description { get; set; }





        public string Category { get; set; }


        public double Height { get; set; }

        public double Width { get; set; }



        public double Depth { get; set; }
    }
}
