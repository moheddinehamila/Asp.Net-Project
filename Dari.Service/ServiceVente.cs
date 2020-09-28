using Dari.Domain;
using Dari.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dari.Service
{
    public class ServiceVente : Service<AnnonceVente>, IServiceVente
    {
        public ServiceVente(IUnitOfWork _uow) : base(_uow)
        {
        }

        public IEnumerable<AnnonceVente> GetByCategory(int venteid)
        {
            return GetMany(f => f.VenteId == venteid);
        }

        public IEnumerable<AnnonceVente> GetByuserid(int UserId)
        {
            return GetMany(f => f.UserId == UserId.ToString());
        }
    }
}
