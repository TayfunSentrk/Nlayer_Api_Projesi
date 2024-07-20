using AutoMapper;
using Nlayer.Core.Dtos;
using Nlayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Service.Mapping
{
    public class MapProfile:Profile 
    {
        public MapProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();//burda ters yönde her ikisi birbirine dönüşebilir
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<ProductFeature,ProductFeatureDto>().ReverseMap();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductDto, ProductUpdateDto>();//tek yönlü ilişki.
            CreateMap<ProductUpdateDto, Product>().ReverseMap();
            CreateMap<Product, ProductWithCategoryDto>().ForMember(dest=>dest.CategoryDto,opt=>opt.MapFrom(src=>src.Category));//Burda tek yönlü product product withcategory çeviriyoruz
            CreateMap<Category, CategoryWithProducts>().ForMember(dest => dest.ProductDtos, opt => opt.MapFrom(src => src.Products));        //Burda tek yönlü Category'i CategoryWithProducts'ye çeviriyoruz                                                                                                            //
        }
    }
}
