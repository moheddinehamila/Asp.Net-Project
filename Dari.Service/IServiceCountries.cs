using Dari.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dari.Service
{
    public interface IServiceCountries : IService<Country>
    {
        IEnumerable<SelectListItem> GetCountries();
    }
}
