using Dari.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dari.Service
{
   public class ServiceRendezVous<T> : IRendezVous<T> where T : class
    {
        IRepository<T> repo;
        IUnitOfWork uow;

        public ServiceRendezVous(IUnitOfWork _uow)
        {
            uow = _uow;
            repo = uow.GetRepository<T>();
        }

        public void Add(T obj)
        {
            repo.Add(obj);
        }

        public void Commit()
        {
            repo.Commit();
        }

        public void Delete(T obj)
        {
            repo.Delete(obj);
        }

        public IEnumerable<T> GetAll()
        {
            return repo.GetAll();
        }

        public T GetById(int id)
        {
            return repo.GetById(id);
        }

        public T GetById(string id)
        {
            return repo.GetById(id);
        }

        public IEnumerable<T> GetByIDUser(Expression<Func<T, bool>> condition = null, Expression<Func<T, bool>> orderBy = null)
        {
            return repo.GetByIDUser(condition, orderBy);
        }

        public void Update(T obj)
        {
            repo.Update(obj);
        }
    }
}
