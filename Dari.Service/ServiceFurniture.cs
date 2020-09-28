using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dari.Data.Infrastructure;
using Dari.Domain;

namespace Dari.Service
{
    public class ServiceFurniture : Service<Furniture>, IServiceFurniture
    {
        public ServiceFurniture(IUnitOfWork _uow) : base(_uow)
        {
        }

        public IEnumerable<Furniture> GetByCategory(int furid)
        {
            return GetMany(f => f.FurnitureId== furid);
        }

        public IEnumerable<Furniture> GetByuserid(string UserId)
        {
            return GetMany(f => f.UserId.Equals(UserId));
        }
    }
}
