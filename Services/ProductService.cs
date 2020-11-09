using GraphQlExplore.Data;
using GraphQlExplore.Interfaces;
using GraphQlExplore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQlExplore.Services
{
    public class ProductService : IProduct
    {

        private ProductsDbContext _dbContext;

        public ProductService(ProductsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //private static List<Product> products = new List<Product>
        //{
        //    new Product{Id = 0, Name = "Coffee", Price = 10},
        //    new Product{Id = 1, Name = "Tea", Price = 15}
        //};
        public Product AddProduct(Product product)
        {
            //products.Add(product);
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return product;
        }

        public void DeleteProduct(int id)
        {
            //products.RemoveAt(id);
            var productToDelete = _dbContext.Products.Find(id);
            _dbContext.Products.Remove(productToDelete);
            _dbContext.SaveChanges();
        }

        public List<Product> GetAllProducts()
        {
            //return products;
            return _dbContext.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            //return products.Find(p => p.Id == id);            
            return _dbContext.Products.Find(id);
        }

        public Product UpdateProduct(int id, Product product)
        {
            //products[id] = product;
            var productToUpdate = _dbContext.Products.Find(id);
            productToUpdate.Name = product.Name;
            productToUpdate.Price = product.Price;
            _dbContext.SaveChanges();
            return product;
        }
    }
}
