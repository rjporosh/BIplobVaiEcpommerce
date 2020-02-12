using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Linq;
using CRUDOperation.Abstractions.Repositories.Base;

namespace CRUDOperation.Repositories.Base
{
    public abstract class EFRepository<T>:IRepository<T> where T:class
    {
        private DbContext _db;

        public EFRepository(DbContext db)
        {
            _db = db;
        }
        public virtual bool Add(T entity)
        {
            _db.Set<T>().Add(entity);
            return _db.SaveChanges() > 0;
        }

        public virtual ICollection<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public virtual bool Update(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            return _db.SaveChanges() > 0;
        }

        public virtual bool Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
            return _db.SaveChanges() > 0;
        }

        public virtual T GetById(long id)
        {
            return _db.Set<T>().Find(id);
        }
    }
}
