using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Dari.Service
{
    public interface IRendezVous<T> where T : class
    {
        IEnumerable<T> GetAll();
        void Update(T obj);
        void Add(T obj);
        void Delete(T obj);
        T GetById(string id);
        IEnumerable<T> GetByIDUser(Expression<Func<T, bool>> condition = null, Expression<Func<T, bool>> orderBy = null);
        T GetById(int id);
        void Commit();
    }
}
