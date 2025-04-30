using Microsoft.EntityFrameworkCore;
using PizzaCatalog.WebApi.Data;
using PizzaCatalog.WebApi.Model.Domain;
using PizzaCatalog.WebApi.Model.DTOs;

namespace PizzaCatalog.WebApi.Repositories
{
    public class ToppingsRepository : IToppingsRepository
    {
        private readonly PizzaCatalogDBContext _dbContext;

        public ToppingsRepository(PizzaCatalogDBContext dBContext)
        {
            _dbContext = dBContext;

        }

        public async Task<List<ToppingsDTO>> GetAllToppingsAsync()
        {
            var toppings = await _dbContext.Toppings.ToListAsync();

            var toppingsDTOList = new List<ToppingsDTO>();

            foreach (var topping in toppings)
            {
                var toppingDTO = new ToppingsDTO()
                {
                    Id = topping.Id,
                    Name = topping.Name,
                    Price = topping.Price
                };
                toppingsDTOList.Add(toppingDTO);
            }

            return toppingsDTOList;
        }

        public async Task<ToppingsDTO> GetToppingsByIdAsync(int id)
        {
            var toppings = await _dbContext.Toppings.FirstOrDefaultAsync(x => x.Id == id);
            var toppingsDTO = new ToppingsDTO();

            if (toppings != null)
            {
                toppingsDTO = new ToppingsDTO()
                {
                    Id = toppings.Id,
                    Name = toppings.Name,
                    Price = toppings.Price
                };
            }

            return toppingsDTO;
        }


        public async Task<ToppingsDTO> InsertToppings(ToppingsDTO toppingsDTO)
        {
            var toppings = new Toppings()
            {
                Name = toppingsDTO.Name,
                Price = toppingsDTO.Price
            };

            _dbContext.Toppings.Add(toppings);
            _dbContext.SaveChanges();

            var toppingDTO = new ToppingsDTO()
            {
                Id = toppings.Id,
                Name = toppings.Name,
                Price = toppings.Price
            };

            return toppingDTO;
        }
    }
}
