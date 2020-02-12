using CRUDOperation.Abstractions.BLL;
using CRUDOperation.Abstractions.Repositories;
using CRUDOperation.BLL;
using CRUDOperation.DatabaseContext;
using CRUDOperation.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDOperation.Configurations
{
    public class ServicesConfigurations
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ICustomerManager, CustomerManager>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<DbContext, CRUDOperationDbContext>();

            services.AddTransient<IProductManager, ProductManager>();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddTransient<ICategoryManager, CategoryManager>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();

            services.AddTransient<IStockManager, StockManager>();
            services.AddTransient<IStockRepository, StockRepository>();

            services.AddTransient<IVariantManager, VariantManager>();
            services.AddTransient<IVariantRepository, VariantRepository>();

            services.AddTransient<IProductOrderManager, ProductOrderManager>();
            services.AddTransient<IProductOrderRepository, ProductOrderRepository>();

            services.AddTransient<IOrderManager, OrderManager>();
            services.AddTransient<IOrderRepository, OrderRepository>();

            services.AddTransient<Microsoft.AspNetCore.Identity.IdentityUser, Microsoft.AspNetCore.Identity.IdentityUser>();
        }
    }
}
