using Dari.Data.Infrastructure;
using Dari.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dari.Service
{
    public class ServiceRequest : Service<Request>, IServiceRequest
    {
        public ServiceRequest(IUnitOfWork uow) : base(uow)
        {

        }
    }
}