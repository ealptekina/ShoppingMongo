using AutoMapper;
using ShoppingMongo.Dtos.CategoryDtos;
using ShoppingMongo.Dtos.CustomerDtos;
using ShoppingMongo.Dtos.ProductDos;
using ShoppingMongo.Entities;

namespace ShoppingMongo.Mapping
{
    public class GeneralMapping : Profile
    {
        //ctor
        public GeneralMapping()
        {

            // Category mapping
            // Category -> CreateCategoryDto eşleme (sadece veri okuma)
            CreateMap<Category, CreateCategoryDto>().ReverseMap(); // çift yönlü eşleme

            // Category -> ResultCategoryDto eşleme (listeleme işlemleri için)
            CreateMap<Category, ResultCategoryDto>().ReverseMap();

            // Category -> GetCategoryByIdDto eşleme (tekil veri getirme)
            CreateMap<Category, GetCategoryByIdDto>().ReverseMap();

            // Category -> UpdateCategoryDto eşleme (güncelleme işlemleri için)
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();

            // Customer mapping
            CreateMap<Customer, CreateCustomerDto>().ReverseMap();
            CreateMap<Customer, ResultCustomerDto>().ReverseMap();
            CreateMap<Customer, GetCustomerByIdDto>().ReverseMap();
            CreateMap<Customer, UpdateCustomerDto>().ReverseMap();

            // Product mapping
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, ResultProductDto>().ReverseMap();
            CreateMap<Product, GetProductByIdDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
        }
    }
}
