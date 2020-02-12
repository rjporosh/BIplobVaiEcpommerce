using CRUDOperation.Abstractions.BLL.Base;
using CRUDOperation.Abstractions.Repositories.Base;
using System;
using System.Collections.Generic;

namespace CRUDOperation.BLL.Base
{
    public abstract class Manager<T> : IManager<T> where T : class
    {
        private IRepository<T> _repository;

        public Manager(IRepository<T> repository)
        {
            _repository = repository;
        }
        //declared the working way
        public virtual bool Add(T entity)
        {
            return _repository.Add(entity);
        }

        public virtual ICollection<T> GetAll()
        {
            return _repository.GetAll();
        }

        public virtual bool Update(T entity)
        {
            return _repository.Update(entity);
        }

        public virtual bool Delete(T entity)
        {
            return _repository.Delete(entity);
        }

        public virtual T GetById(long id)
        {
            return _repository.GetById(id);
        }
    }
}
