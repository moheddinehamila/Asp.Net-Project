using Dari.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dari.Service
{
    public interface IServiceFurniture:IService<Furniture>
    {

        IEnumerable<Furniture> GetByCategory(int categoryId);
        IEnumerable<Furniture> GetByuserid(string UserId);

    }
}
