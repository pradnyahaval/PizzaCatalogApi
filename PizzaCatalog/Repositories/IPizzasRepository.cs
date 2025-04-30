using PizzaCatalog.WebApi.Model.DTOs;

namespace PizzaCatalog.WebApi.Repositories
{
    public interface IPizzasRepository
    {
        Task<List<PizzasDTO>> GetPizzasAsync();

        Task<PizzasDTO> GetPizzaByIdAsync(int id);

        Task<PizzasDTO> InsertPizzaAsync(PizzaRequestDTO pizzasDTO);

        Task<PizzasDTO> UpdatePizzaAsync(int pizzaid, PizzaUpdateDTO pizzaDTO);

        Task DeletePizzaByIdAsync(int id);
      
    }
}
