using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQlExplore.Interfaces;
using GraphQlExplore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraphQlExplore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private IProduct _productService;
        public ProductsController(IProduct productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _productService.GetAllProducts();
        }

        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return _productService.GetProductById(id);
        }

        [HttpPost]
        public Product Post([FromBody] Product product)
        {
            _productService.AddProduct(product);
            return product;
        }

        [HttpPut("{id}")]
        public Product Put(int id, [FromBody] Product product)
        {
            _productService.UpdateProduct(id, product);
            return product;
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _productService.DeleteProduct(id);
        }


    }
}
