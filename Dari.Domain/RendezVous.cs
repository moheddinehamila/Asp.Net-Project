using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Dari.Domain
{
    public class RendezVous
    {

        public int id { get; set; }
        public String idbuyer { get; set; }
        public String idseller { get; set; }
        public String title { get; set; }
        [DefaultValue("orange")]
        public String color { get; set; }
        public String start { get; set; }
        public String end { get; set; }
        public bool? allDay { get; set; }
        public bool? validappointment { get; set; }
        public String description { get; set; }

        //[ForeignKey("idbuyer")]
        //public virtual User buyer { get; set; }
        //[ForeignKey("idseller")]
        //public virtual User seller { get; set; }

    }
}
