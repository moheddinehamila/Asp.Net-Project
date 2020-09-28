using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dari.Data.Infrastructure;
using Dari.Domain;

namespace Dari.Service
{
    public class ServiceCoupon : Service<Coupon>, IServiceCoupon
    {
        public ServiceCoupon(IUnitOfWork _uow) : base(_uow)
        {
        }

       
    }
}
