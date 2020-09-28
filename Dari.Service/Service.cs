using Dari.Data;
using Dari.Data.Infrastructure;
using Dari.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dari.Service
{
        public class Service<T> : IService<T> where T : class
        {
            IRepository<T> repo;
            IUnitOfWork uow;
            public Service(IUnitOfWork _uow)
            {
                uow = _uow;
                repo = uow.GetRepository<T>();
            }
            public void Add(T obj)
            {
                repo.Add(obj);
            }
            public void Update(T obj)
            {
                repo.Update(obj);
            }
            public void Deleteq(T obj)
            {
                repo.Deleteq(obj);
            }
            public void Delete(Expression<Func<T, bool>> condition)
            {
                repo.Delete(condition);
            }
            public void Commit()
            {
                repo.Commit();
            }
            public T GetById(string id)
            {
                return repo.GetById(id);
            }
            public T GetById(int id)
            {
                return repo.GetById(id);
            }
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> filter = null, Expression<Func<T, bool>> orderBy = null)
        {
            return uow.GetRepository<T>().GetMany(filter, orderBy);
        }
        public IEnumerable<T> GetMany2(Expression<Func<T, bool>> condition = null)
        {
            return repo.GetMany2(condition);
        }


        public IEnumerable<T> GetAll()
            {
                return repo.GetAll();
            }



  
    }
}
