using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dari.web.Models
{
    public class RolesModels : IEnumerable<RolesModels>
    {

        public string Id { get; set; }

        [Required]

        public string Name { get; set; }
        public IEnumerator<RolesModels> GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public class AddUserToRole
    {
       [Required]
        public string username { get; set; }

    }


}