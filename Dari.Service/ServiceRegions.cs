using Dari.Data;
using Dari.Data.Infrastructure;
using Dari.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Dari.Service
{
    public class ServiceRegions : Service<Region>, IServiceRegions
    {
    

        public ServiceRegions(IUnitOfWork uow) : base(uow)
        {

        }
        public IEnumerable<SelectListItem> GetRegions()
        {
            List<SelectListItem> regions = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = null,
                    Text = " "
                }
            };
            return regions;
        }

        public IEnumerable<SelectListItem> GetRegions(string iso3)
        {
            if (!String.IsNullOrWhiteSpace(iso3))
            {
                using (var con = new DariContext())
                {
                    IEnumerable<SelectListItem> regions = con.Regions.AsNoTracking()
                        .OrderBy(n => n.RegionNameEnglish)
                        .Where(n => n.Iso3 == iso3)
                        .Select(n =>
                           new SelectListItem
                           {
                               Value = n.RegionCode,
                               Text = n.RegionNameEnglish
                           }).ToList();
                    return new SelectList(regions, "Value", "Text");
                }
            }
            return null;
        }
    }
}