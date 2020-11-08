using GraphQL.Types;
using GraphQlExplore.GraphQl.Mutations;
using GraphQlExplore.GraphQl.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQlExplore.GraphQl.Schemas
{
    public class ProductSchema : Schema
    {
        public ProductSchema(ProductQuery productQuery, ProductMutation productMutation)
        {
            //Set up the query in the schema
            Query = productQuery;

            //Set up the mutation in the schema
            Mutation = productMutation;
        }
    }
}
