using Dari.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dari.Service
{
    public interface IServiceRegions : IService<Region>
    {
        IEnumerable<SelectListItem> GetRegions();
        IEnumerable<SelectListItem> GetRegions(string iso3);

    }
}
