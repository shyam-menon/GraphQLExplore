using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphiQl;
using GraphQL.Server;
using GraphQL.Types;
using GraphQlExplore.Data;
using GraphQlExplore.GraphQl.Mutations;
using GraphQlExplore.GraphQl.Queries;
using GraphQlExplore.GraphQl.Schemas;
using GraphQlExplore.GraphQl.Types;
using GraphQlExplore.Interfaces;
using GraphQlExplore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GraphQlExplore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IProduct, ProductService>();

            //GraphQL types, queries,schemas and mutations
            services.AddSingleton<ProductType>();
            services.AddScoped<ProductQuery>();
            services.AddScoped<ProductMutation>();
            services.AddScoped<ISchema,ProductSchema>();

            services.AddGraphQL(options =>
            {
                //start time, end time, duration etc are included in the response if the metrics is enabled
                options.EnableMetrics = false;
            }).AddSystemTextJson();

            //EF DB context with SQL server
            services.AddDbContext<ProductsDbContext>
                (option => option.UseSqlServer(@"Data Source=.; Initial Catalog=GQlProductDB; Integrated Security = True"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ProductsDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Ensure that DB exists
            dbContext.Database.EnsureCreated();

            //Enable support for Graph QL playground at this endpoint and use the schema defined
            app.UseGraphiQl("/graphql");
            app.UseGraphQL<ISchema>();

            //Below lines can be commented if REST support is not needed
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
