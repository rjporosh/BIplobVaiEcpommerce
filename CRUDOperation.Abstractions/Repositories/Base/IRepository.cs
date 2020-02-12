using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDOperation.Abstractions.Repositories.Base
{
    public interface IRepository<T> where T:class //generic class
    {
        bool Add(T entity);
        ICollection<T> GetAll();
        T GetById(long id);
        bool Update(T entity);
        bool Delete(T entity);

    }
}
