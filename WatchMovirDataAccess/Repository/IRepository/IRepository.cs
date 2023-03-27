using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WacthMovie.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T- Category(suppose )
        T GetFirstorDefault(Expression<Func<T, bool>> filter, string? includeProperties = null ); 
        //categorycontroller bata retrieve
        IEnumerable<T> GetAll(string? includeProperties = null);
         
        void Add(T entity);

        //update ko lagi generic repo ma narakhney

        //for Delete
        void Remove(T entity);  
        void RemoveRange(IEnumerable<T> entity);  
    }
}
