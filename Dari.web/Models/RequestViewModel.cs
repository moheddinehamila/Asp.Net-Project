using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dari.web.Models
{
    public enum TypeChoix { Buy, Rent }
    public enum TypeDari { S_0, S_1, S_2, S_3, S_4, S_Plus, Villa, Fond_De_Commerce, Terrain,Local }
    public class RequestViewModel
    {
        [Key]
        public int RequestId { get; set; }
        [Range(1, Int32.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public int Budget { get; set; }
        [Required, MinLength(8), MaxLength(8)]
        public String Phone { get; set; }
        public TypeChoix TypeCh { get; set; }
        public TypeDari Type { get; set; }
        [DataType(DataType.MultilineText)]
        public String Description { get; set; }
        [Range(1, Int32.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public int Area { get; set; }
        public DateTime Date { get; set; }
        public String Image { get; set; }
        public String Title { get; set; }
        [MaxLength(100)]
        public String UserId { get; set; }
        public String Country { get; set; }
        [Required]
        [Display(Name = "Country")]
        public string SelectedCountryIso3 { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }

        [Required]
        [Display(Name = "State / Region")]
        public string SelectedRegionCode { get; set; }
        public IEnumerable<SelectListItem> Regions { get; set; }

    }
}