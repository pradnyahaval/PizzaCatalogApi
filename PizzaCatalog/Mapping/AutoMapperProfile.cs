using AutoMapper;
using PizzaCatalog.WebApi.Model.Domain;
using PizzaCatalog.WebApi.Model.DTOs;

namespace PizzaCatalog.WebApi.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Pizzas, PizzasDTO>().ReverseMap();
        }
               
    }
}
