using GraphQL;
using GraphQL.Types;
using GraphQlExplore.GraphQl.Types;
using GraphQlExplore.Interfaces;
using GraphQlExplore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQlExplore.GraphQl.Mutations
{
    public class ProductMutation : ObjectGraphType
    {        
        public ProductMutation(IProduct productService)
        {
            //Create product
            Field<ProductType>("createProduct", arguments: new QueryArguments(new QueryArgument<ProductInputType> { Name = "product" }),
               resolve: context =>
               {
                   return productService.AddProduct(context.GetArgument<Product>("product"));
               });

            //Update product
            Field<ProductType>("updateProduct", 
                arguments: 
                new QueryArguments(
                new QueryArgument<IntGraphType> { Name = "id" },
                new QueryArgument<ProductInputType> { Name = "product" }),
               resolve: context =>
               {
                   return productService.UpdateProduct(
                       context.GetArgument<int>("id"),
                       context.GetArgument<Product>("product"));
               });

            //Delete product. There is no void type and so return a string
            Field<StringGraphType>("deleteProduct",
               arguments:
               new QueryArguments(
               new QueryArgument<IntGraphType> { Name = "id" }),
              resolve: context =>
              {
                  var productId = context.GetArgument<int>("id");
                  productService.DeleteProduct(productId);
                  return $"ProductId: {productId} has been deleted";
              });
        }
    }
}
