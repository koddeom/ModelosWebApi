using AutoMapper;
using Controller_EF_Dapper_Repository_UnityOfWork.AppDomain.Database.Entities;
using Controller_EF_Dapper_Repository_UnityOfWork.Business.Models.Product;
using Controller_EF_Dapper_Repository_UnityOfWork.Endpoints.Categories.DTO;
using Controller_EF_Dapper_Repository_UnityOfWork.Endpoints.Orders.DTO;
using Controller_EF_Dapper_Repository_UnityOfWork.Endpoints.Products.DTO;

namespace Controller_EF_Dapper_Repository_UnityOfWork.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Product
            CreateMap<Product, ProductRequestDTO>();
            CreateMap<Product, ProductResponseDTO>();
            CreateMap<Product, ProductSoldResponseDTO>();

            //Category
            CreateMap<Category, CategoryRequestDTO>();
            CreateMap<Category, CategoryResponseDTO>();

            //Order
            CreateMap<Order, OrderRequestDTO>();
            CreateMap<OrderDetailed, OrderDetailedResponseDTO>();
        }
    }
}