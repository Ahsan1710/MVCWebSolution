using MVCWeb.DataAccess.Data;
using MVCWeb.DataAccess.Repository.IRepository;
using MVCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVCWeb.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            var ObjFromDb = _db.Products.FirstOrDefault(p => p.Id == product.Id);
            if (ObjFromDb != null)
            {
                ObjFromDb.Title = product.Title;
                ObjFromDb.Description = product.Description;
                ObjFromDb.ISBN = product.ISBN;
                ObjFromDb.Author = product.Author;
                ObjFromDb.ListPrice = product.ListPrice;
                ObjFromDb.Price = product.Price;
                ObjFromDb.Price50 = product.Price50;
                ObjFromDb.Price100 = product.Price100;
                ObjFromDb.CategoryId = product.CategoryId;
                if (product.ImageUrl != null)
                {
                    ObjFromDb.ImageUrl = product.ImageUrl;
                }
            }
        }
    }
}
