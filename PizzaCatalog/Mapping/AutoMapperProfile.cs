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
            CreateMap<PizzaImages, PizzaImagesDTO>().ReverseMap();
            CreateMap<PizzaToppings, PizzaToppingsDTO>().ReverseMap();
            CreateMap<PizzaUpdateDTO, Pizzas>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
               
    }
}
