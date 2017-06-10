using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngkorMoon.DataModel.Repositories
{
    public interface IRepository<TContext, T> 
        where T : class 
        where TContext : DbContext
    {
        TContext DbContext { get; }

        T Get(long id);

        IQueryable<T> GetAll();

        void Save();

        T New<TSub>() where TSub : T;

        void Delete(T entity);
    }
}
