using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
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
                    PizzaImages = new PizzaImagesDTO()
                    {
                        Id = p.PizzaImages.Id,
                        PizzaId = p.PizzaImages.PizzaId,
                        PizzaImageUrl = p.PizzaImages.PizzaImageUrl
                    },
                    PizzaToppings = p.PizzaToppings.Select(pt => new PizzaToppingsDTO()
                    {
                        Id = pt.Toppings.Id,
                        PizzId = pt.PizzId,
                        ToppingId = pt.Toppings.Id
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
                    PizzaImages = new PizzaImagesDTO()
                    {
                        Id = p.PizzaImages.Id,
                        PizzaId = p.PizzaImages.PizzaId,
                        PizzaImageUrl = p.PizzaImages.PizzaImageUrl
                    },
                    PizzaToppings = p.PizzaToppings.Select(pt => new PizzaToppingsDTO()
                    {
                        Id = pt.Toppings.Id,
                        PizzId = pt.PizzId,
                        ToppingId = pt.Toppings.Id

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

        public async Task<PizzasDTO> UpdatePizzaAsync(int pizzaid, PizzaUpdateDTO pizzaDTO)
        {
            var pizza = await _dbContext.Pizzas.FindAsync(pizzaid);

           
            if(pizza != null && pizzaid == pizza.Id)
            {
                _mapper.Map(pizzaDTO, pizza);   //only non-nullable fields will update
                await _dbContext.SaveChangesAsync();
            }

            var pizzaDTOdetails = _mapper.Map<PizzasDTO>(await _dbContext.Pizzas.FindAsync(pizzaid));


            return pizzaDTOdetails;
        }

        public async Task DeletePizzaByIdAsync(int id)
        {
            var pizza = await _dbContext.Pizzas.FindAsync(id);

            if(pizza != null)
            {
                _dbContext.Pizzas.Remove(pizza);
                _dbContext.SaveChanges();
            }
                
        }
    }
}
