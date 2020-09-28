using Dari.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dari.Service
{
    public interface IServiceVente:IService<AnnonceVente>
    {
        IEnumerable<AnnonceVente> GetByCategory(int categoryId);
        IEnumerable<AnnonceVente> GetByuserid(int UserId);

    }
}
