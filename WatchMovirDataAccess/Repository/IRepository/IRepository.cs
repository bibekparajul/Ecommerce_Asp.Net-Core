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
        T GetFirstorDefault(Expression<Func<T, bool>> filter); 
        //categorycontroller bata retrieve
        IEnumerable<T> GetAll();

        void Add(T entity);

        //update ko lagi generic repo ma narakhney

        //for Delete
        void RemoveRange(IEnumerable<T> entity);  
    }
}
