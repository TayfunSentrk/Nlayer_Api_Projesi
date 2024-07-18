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
            CreateMap<ProductUpdateDto, Product>();//Çünkü tek yönlü olucak ondan reverse yapmadım
            CreateMap<Product, ProductWithCategoryDto>().ForMember(dest=>dest.CategoryDto,opt=>opt.MapFrom(src=>src.Category));//Burda tek yönlü product product withcategory çeviriyoruz 
        }
    }
}
