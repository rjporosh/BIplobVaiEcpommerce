using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDOperation.Abstractions.BLL.Base
{
    public interface IManager<T> where T:class
    {
        //declared the signatures
        bool Add(T entity);
        ICollection<T> GetAll();
        T GetById(long id);
        bool Update(T entity);
        bool Delete(T entity);
    }
}
