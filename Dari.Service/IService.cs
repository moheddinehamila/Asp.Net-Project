using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dari.Service
{
    public interface IService<T> where T : class
    {
        void Add(T obj);
        void Update(T obj);
        void Deleteq(T obj);
        void Delete(Expression<Func<T, bool>> Condition);
        void Commit();
        T GetById(string id);
        T GetById(int id);
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where = null, Expression<Func<T, bool>> orderBy = null);
        IEnumerable<T> GetMany2(Expression<Func<T, bool>> condition = null);
        IEnumerable<T> GetAll();




    }
}
