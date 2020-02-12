using AutoMapper;
using CRUDOperation.Models;
using CRUDOperation.Models.APIViewModels;
using CRUDOperation.Models.RazorViewModels.Category;
using CRUDOperation.Models.RazorViewModels.Customer;
using CRUDOperation.Models.RazorViewModels.Order;
using CRUDOperation.Models.RazorViewModels.Product;
using CRUDOperation.Models.RazorViewModels.ProductOrder;
using CRUDOperation.Models.RazorViewModels.Stock;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDOperation.Configurations.AutoMapperConfigurations
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CustomerCreateViewModel,Customer>();
            CreateMap<Customer,CustomerCreateViewModel>();
            CreateMap<ProductCreateViewModel, Product>();
            CreateMap<Product, ProductCreateViewModel>();/*.ForMember(vm => vm.CategoryName, map => map.MapFrom(m => m.Category.Name));*/
            CreateMap<StockCreateViewModel, Stock>();
            CreateMap<Stock, StockCreateViewModel>();

            CreateMap<Product, ProductDto>();

            CreateMap<Category, CategoryCreateViewModel>();
            CreateMap<CategoryCreateViewModel, Category>();

            CreateMap<Order, OrderCreateViewModel>();
            CreateMap<OrderCreateViewModel, Order>();

            CreateMap<ProductOrder, ProductOrderVM>();
            CreateMap<ProductOrderVM, ProductOrder>();
        }
    }
}
