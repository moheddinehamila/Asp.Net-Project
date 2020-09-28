using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dari.Data.Infrastructure;
using Dari.Domain;

namespace Dari.Service
{
    public class ServiceBasket: Service<Basket>, IServiceBasket
    {
        public ServiceBasket(IUnitOfWork _uow) : base(_uow)
        {
        }

        public IEnumerable<Basket> GetByCategory(int basketid)
        {
            return GetMany(f => f.BasketId== basketid);
        }

        public IEnumerable<Basket> GetByuserid(string UserId)
        {
            return GetMany(f => f.UserId.Equals(UserId));
        }
    }
}
