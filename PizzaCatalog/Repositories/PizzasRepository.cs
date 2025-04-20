using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PizzaCatalog.WebApi.Data;
using PizzaCatalog.WebApi.Model.Domain;
using PizzaCatalog.WebApi.Model.DTOs;

namespace PizzaCatalog.WebApi.Repositories
{
    public class PizzasRepository : IPizzasRepository
    {
        private readonly PizzaCatalogDBContext _dbContext;
        private readonly IMapper _mapper;
        public PizzasRepository(PizzaCatalogDBContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }

        public async Task<List<PizzasDTO>> GetPizzasAsync()
        {
            var pizzasDTO = await _dbContext.Pizzas
                .Include(p => p.PizzaToppings)
                .ThenInclude(pt => pt.Toppings)
                .Include(p => p.PizzaImages)
                .Select(p => new PizzasDTO()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    BasePrice = p.BasePrice,
                    IsVeg = p.IsVeg,
                    //Image = p.PizzaImages.PizzaImageUrl,
                    PizzaImages = p.PizzaImages,
                    Toppings = p.PizzaToppings.Select(pt => new PizzaToppingsDTO()
                    {
                        Id = pt.Toppings.Id,
                        Name = pt.Toppings.Name
                    }).ToList()
                }).ToListAsync();

            return pizzasDTO;
        }

        public async Task<PizzasDTO> GetPizzaByIdAsync(int id)
        {
            var pizzaDTO = await _dbContext.Pizzas
                .Include(p => p.PizzaToppings)
                .ThenInclude(pt => pt.Toppings)
                .Include(p => p.PizzaImages)
                .Select(p => new PizzasDTO()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    BasePrice = p.BasePrice,
                    IsVeg = p.IsVeg,
                    //Image = p.PizzaImages.PizzaImageUrl,
                    PizzaImages = p.PizzaImages,
                    Toppings = p.PizzaToppings.Select(pt => new PizzaToppingsDTO()
                    {
                        Id = pt.Toppings.Id,
                        Name = pt.Toppings.Name

                    }).ToList()
                })
                .FirstOrDefaultAsync(x => x.Id == id);

            return pizzaDTO;
        }

        public async Task<PizzasDTO> InsertPizzaAsync(PizzaRequestDTO pizzasDTO)
        {
            var pizza = new Pizzas()
            {
                Name = pizzasDTO.Name,
                Description = pizzasDTO.Description,
                BasePrice = pizzasDTO.BasePrice,
                IsVeg = pizzasDTO.IsVeg,
                PizzaImages = new PizzaImages()
                {
                    PizzaImageUrl = pizzasDTO.Image
                },
                PizzaToppings = pizzasDTO.Toppings.Select(p => new PizzaToppings()
                {
                    
                    ToppingId = p.ToppingId,
                    IsDefault_Topping = true
                }).ToList()
            };

            await _dbContext.Pizzas.AddAsync(pizza);
            await _dbContext.SaveChangesAsync();


            var pizzaDTO = _mapper.Map<PizzasDTO>(pizza);

            return pizzaDTO;
        }
    }
}
