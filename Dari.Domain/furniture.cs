using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dari.Domain
{
    public class Furniture
    {
       
        [Required]
        public int FurnitureId { get; set; } 

        [Required]
        [Range(0, 500, ErrorMessage = "The Quantity must be between 0 and 500")]
        public int Qte { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Type { get; set; } 

        [Required]
        [Range(0.1, 5000, ErrorMessage = "The Price must be between 0.1 and 5000 Dinars")]
        public double Price{ get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        [StringLength(225, ErrorMessage = "The description must not surpass 255 characters")]
        public string description{ get; set; }
        
        [Required]
        [Range(0, 500, ErrorMessage = "The Height must be between 1 and 500 CM")]
        public int Height { get; set; }

        [Required]
        [Range(0, 500, ErrorMessage = "The Width must be between 1 and 500 CM")]
        public int Width { get; set; }

        [Required]
        [Range(0, 500, ErrorMessage = "Th Depth must be between 1 and 500 CM")]
        public int Depth { get; set; }

        public string UserId { get; set; } 


    }
}
