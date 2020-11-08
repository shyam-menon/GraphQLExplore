using GraphQL;
using GraphQL.Types;
using GraphQlExplore.GraphQl.Types;
using GraphQlExplore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQlExplore.GraphQl.Queries
{
    public class ProductQuery : ObjectGraphType
    {
        public ProductQuery(IProduct productService)
        {
            Field<ListGraphType<ProductType>>("products", resolve: context => { return productService.GetAllProducts(); });

            Field<ProductType>("product", arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id"}),                
                resolve: context => 
                {
                    return productService.GetProductById(context.GetArgument<int>("id")); 
                });
        }
    }
}
