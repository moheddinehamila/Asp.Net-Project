using Dari.Data;
using Dari.Data.Infrastructure;
using Dari.Domain;
using Dari.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dari.Service
{
  public  class ServiceVerification : Service<Verification>, IServiceVerification 
    {
        public ServiceVerification(IUnitOfWork _uow) : base(_uow)
        {
        }

       

        public IEnumerable<Verification> GetByuserid(string UserId)
        {
            return GetMany(f => f.userId.Equals(UserId));
        }
    }
}
