using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WacthMovie.DataAccess.Repository.IRepository;
using WatchMovie.Models;
using WatchMovieWeb.DataAccess;

namespace WacthMovie.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        private ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }   


        public void Update(Product obj)
        {

            //_db can be used directy here but bahira paudaina ...
            var objFromDb = _db.Products.FirstOrDefault(u=> u.Id == obj.Id);
            if(objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Price = obj.Price;
                objFromDb.ListPrice= obj.ListPrice;
                objFromDb.Price50 = obj.Price50;
                objFromDb.Price100 = obj.Price100;
                objFromDb.Description = obj.Description;
                objFromDb.AuthorName = obj.AuthorName;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.CoverTypeId = obj.CoverTypeId;
                objFromDb.ISBN = obj.ISBN;
                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }

            }
        }
    }
}
