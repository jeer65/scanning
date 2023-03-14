using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppScanning.Services;

//TODO: Decide on a repository pattern and implement it.
public interface IRepository<T>
{
    void Insert(T entity);
    void Delete(T entity);
    IQueryable<T> Find(Expression<Func<T, bool>> predicate);
    IQueryable<T> FindAll();
    T GetById(int id);
}