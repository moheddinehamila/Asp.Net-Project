using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dari.Data.Infrastructure;
using Dari.Domain;

namespace Dari.Service
{
    public class ServiceOrder : Service<Order>, IServiceOrder
    {
        public ServiceOrder(IUnitOfWork _uow) : base(_uow)
        {
        }
        
        public IEnumerable<Order> GetByuserid(string UserId)
        {
         
            return GetMany(f => f.UserId.Equals(UserId));
        }
    }
}
